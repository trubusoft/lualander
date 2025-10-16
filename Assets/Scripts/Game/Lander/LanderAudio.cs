using System;
using UnityEngine;

public class LanderAudio : MonoBehaviour {
    [SerializeField] private AudioSource thrusterAudioSource;
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

        thrusterAudioSource.Pause();
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