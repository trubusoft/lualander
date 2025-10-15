using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {
    public enum LandingType {
        Success,
        LandedOnTerrain,
        LandedTooSteep,
        LandedTooFast,
    }

    public enum State {
        Ready,
        Playing,
        GameOver
    }

    private const float GravityNormal = 0.7f;
    private const float GravityDisabled = 0f;
    private const float ThrustSpeed = 700f;
    private const float TorqueSpeed = 300f;
    private const float SpeedThreshold = 4f;
    private const float AngleThreshold = 0.9f;
    private const float FuelStartingAmount = 10f;
    private const float FuelPickupAmount = 10f;
    private const float FuelConsumptionRate = 1f;
    private float _fuelAmount;
    private Rigidbody2D _rigidbody2D;
    private State _state;
    public static Lander instance { get; private set; }

    private void Awake() {
        AssignSingleton();
        AssignRigidBody();
        AssignEventCallback();
        SetState(State.Ready);
    }

    private void FixedUpdate() {
        HandleIdle();

        switch (_state) {
            case State.Ready:
                if (Keyboard.current.upArrowKey.isPressed ||
                    Keyboard.current.upArrowKey.isPressed ||
                    Keyboard.current.upArrowKey.isPressed) {
                    SetState(State.Playing);
                }

                break;
            case State.Playing:
                bool isMoveable = 0f < _fuelAmount;
                if (isMoveable) {
                    bool isGoingUp = HandleUpwardThrust();
                    bool isGoingLeft = HandleLeftRotation();
                    bool isGoingRight = HandleRightRotation();
                    if (isGoingUp || isGoingLeft || isGoingRight) {
                        ConsumeFuel();
                    }
                }

                break;
            case State.GameOver:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D otherCollision2D) {
        HandleTerrainCollision(otherCollision2D);
        HandleLandingPadCollision(otherCollision2D);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider2D) {
        HandleFuelCollision(otherCollider2D);
        HandleCoinCollision(otherCollider2D);
    }

    private void AssignSingleton() {
        instance = this;
    }

    private void AssignRigidBody() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(_rigidbody2D);
    }

    private void AssignEventCallback() {
        OnStateChanged += HandleOnStateChanged;
    }

    private void HandleOnStateChanged(object sender, OnStateChangedArgs e) {
        switch (_state) {
            case State.Ready:
                _fuelAmount = FuelStartingAmount;
                _rigidbody2D.gravityScale = GravityDisabled;
                break;
            case State.Playing:
                _rigidbody2D.gravityScale = GravityNormal;
                break;
            case State.GameOver:
                _rigidbody2D.gravityScale = GravityNormal;
                _rigidbody2D.gravityScale = GravityDisabled;
                break;
        }
    }

    private void HandleLandingPadCollision(Collision2D landingPadCollision) {
        if (landingPadCollision.gameObject.TryGetComponent(out LandingPad landingPad)) {
            float landingSpeed = CalculateLandingSpeed(landingPadCollision);
            float landingAngle = CalculateLandingAngle();

            bool isLandingSpeedValid = landingSpeed < SpeedThreshold;
            bool isLandingAngleValid = AngleThreshold <= landingAngle;
            bool isWinConditionMet = isLandingSpeedValid && isLandingAngleValid;

            OnLandingArgs onLandingArgs = new OnLandingArgs {
                LandingSpeed = landingSpeed,
                LandingAngle = landingAngle,
                ScoreMultiplier = landingPad.GetScoreMultiplier(),
                Score = 0
            };

            if (isWinConditionMet) {
                int finalScore = CalculateScore(landingSpeed, landingAngle, landingPad.GetScoreMultiplier());
                onLandingArgs.LandingType = LandingType.Success;
                onLandingArgs.Score = finalScore;
            } else if (!isLandingSpeedValid) {
                onLandingArgs.LandingType = LandingType.LandedTooFast;
            } else if (!isLandingAngleValid) {
                onLandingArgs.LandingType = LandingType.LandedTooSteep;
            }

            OnLanding?.Invoke(this, onLandingArgs);
            SetState(State.GameOver);
        }
    }

    private void HandleTerrainCollision(Collision2D terrainCollision) {
        if (terrainCollision.gameObject.TryGetComponent(out Wall _)) {
            float landingSpeed = CalculateLandingSpeed(terrainCollision);
            float landingAngle = CalculateLandingAngle();

            OnLanding?.Invoke(this, new OnLandingArgs {
                LandingType = LandingType.LandedOnTerrain,
                LandingSpeed = landingSpeed,
                LandingAngle = landingAngle,
                ScoreMultiplier = 0,
                Score = 0
            });
            SetState(State.GameOver);
        }
    }

    private void HandleFuelCollision(Collider2D fuelCollider) {
        if (fuelCollider.gameObject.TryGetComponent(out Fuel fuel)) {
            // refill fuel
            _fuelAmount += FuelPickupAmount;
            _fuelAmount = Mathf.Clamp(_fuelAmount, 0, FuelStartingAmount);
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
    public event EventHandler<OnStateChangedArgs> OnStateChanged;

    private void SetState(State state) {
        _state = state;
        OnStateChanged?.Invoke(this, new OnStateChangedArgs { State = state });
    }

    private static float CalculateLandingSpeed(Collision2D landingPadCollision) {
        var relativeVelocity = landingPadCollision.relativeVelocity;
        return relativeVelocity.magnitude;
    }

    private float CalculateLandingAngle() {
        return Vector2.Dot(Vector2.up, transform.up);
    }

    private int CalculateScore(float landingSpeed, float landingAngle, int scoreMultiplier) {
        const float maxAngleScore = 100;
        const float maxSpeedScore = 100;

        float angleScore = maxAngleScore - Mathf.Abs(landingAngle - 1f) * 10f * maxAngleScore;
        float speedScore = (SpeedThreshold - landingSpeed) * maxSpeedScore;

        int finalScore = (int)(speedScore + angleScore) * scoreMultiplier;
        return finalScore;
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

    public float GetFuelNormalized() {
        return _fuelAmount / FuelStartingAmount;
    }

    public State GetCurrentState() {
        return _state;
    }

    public class OnLandingArgs : EventArgs {
        public float LandingAngle;
        public float LandingSpeed;
        public LandingType LandingType;
        public int Score;
        public float ScoreMultiplier;
    }

    public class OnStateChangedArgs : EventArgs {
        public State State;
    }
}