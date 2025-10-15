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
        // todo
        // Lander.instance.OnFuelPickup += LanderOnFuelPickup();
    }

    private void LanderOnCoinPickup(object sender, EventArgs e) {
        AudioSource.PlayClipAtPoint(coinPickupAudioClip, Camera.main.transform.position);
    }

    // todo
    // private void LanderOnFuelPickup(object sender, EventArgs e) {
    //     AudioSource.PlayClipAtPoint(fuelPickupAudioClip, Camera.main.transform.position);
    // }

    private void LanderOnLanding(object sender, Lander.OnLandingArgs e) {
        switch (e.LandingType) {
            case Lander.LandingType.LandedOnTerrain:
            case Lander.LandingType.LandedTooFast:
            case Lander.LandingType.LandedTooSteep:
                Camera mainCamera = Camera.main;
                if (mainCamera == null) {
                    break;
                }

                AudioSource.PlayClipAtPoint(crashAudioClip, mainCamera.transform.position);
                AudioSource.PlayClipAtPoint(sadTromboneAudioClip, mainCamera.transform.position);
                break;
        }
    }
}