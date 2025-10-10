using TMPro;
using UnityEngine;

public class LandingPadVisual : MonoBehaviour {
    [SerializeField] private TextMeshPro scoreMultiplierTextMesh;

    private void Awake() {
        UpdateMultiplierText();
    }

    private void UpdateMultiplierText() {
        LandingPad landingPad = gameObject.GetComponent<LandingPad>();
        scoreMultiplierTextMesh.text = "x" + landingPad.getScoreMultiplier;
    }
}