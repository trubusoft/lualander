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
    private float _landingAngle;
    private float _landingSpeed;
    private Rigidbody2D _rigidbody2D;

    private bool isMoveable => 0f < _fuelAmount;

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(_rigidbody2D);
        _fuelAmount = FuelStartingAmount;
    }

    private void FixedUpdate() {
        HandleIdle();
        Debug.Log(_fuelAmount);
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
            CalculateLandingSpeed(landingPadCollision);
            CalculateLandingAngle();

            bool isWinCondition = IsLandingSpeedValid() && IsLandingAngleValid();
            if (isWinCondition) {
                Debug.Log("Win");
                CalculateScore(landingPad);
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
            // todo: add score
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

    private void CalculateLandingSpeed(Collision2D landingPadCollision) {
        var relativeVelocity = landingPadCollision.relativeVelocity;
        _landingSpeed = relativeVelocity.magnitude;
    }

    private void CalculateLandingAngle() {
        _landingAngle = Vector2.Dot(Vector2.up, transform.up);
    }

    private void CalculateScore(LandingPad landingPad) {
        const float maxAngleScore = 100;
        const float scoreMultiplier = 10f;
        float angleScore = maxAngleScore -
                           Mathf.Abs(_landingAngle - 1f) * scoreMultiplier * maxAngleScore;

        const float maxSpeedScore = 100;
        float speedScore = (SpeedThreshold - _landingSpeed) * maxSpeedScore;

        Debug.Log(speedScore);
        Debug.Log(angleScore);

        float finalScore = (speedScore + angleScore) * landingPad.GetScoreMultiplier();
        Debug.Log(finalScore);
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

    private bool IsLandingSpeedValid() {
        return _landingSpeed < SpeedThreshold;
    }

    private bool IsLandingAngleValid() {
        return AngleThreshold <= _landingAngle;
    }
}