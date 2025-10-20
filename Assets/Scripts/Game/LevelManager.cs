using System;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelManager : MonoBehaviour {
    private const int CoinPickupScore = 100;
    [SerializeField] private int levelNumber;
    [SerializeField] private Scene currentScene;

    [SerializeField] private Scene nextScene;

    private bool _isLevelTimerActive;
    private LandedUI _landedUI;
    private Lander _lander;
    private int _levelScore;

    private float _levelTimer;
    private StatsUI _statsUI;

    private void Start() {
        _lander = FindAnyObjectByType<Lander>();
        _landedUI = FindAnyObjectByType<LandedUI>(FindObjectsInactive.Include);
        _statsUI = FindAnyObjectByType<StatsUI>(FindObjectsInactive.Include);

        Assert.IsNotNull(_lander);
        Assert.IsNotNull(_landedUI);
        Assert.IsNotNull(_statsUI);

        _lander.OnCoinPickup += LanderCoinPickup;
        _lander.OnLanding += LanderOnLanding;
        _lander.OnStateChanged += LanderOnStateChanged;

        _landedUI.SetUp(this, _lander);
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