using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {
    private const float ThrustSpeed = 5f;

    private Rigidbody2D _rigidbody2D;

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        HandleUpwardThrust();
    }

    private void HandleUpwardThrust() {
        if (Keyboard.current.upArrowKey.isPressed) {
            _rigidbody2D.AddForce(transform.up * ThrustSpeed, ForceMode2D.Force);
        }
    }
    
    private void HandleLeftRotation() {
        throw new NotImplementedException();
    }

    private void HandleRightRotation() {
        throw new NotImplementedException();
    }
}