using UnityEngine;

public class LanderVisual : MonoBehaviour {
    [SerializeField] private ParticleSystem leftThruster;
    [SerializeField] private ParticleSystem middleThruster;
    [SerializeField] private ParticleSystem rightThruster;

    private void UpdateLeftThruster(bool state) {
        ParticleSystem.EmissionModule emission = leftThruster.emission;
        emission.enabled = state;
    }

    private void UpdateMiddleThruster(bool state) {
        ParticleSystem.EmissionModule emission = middleThruster.emission;
        emission.enabled = state;
    }

    private void UpdateRightThruster(bool state) {
        ParticleSystem.EmissionModule emission = rightThruster.emission;
        emission.enabled = state;
    }
}