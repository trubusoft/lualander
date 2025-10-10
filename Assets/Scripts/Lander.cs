using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {
    private const float ThrustSpeed = 700f;
    private const float TorqueSpeed = 300f;
    private const float SoftLandingThreshold = 4f;
    private const float StraightLandingThreshold = 0.9f;
    private Collision2D _collision2D;
    private float _landingMagnitude;
    private float _landingStraightness;
    private Rigidbody2D _rigidbody2D;

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        HandleUpwardThrust();
        HandleLeftRotation();
        HandleRightRotation();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        _collision2D = collision;

        if (IsCollidedWithLandingPad()) {
            if (IsSoftLanding() && IsStraightLanding()) {
                CalculateScore();
            }
        }
    }

    private void CalculateScore() {
        Debug.Log("CalculateScore");
    }

    private bool IsCollidedWithLandingPad() {
        Assert.IsNotNull(_collision2D);
        var isFound = _collision2D.gameObject.TryGetComponent(out LandingPad _);
        return isFound;
    }

    private void HandleUpwardThrust() {
        if (Keyboard.current.upArrowKey.isPressed) {
            _rigidbody2D.AddForce(transform.up * (ThrustSpeed * Time.deltaTime), ForceMode2D.Force);
        }
    }

    private void HandleLeftRotation() {
        if (Keyboard.current.leftArrowKey.isPressed) {
            _rigidbody2D.AddTorque(TorqueSpeed * Time.deltaTime);
        }
    }

    private void HandleRightRotation() {
        if (Keyboard.current.rightArrowKey.isPressed) {
            _rigidbody2D.AddTorque(-TorqueSpeed * Time.deltaTime);
        }
    }

    private bool IsSoftLanding() {
        Assert.IsNotNull(_collision2D);
        var relativeVelocity = _collision2D.relativeVelocity;
        _landingMagnitude = relativeVelocity.magnitude;
        return _landingMagnitude < SoftLandingThreshold;
    }

    private bool IsStraightLanding() {
        _landingStraightness = Vector2.Dot(Vector2.up, transform.up);
        return StraightLandingThreshold <= _landingStraightness;
    }
}