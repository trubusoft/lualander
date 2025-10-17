using System;
using UnityEngine;

public class FuelAudio : MonoBehaviour {
    [SerializeField] private AudioClip fuelPickupAudioClip;

    private Lander _lander;

    void Start() {
        _lander = GetComponent<Lander>();
        _lander.OnFuelPickup += LanderOnFuelPickup;
    }

    private void LanderOnFuelPickup(object sender, EventArgs e) {
        AudioSource.PlayClipAtPoint(fuelPickupAudioClip, Helper.GetCameraPositionOrOrigin());
    }
}