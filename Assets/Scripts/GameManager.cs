using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private int _score;

    private void Start() {
        Lander.instance.OnCoinPickup += LanderCoinPickup;
        Lander.instance.OnLanding += LanderOnLanding;
    }

    private void LanderOnLanding(object sender, Lander.OnLandingArgs e) {
        AddScore(e.Score);
    }

    private void LanderCoinPickup(object sender, EventArgs e) {
        AddScore(100);
    }

    private void AddScore(int amount) {
        _score += amount;
        Debug.Log("Score: " + _score);
    }
}