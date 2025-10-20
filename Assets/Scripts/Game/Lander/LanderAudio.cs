using System;
using UnityEngine;

public class LanderAudio : MonoBehaviour {
    [SerializeField] private AudioSource thrusterAudioSource;
    [SerializeField] private AudioClip sadTromboneAudioClip;
    [SerializeField] private AudioClip crashAudioClip;
    [SerializeField] private AudioClip landingSuccessAudioClip;

    private Lander _lander;

    private void Awake() {
        _lander = GetComponent<Lander>();
        PauseThrusterSound();
    }

    void Start() {
        _lander.OnIdle += LanderOnIdle;
        _lander.OnUpForce += LanderOnMove;
        _lander.OnLeftForce += LanderOnMove;
        _lander.OnRightForce += LanderOnMove;
        _lander.OnLanding += LanderOnLanding;
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

    private void PlayThrusterSound() {
        if (!thrusterAudioSource.isPlaying) {
            thrusterAudioSource.Play();
        }
    }

    private void PauseThrusterSound() {
        thrusterAudioSource.Pause();
    }

    private void LanderOnMove(object sender, EventArgs e) {
        PlayThrusterSound();
    }

    private void LanderOnIdle(object sender, EventArgs e) {
        PauseThrusterSound();
    }
}