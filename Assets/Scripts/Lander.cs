using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {
    private const float ThrustSpeed = 700f;
    private const float TorqueSpeed = 300f;
    private const float SoftLandingThreshold = 4f;
    private const float StraightLandingThreshold = 0.9f;
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
        if (IsCollidedWithLandingPad(collision)) {
            Debug.Log("Collided with Landing Pad");

            if (IsSoftLanding(collision)) {
                Debug.Log("Soft Landing");
            } else {
                Debug.Log("Hard landing");
            }

            if (IsStraightLanding()) {
                Debug.Log("Straight Landing");
            } else {
                Debug.Log("Steep Landing");
            }
        }
    }

    private static bool IsCollidedWithLandingPad(Collision2D other) {
        var isFound = other.gameObject.TryGetComponent(out LandingPad _);
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

    private static bool IsSoftLanding(Collision2D collision) {
        var relativeVelocity = collision.relativeVelocity;
        var magnitude = relativeVelocity.magnitude;
        return magnitude < SoftLandingThreshold;
    }

    private bool IsStraightLanding() {
        var straightness = Vector2.Dot(Vector2.up, transform.up);
        return StraightLandingThreshold <= straightness;
    }
}