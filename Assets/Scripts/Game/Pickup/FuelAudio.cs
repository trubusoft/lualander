using System;
using UnityEngine;
using UnityEngine.Assertions;

public class FuelAudio : MonoBehaviour {
    [SerializeField] private AudioClip fuelPickupAudioClip;

    private Lander _lander;

    void Start() {
        _lander = FindAnyObjectByType<Lander>();
        Assert.IsNotNull(_lander);

        _lander.OnFuelPickup += LanderOnFuelPickup;
    }

    private void LanderOnFuelPickup(object sender, EventArgs e) {
        AudioSource.PlayClipAtPoint(fuelPickupAudioClip, Helper.GetCameraPositionOrOrigin());
    }
}