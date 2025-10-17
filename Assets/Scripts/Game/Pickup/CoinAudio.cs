using System;
using UnityEngine;

public class CoinAudio : MonoBehaviour {
    [SerializeField] private AudioClip coinPickupAudioClip;

    private Lander _lander;

    void Start() {
        _lander = GetComponent<Lander>();
        _lander.OnCoinPickup += LanderOnCoinPickup;
    }

    private void LanderOnCoinPickup(object sender, EventArgs e) {
        AudioSource.PlayClipAtPoint(coinPickupAudioClip, Helper.GetCameraPositionOrOrigin());
    }
}