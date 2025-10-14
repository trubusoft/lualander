using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
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
    }

    private void Update() {
        _time += Time.deltaTime;
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