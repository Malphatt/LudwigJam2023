using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    // Input
    public PlayerInput _PlayerInput;
    InputAction _move;
    InputAction _jump;
    Vector2 _moveDirection;

    public GameObject mainCamera;
    private GameObject currentZone;
    private GameObject previousZone;

    // Rigidbody and Colliders
    Rigidbody2D _rb;
    public Collider2D _groundCollider;
    public bool grounded;
    bool sloped;

    // Movement
    float speed = 3f;
    float jumpPower = 10f;
    float startJumpTime;
    float maxHoldTime = 1.25f;

    public bool jumping;
    bool jumpButtonRelease;
    bool jumped;

    void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();
        _PlayerInput = new PlayerInput();
        currentZone = GameObject.Find("Zone 1");
    }

    void OnEnable() 
    {
        _move = _PlayerInput.Player.Move;
        _jump = _PlayerInput.Player.Jump;
        _move.Enable();
        _jump.Enable();
    }

    void OnDisable() 
    {
        _move.Disable();
        _jump.Disable();
    }

    void Update() 
    {
        _moveDirection = _move.ReadValue<Vector2>();

        _jump.performed += ctx => Jump(true);
        _jump.canceled += ctx => Jump(false);
        
    // Check if grounded
        if (!jumping) {
            if (groundCheck()) {
                grounded = true;
            } else {
                grounded = false;
            }
        }

        //Move camera to current zone if not empty
        Vector3 travelpoint = new Vector3(currentZone.transform.position.x, currentZone.transform.position.y, -10);
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, travelpoint, 20f * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Zone")
        {
            currentZone = collision.gameObject;
            //Call set zone function in game manager
            GameObject.Find("GameManager").GetComponent<GameManager>().setZone(collision.gameObject);
        }
    }


    void FixedUpdate() {
        if ((sloped && grounded) || (!sloped && grounded)) {
            if (grounded && !jumping) {
                _rb.velocity = new Vector2(_moveDirection.x * speed, _rb.velocity.y);
            }
        }

        // Vary jumpPower dependent on how long space is pressed for
        if (jumping) {
            if (!jumped) { // Allows for a single jump after releasing the jump button or after the max hold time
                if (Time.time - startJumpTime < maxHoldTime) { // If the jump button is held for less than the max hold time
                    if (jumpButtonRelease) { // If the jump button is released
                        if (groundCheck()) {
                            jumped = true;
                            float jumpTimeNormalised = (Time.time - startJumpTime) / maxHoldTime; // Normalise the time to 0-1
                            float jumpHoldPower = convertJumpPower(jumpTimeNormalised) * jumpPower;
                            _rb.velocity = new Vector2(jumpHoldPower * _moveDirection.x, jumpHoldPower * 2.0f);
                        } else {
                            jumped = true;
                        }
                    }
                } else { // If the jump button is held for more than the max hold time
                    if (groundCheck()) {
                        jumped = true;
                        _rb.velocity = new Vector2(jumpPower * _moveDirection.x, jumpPower * 2.0f);
                    } else {
                        jumped = true;
                    }
                }
            }
        }
    }

    void Jump(bool jump) {
        if (jump) {
            if (grounded) {
                if (!jumping) {
                    grounded = false;
                    jumping = true;
                    jumpButtonRelease = false;
                    jumped = false;
                    _rb.velocity = new Vector2(0, _rb.velocity.y);
                    startJumpTime = Time.time;
                }
            }
        } else {
            jumpButtonRelease = true;
        }
    }

    bool groundCheck() {
        Collider2D[] ground = Physics2D.OverlapBoxAll(_groundCollider.bounds.center, _groundCollider.bounds.size, 0);
            
        foreach (Collider2D groundCollider in ground) {
            if (groundCollider.gameObject.layer == LayerMask.NameToLayer("Ground") && groundCollider.gameObject.tag != "Slope") {
                return true;
            }
        }
        return false;
    }

    public void OnBonk() {
        _rb.velocity = new Vector2(-_rb.velocity.x/2, _rb.velocity.y/2);
    }

    public void OnLand() {
        jumping = false;
    }

    public bool IsGounded() {
        return grounded;
    }

    public bool IsStationary() {
        return _moveDirection.x == 0;
    }
    
    public int LastMovingDirection() { // 0 = not moving, 1 = moving right, -1 = moving left
        if (_moveDirection.x > 0) {
            return 1;
        } else if (_moveDirection.x < 0) {
            return -1;
        } else {
            return 0;
        }
    }

    public void Sloped(bool sloped) {
        this.sloped = sloped;
    }

    float convertJumpPower(float normalisedJumpTime) {
        // Put normalised value into logarithmic function to figure out power
        // return log[101](100x +1)
        return Mathf.Log(100 * normalisedJumpTime + 1, 101);
    }
}
