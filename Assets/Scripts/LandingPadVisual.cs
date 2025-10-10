using TMPro;
using UnityEngine;

public class LandingPadVisual : MonoBehaviour {
    [SerializeField] private TextMeshPro scoreMultiplierTextMesh;

    private void Awake() {
        UpdateScoreMultiplierText();
    }

    private void UpdateScoreMultiplierText() {
        LandingPad landingPad = gameObject.GetComponent<LandingPad>();
        scoreMultiplierTextMesh.text = "x" + landingPad.getScoreMultiplier;
    }
}