using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {
    private const float ThrustSpeed = 700f;
    private const float TorqueSpeed = 300f;
    private const float SpeedThreshold = 4f;
    private const float AngleThreshold = 0.9f;
    private const float FuelStartingAmount = 10f;
    private const float FuelPickupAmount = 10f;
    private const float FuelConsumptionRate = 1f;
    private float _fuelAmount;
    private Rigidbody2D _rigidbody2D;

    public static Lander instance { get; private set; }

    private bool isMoveable => 0f < _fuelAmount;

    private void Awake() {
        instance = this;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(_rigidbody2D);
        _fuelAmount = FuelStartingAmount;
    }

    private void FixedUpdate() {
        HandleIdle();
        if (isMoveable) {
            bool isGoingUp = HandleUpwardThrust();
            bool isGoingLeft = HandleLeftRotation();
            bool isGoingRight = HandleRightRotation();
            if (isGoingUp || isGoingLeft || isGoingRight) {
                ConsumeFuel();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D otherCollision2D) {
        HandleLandingPadCollision(otherCollision2D);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider2D) {
        HandleFuelCollision(otherCollider2D);
        HandleCoinCollision(otherCollider2D);
    }

    private void HandleLandingPadCollision(Collision2D landingPadCollision) {
        if (landingPadCollision.gameObject.TryGetComponent(out LandingPad landingPad)) {
            float landingSpeed = CalculateLandingSpeed(landingPadCollision);
            float landingAngle = CalculateLandingAngle();

            bool isLandingSpeedValid = landingSpeed < SpeedThreshold;
            bool isLandingAngleValid = AngleThreshold <= landingAngle;
            bool isWinConditionMet = isLandingSpeedValid && isLandingAngleValid;

            if (isWinConditionMet) {
                CalculateScore(landingSpeed, landingAngle, landingPad.GetScoreMultiplier());
            }
        }
    }

    private void HandleFuelCollision(Collider2D fuelCollider) {
        if (fuelCollider.gameObject.TryGetComponent(out Fuel fuel)) {
            // refill fuel
            _fuelAmount += FuelPickupAmount;
            _fuelAmount = Mathf.Clamp(_fuelAmount, 0, float.MaxValue);
            fuel.DestroySelf();
        }
    }

    private void HandleCoinCollision(Collider2D coinCollider) {
        if (coinCollider.gameObject.TryGetComponent(out Coin coin)) {
            OnCoinPickup?.Invoke(this, EventArgs.Empty);
            coin.DestroySelf();
        }
    }

    private void ConsumeFuel() {
        if (0f < _fuelAmount) {
            float consumedFuel = FuelConsumptionRate * Time.deltaTime;
            _fuelAmount -= consumedFuel;
            _fuelAmount = Mathf.Clamp(_fuelAmount, 0, float.MaxValue);
        }
    }

    public event EventHandler OnIdle;
    public event EventHandler OnUpForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnCoinPickup;
    public event EventHandler<OnLandingArgs> OnLanding;

    private static float CalculateLandingSpeed(Collision2D landingPadCollision) {
        var relativeVelocity = landingPadCollision.relativeVelocity;
        return relativeVelocity.magnitude;
    }

    private float CalculateLandingAngle() {
        return Vector2.Dot(Vector2.up, transform.up);
    }

    private void CalculateScore(float landingSpeed, float landingAngle, int scoreMultiplier) {
        const float maxAngleScore = 100;
        const float maxSpeedScore = 100;

        float angleScore = maxAngleScore - Mathf.Abs(landingAngle - 1f) * 10f * maxAngleScore;
        float speedScore = (SpeedThreshold - landingSpeed) * maxSpeedScore;

        int finalScore = (int)(speedScore + angleScore) * scoreMultiplier;
        OnLanding?.Invoke(this, new OnLandingArgs {
            Score = finalScore,
        });
    }

    private void HandleIdle() {
        OnIdle?.Invoke(this, EventArgs.Empty);
    }

    private bool HandleUpwardThrust() {
        if (Keyboard.current.upArrowKey.isPressed) {
            _rigidbody2D.AddForce(transform.up * (ThrustSpeed * Time.deltaTime), ForceMode2D.Force);
            OnUpForce?.Invoke(this, EventArgs.Empty);
            return true;
        }

        return false;
    }

    private bool HandleLeftRotation() {
        if (Keyboard.current.leftArrowKey.isPressed) {
            _rigidbody2D.AddTorque(TorqueSpeed * Time.deltaTime);
            OnLeftForce?.Invoke(this, EventArgs.Empty);
            return true;
        }

        return false;
    }

    private bool HandleRightRotation() {
        if (Keyboard.current.rightArrowKey.isPressed) {
            _rigidbody2D.AddTorque(-TorqueSpeed * Time.deltaTime);
            OnRightForce?.Invoke(this, EventArgs.Empty);
            return true;
        }

        return false;
    }

    public float GetSpeedX() {
        return _rigidbody2D.linearVelocityX;
    }

    public float GetSpeedY() {
        return _rigidbody2D.linearVelocityY;
    }

    public float GetFuel() {
        return _fuelAmount;
    }

    public class OnLandingArgs : EventArgs {
        public int Score;
    }
}