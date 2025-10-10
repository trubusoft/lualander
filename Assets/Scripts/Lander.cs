using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {
    private const float ThrustSpeed = 700f;
    private const float TorqueSpeed = 300f;
    private const float SpeedThreshold = 4f;
    private const float AngleThreshold = 0.9f;
    private Collision2D _collision2D;
    private float _landingAngle;
    private float _landingSpeed;
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
            CalculateLandingSpeed();
            CalculateLandingAngle();

            bool isWinCondition = IsLandingSpeedValid() && IsLandingAngleValid();
            if (isWinCondition) {
                Debug.Log("Win");
                CalculateScore();
                return;
            }
        }

        Debug.Log("Lose");
    }

    private void CalculateLandingSpeed() {
        Assert.IsNotNull(_collision2D);
        var relativeVelocity = _collision2D.relativeVelocity;
        _landingSpeed = relativeVelocity.magnitude;
    }

    private void CalculateLandingAngle() {
        Assert.IsNotNull(_collision2D);
        _landingAngle = Vector2.Dot(Vector2.up, transform.up);
    }

    private void CalculateScore() {
        const float maxAngleScore = 100;
        const float scoreMultiplier = 10f;
        float angleScore = maxAngleScore -
                           Mathf.Abs(_landingAngle - 1f) * scoreMultiplier * maxAngleScore;

        const float maxSpeedScore = 100;
        float speedScore = (SpeedThreshold - _landingSpeed) * maxSpeedScore;

        Debug.Log(speedScore);
        Debug.Log(angleScore);
    }

    private bool IsCollidedWithLandingPad() {
        Assert.IsNotNull(_collision2D);
        if (_collision2D.gameObject.TryGetComponent(out LandingPad landingPad)) {
            SetCurrentLandingPad(landingPad);
        }

        return false;
    }

    private void SetCurrentLandingPad(LandingPad landingPad) {
        _landingPad = landingPad;
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

    private bool IsLandingSpeedValid() {
        return _landingSpeed < SpeedThreshold;
    }

    private bool IsLandingAngleValid() {
        return AngleThreshold <= _landingAngle;
    }
}