using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    // Input
    public PlayerInput _PlayerInput;
    InputAction _move;
    Vector2 _moveDirection;

    // Rigidbody and Colliders
    Rigidbody2D _rb;
    public Collider2D _groundCollider;
    bool grounded;
    bool sloped;

    // Movement
    float speed = 5f;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _PlayerInput = new PlayerInput();
    }

    void OnEnable() {
        _move = _PlayerInput.Player.Move;
        _move.Enable();
    }

    void OnDisable() {
        _move.Disable();
    }

    void Update() {
        _moveDirection = _move.ReadValue<Vector2>();
        
    // Check if grounded
        Collider2D[] ground = Physics2D.OverlapBoxAll(_groundCollider.bounds.center, _groundCollider.bounds.size, 0);
        foreach (Collider2D groundCollider in ground) {
            if (groundCollider.gameObject != gameObject) {
                if (groundCollider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                    grounded = true;
                } else {
                    grounded = false;
                }
            }
        }

    }

    void FixedUpdate() {
        if (grounded) {
            _rb.velocity = new Vector2(_moveDirection.x * speed, _rb.velocity.y);
        }
    }

    void OnJump() {
        if (grounded) {
            _rb.velocity = new Vector2(30 * _moveDirection.x, 25);
        }
    }

    public void OnBonk() {
        _rb.velocity = new Vector2(-_rb.velocity.x/2, _rb.velocity.y/2);
    }

    public bool IsGounded() {
        return grounded;
    }

    public void Sloped(bool sloped) {
        this.sloped = sloped;
    }
}
