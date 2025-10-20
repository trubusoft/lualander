using UnityEngine;

/// <summary>
/// <para>
/// This class plays the background music inter-scene.
/// </para>
/// <para>
/// Current played time is saved as a static <see cref="_musicTime"/>
/// and will persist across scene, so music will continue. 
/// </para>
/// </summary>
public class MusicPlayer : MonoBehaviour {
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