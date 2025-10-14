using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private int levelNumber;
    [SerializeField] private List<Level> levels;

    private int _score;
    private float _time;
    public static GameManager instance { get; private set; }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject); // destroy duplicate item
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject); // persist across scenes
        }
    }

    private void Start() {
        Lander.instance.OnCoinPickup += LanderCoinPickup;
        Lander.instance.OnLanding += LanderOnLanding;

        LoadCurrentLevel();
    }

    private void Update() {
        _time += Time.deltaTime;
    }

    private void LoadCurrentLevel() {
        foreach (Level level in levels) {
            if (level.GetLevelNumber() == levelNumber) {
                // load level
                Level instantiatedLevel = Instantiate(level, Vector3.zero, Quaternion.identity);

                // set lander to starting position
                Vector3 landerStartingPosition = instantiatedLevel.GetLanderStartingPosition();
                Lander.instance.transform.position = landerStartingPosition;
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
}