using System;
using UnityEngine;
using UnityEngine.Assertions;

public class LanderVisual : MonoBehaviour {
    [SerializeField] private ParticleSystem leftThruster;
    [SerializeField] private ParticleSystem middleThruster;
    [SerializeField] private ParticleSystem rightThruster;
    [SerializeField] private GameObject landerExplosionVfx;

    private Lander _lander;

    private void Awake() {
        _lander = GetComponent<Lander>();
        Assert.IsNotNull(_lander);

        _lander.OnIdle += LanderOnOnIdle;
        _lander.OnUpForce += LanderOnUpForce;
        _lander.OnLeftForce += LanderOnLeftForce;
        _lander.OnRightForce += LanderOnRightForce;
    }

    private void Start() {
        _lander.OnLanding += LanderOnLanding;
    }

    private void LanderOnLanding(object sender, Lander.OnLandingArgs e) {
        HandleCrash(e);
    }

    private void HandleCrash(Lander.OnLandingArgs e) {
        switch (e.LandingStatus) {
            case Lander.LandingStatus.LandedOnTerrain:
            case Lander.LandingStatus.LandedTooFast:
            case Lander.LandingStatus.LandedTooSteep:
                Instantiate(landerExplosionVfx, transform.position, Quaternion.identity);
                _lander.gameObject.SetActive(false);
                break;
        }
    }

    private void LanderOnOnIdle(object sender, EventArgs e) {
        SetIdleThruster();
    }

    private void LanderOnUpForce(object sender, EventArgs e) {
        SetUpwardThruster();
    }

    private void LanderOnLeftForce(object sender, EventArgs e) {
        SetRightThruster();
    }

    private void LanderOnRightForce(object sender, EventArgs e) {
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