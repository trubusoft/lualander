using UnityEngine;

public class MusicManager : MonoBehaviour {
    private static float _musicTime;
    private AudioSource _musicAudioSource;

    private void Awake() {
        _musicAudioSource = GetComponent<AudioSource>();
        _musicAudioSource.time = _musicTime;
    }

    private void Update() {
        _musicTime = _musicAudioSource.time;
    }
}