using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField] private AudioClip crashAudioClip;
    [SerializeField] private AudioClip sadTromboneAudioClip;

    [SerializeField] private AudioClip fuelPickupAudioClip;
    [SerializeField] private AudioClip coinPickupAudioClip;

    void Start() {
        Lander.instance.OnLanding += LanderOnLanding;
        Lander.instance.OnCoinPickup += LanderOnCoinPickup;
        Lander.instance.OnFuelPickup += LanderOnFuelPickup;
    }

    private Vector3 GetPlayPoint() {
        Camera mainCamera = Camera.main;
        if (mainCamera != null) {
            return mainCamera.transform.position;
        }

        return Vector3.zero;
    }

    private void LanderOnCoinPickup(object sender, EventArgs e) {
        AudioSource.PlayClipAtPoint(coinPickupAudioClip, GetPlayPoint());
    }

    private void LanderOnFuelPickup(object sender, EventArgs e) {
        AudioSource.PlayClipAtPoint(fuelPickupAudioClip, GetPlayPoint());
    }

    private void LanderOnLanding(object sender, Lander.OnLandingArgs e) {
        switch (e.LandingType) {
            case Lander.LandingType.LandedOnTerrain:
            case Lander.LandingType.LandedTooFast:
            case Lander.LandingType.LandedTooSteep:
                AudioSource.PlayClipAtPoint(crashAudioClip, GetPlayPoint());
                AudioSource.PlayClipAtPoint(sadTromboneAudioClip, GetPlayPoint());
                break;
        }
    }
}