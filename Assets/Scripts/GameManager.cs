using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private static int _levelNumber = 1;
    [SerializeField] private List<Level> levels;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    private bool _isTimerActive;

    private int _score;
    private float _time;
    public static GameManager instance { get; private set; }

    private void Awake() {
        instance = this;
        // Debug.Log("GameManager Awake");
        // if (instance != null && instance != this) {
        //     Debug.Log("GameManager Instance destroyed");
        //     Destroy(gameObject); // destroy duplicate item
        // } else {
        //     Debug.Log("GameManager Instance assigned");
        //     instance = this;
        //     DontDestroyOnLoad(gameObject); // persist across scenes
        // }
    }

    private void Start() {
        Lander.instance.OnCoinPickup += LanderCoinPickup;
        Lander.instance.OnLanding += LanderOnLanding;
        Lander.instance.OnStateChanged += LanderOnStateChanged;

        LoadCurrentLevel();
    }

    private void Update() {
        TickTimer();
    }

    private void LanderOnStateChanged(object sender, Lander.OnStateChangedArgs e) {
        switch (e.State) {
            case Lander.State.Ready:
                _isTimerActive = false;
                break;
            case Lander.State.Playing:
                _isTimerActive = true;
                cinemachineCamera.Target.TrackingTarget = Lander.instance.transform;
                CinemachineCameraZoom.instance.ResetOrthographicSize();
                break;
            case Lander.State.GameOver:
                _isTimerActive = false;
                break;
        }
    }

    private void TickTimer() {
        if (_isTimerActive) {
            _time += Time.deltaTime;
        }
    }

    private void LoadCurrentLevel() {
        foreach (Level level in levels) {
            if (level.GetLevelNumber() == _levelNumber) {
                // load level
                Level instantiatedLevel = Instantiate(level, Vector3.zero, Quaternion.identity);

                // set lander to starting position
                Vector3 landerStartingPosition = instantiatedLevel.GetLanderStartingPosition();
                Lander.instance.transform.position = landerStartingPosition;

                // set camera to starting position
                cinemachineCamera.Target.TrackingTarget = instantiatedLevel.GetCameraStartingPosition();
                CinemachineCameraZoom.instance.SetOrthographicSize(
                    instantiatedLevel.GetZoomedOutOrthographicSize());
            }
        }
    }

    private void LanderOnLanding(object sender, Lander.OnLandingArgs e) {
        AddScore(e.Score);
    }

    private void LanderCoinPickup(object sender, EventArgs e) {
        AddScore(100);
    }

    private void AddScore(int amount) {
        _score += amount;
    }

    public int GetScore() {
        return _score;
    }

    public float GetTime() {
        return _time;
    }

    public int GetLevelNumber() {
        return _levelNumber;
    }

    public void GoToNextLevel() {
        _levelNumber++;
        SceneManager.LoadScene(0);
    }

    public void RetryLevel() {
        SceneManager.LoadScene(0);
    }
}