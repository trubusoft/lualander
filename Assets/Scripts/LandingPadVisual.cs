using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class LandingPadVisual : MonoBehaviour {
    [SerializeField] private TextMeshPro scoreMultiplierTextMesh;

    private void Awake() {
        Assert.IsNotNull(scoreMultiplierTextMesh);
        UpdateScoreMultiplierText();
    }

    private void UpdateScoreMultiplierText() {
        LandingPad landingPad = gameObject.GetComponent<LandingPad>();
        scoreMultiplierTextMesh.text = "x" + landingPad.getScoreMultiplier;
    }
}