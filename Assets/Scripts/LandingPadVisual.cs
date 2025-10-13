using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class LandingPadVisual : MonoBehaviour {
    [SerializeField] private TextMeshPro scoreMultiplierTextMeshPro;

    private LandingPad _landingPad;

    private void Awake() {
        Assert.IsNotNull(scoreMultiplierTextMeshPro);
        GetLandingPad();
        SetScoreMultiplierText();
    }

    private void GetLandingPad() {
        _landingPad = GetComponentInParent<LandingPad>();
        Assert.IsNotNull(_landingPad);
    }

    private void SetScoreMultiplierText() {
        int multiplier = _landingPad.getScoreMultiplier;
        string text = $"x{multiplier}";
        scoreMultiplierTextMeshPro.text = text;
    }
}