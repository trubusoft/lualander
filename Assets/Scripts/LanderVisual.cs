using System;
using UnityEngine;
using UnityEngine.Assertions;

public class LanderVisual : MonoBehaviour {
    [SerializeField] private ParticleSystem leftThruster;
    [SerializeField] private ParticleSystem middleThruster;
    [SerializeField] private ParticleSystem rightThruster;

    private Lander _lander;

    private void Awake() {
        _lander = GetComponent<Lander>();
        Assert.IsNotNull(_lander);

        _lander.OnIdle += LanderOnOnIdle;
        _lander.OnUpForce += LanderOnOnUpForce;
        _lander.OnLeftForce += LanderOnOnLeftForce;
        _lander.OnRightForce += LanderOnOnRightForce;
    }

    private void LanderOnOnIdle(object sender, EventArgs e) {
        SetIdleThruster();
    }

    private void LanderOnOnUpForce(object sender, EventArgs e) {
        SetUpwardThruster();
    }

    private void LanderOnOnLeftForce(object sender, EventArgs e) {
        SetRightThruster();
    }

    private void LanderOnOnRightForce(object sender, EventArgs e) {
        SetLeftThruster();
    }

    private void SetIdleThruster() {
        SetThruster(leftThruster, false);
        SetThruster(middleThruster, false);
        SetThruster(rightThruster, false);
    }

    private void SetUpwardThruster() {
        SetThruster(leftThruster, true);
        SetThruster(middleThruster, true);
        SetThruster(rightThruster, true);
    }

    private void SetLeftThruster() {
        SetThruster(leftThruster, true);
    }

    private void SetRightThruster() {
        SetThruster(rightThruster, true);
    }

    private static void SetThruster(ParticleSystem particleSystem, bool state) {
        ParticleSystem.EmissionModule emission = particleSystem.emission;
        emission.enabled = state;
    }
}