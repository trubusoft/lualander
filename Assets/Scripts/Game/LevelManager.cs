using System;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    private const int CoinPickupScore = 100;
    [SerializeField] private int levelNumber;
    [SerializeField] private Scene currentScene;
    [SerializeField] private Scene nextScene;

    private bool _isLevelTimerActive;
    private Lander _lander;
    private int _levelScore;
    private float _levelTimer;

    private void Start() {
        _lander = GetComponent<Lander>();

        _lander.OnCoinPickup += LanderCoinPickup;
        _lander.OnLanding += LanderOnLanding;
        _lander.OnStateChanged += LanderOnStateChanged;
    }

    private void Update() {
        TickLevelTimer();
    }

    public int GetLevelNumber() {
        return levelNumber;
    }

    private void AddLevelScore(int amount) {
        _levelScore += amount;
    }

    public int GetLevelScore() {
        return _levelScore;
    }

    public float GetLevelTime() {
        return _levelTimer;
    }

    private void TickLevelTimer() {
        if (_isLevelTimerActive) {
            _levelTimer += Time.deltaTime;
        }
    }

    private void LanderOnStateChanged(object sender, Lander.OnStateChangedArgs e) {
        switch (e.State) {
            case Lander.State.Ready:
                _isLevelTimerActive = false;
                break;
            case Lander.State.Playing:
                _isLevelTimerActive = true;
                // cinemachineCamera.Target.TrackingTarget = Lander.instance.transform;
                // CinemachineCameraZoom.instance.ResetOrthographicSize();
                break;
            case Lander.State.GameOver:
                _isLevelTimerActive = false;
                break;
        }
    }

    private void LanderOnLanding(object sender, Lander.OnLandingArgs e) {
        AddLevelScore(e.Score);
    }

    private void LanderCoinPickup(object sender, EventArgs e) {
        AddLevelScore(CoinPickupScore);
    }

    public void GoToNextLevel() {
        SessionManager.instance.AddTotalScore(_levelScore);
        SceneLoader.LoadScene(nextScene);
    }

    public void RetryLevel() {
        SceneLoader.LoadScene(currentScene);
    }
}