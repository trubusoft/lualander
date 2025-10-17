using System;
using UnityEngine;

public class LanderAudio : MonoBehaviour {
    [SerializeField] private AudioSource thrusterAudioSource;
    [SerializeField] private AudioClip sadTromboneAudioClip;
    [SerializeField] private AudioClip crashAudioClip;
    [SerializeField] private AudioClip landingSuccessAudioClip;

    private AudioSource _asd;

    private Lander _lander;

    private void Awake() {
        _lander = GetComponent<Lander>();
    }

    void Start() {
        _lander.OnIdle += LanderOnIdle;
        _lander.OnUpForce += LanderOnUpForce;
        _lander.OnLeftForce += LanderOnLeftForce;
        _lander.OnRightForce += LanderOnRightForce;
        _lander.OnLanding += LanderOnLanding;

        thrusterAudioSource.Pause();
    }

    private void LanderOnLanding(object sender, Lander.OnLandingArgs e) {
        switch (e.LandingStatus) {
            case Lander.LandingStatus.Success:
                AudioSource.PlayClipAtPoint(landingSuccessAudioClip, Helper.GetCameraPositionOrOrigin());
                break;
            case Lander.LandingStatus.LandedOnTerrain:
            case Lander.LandingStatus.LandedTooFast:
            case Lander.LandingStatus.LandedTooSteep:
                AudioSource.PlayClipAtPoint(crashAudioClip, Helper.GetCameraPositionOrOrigin());
                AudioSource.PlayClipAtPoint(sadTromboneAudioClip, Helper.GetCameraPositionOrOrigin());
                break;
        }
    }

    private void LanderOnRightForce(object sender, EventArgs e) {
        if (!thrusterAudioSource.isPlaying) {
            thrusterAudioSource.Play();
        }
    }

    private void LanderOnLeftForce(object sender, EventArgs e) {
        if (!thrusterAudioSource.isPlaying) {
            thrusterAudioSource.Play();
        }
    }

    private void LanderOnUpForce(object sender, EventArgs e) {
        if (!thrusterAudioSource.isPlaying) {
            thrusterAudioSource.Play();
        }
    }

    private void LanderOnIdle(object sender, EventArgs e) {
        thrusterAudioSource.Pause();
    }
}