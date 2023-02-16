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

    // Movement
    float speed = 5f;
    bool jumping;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        jumping = false;

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
    }

    void FixedUpdate() {
        if (!jumping) {
            _rb.velocity = new Vector2(_moveDirection.x * speed, _rb.velocity.y);
        }
    }

    public void ResetJump() {
        jumping = false;
    }

    void OnJump() {
        if (!jumping) {
            jumping = true;
            _rb.velocity = new Vector2(10 * _moveDirection.x, 20);
        }
    }

    public void OnBonk() {
        _rb.velocity = new Vector2(-_rb.velocity.x, _rb.velocity.y);
    }
}
