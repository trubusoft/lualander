using System;
using UnityEngine;
using UnityEngine.Assertions;

public class CoinAudio : MonoBehaviour {
    [SerializeField] private AudioClip coinPickupAudioClip;

    private Lander _lander;

    void Start() {
        _lander = FindAnyObjectByType<Lander>();
        Assert.IsNotNull(_lander);

        _lander.OnCoinPickup += LanderOnCoinPickup;
    }

    private void LanderOnCoinPickup(object sender, EventArgs e) {
        AudioSource.PlayClipAtPoint(coinPickupAudioClip, Helper.GetCameraPositionOrOrigin());
    }
}