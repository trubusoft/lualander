using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField] private AudioClip crashAudioClip;
    [SerializeField] private AudioClip sadTromboneAudioClip;

    void Start() {
        Lander.instance.OnLanding += LanderOnLanding;
    }

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