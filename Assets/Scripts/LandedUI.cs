using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI titleTextMesh;
    [SerializeField] TextMeshProUGUI statsTextMesh;
    [SerializeField] TextMeshProUGUI nextButtonTextMesh;
    [SerializeField] Button nextButton;

    private Action _nextButtonAction;

    private void Awake() {
        // restart scene
        nextButton.onClick.AddListener(() => { _nextButtonAction(); });
    }

    private void Start() {
        Lander.instance.OnLanding += LanderOnLanding;
        Hide();
    }

    private void LanderOnLanding(object sender, Lander.OnLandingArgs e) {
        switch (e.LandingType) {
            case Lander.LandingType.Success:
                titleTextMesh.text = "Successful Landing";
                nextButtonTextMesh.text = "Next Level";
                _nextButtonAction = GameManager.instance.GoToNextLevel;
                break;
            case Lander.LandingType.LandedTooFast:
                titleTextMesh.text = "Landed too Fast";
                nextButtonTextMesh.text = "Retry";
                _nextButtonAction = GameManager.instance.Retrylevel;
                break;
            case Lander.LandingType.LandedTooSteep:
                titleTextMesh.text = "Landed too Steep";
                nextButtonTextMesh.text = "Retry";
                _nextButtonAction = GameManager.instance.Retrylevel;
                break;
            case Lander.LandingType.LandedOnTerrain:
                titleTextMesh.text = "Crashed";
                nextButtonTextMesh.text = "Retry";
                _nextButtonAction = GameManager.instance.Retrylevel;
                break;
        }

        var landingSpeed = Mathf.Round(e.LandingSpeed * 2f);
        var landingAngle = Mathf.Round(e.LandingAngle * 100f);

        statsTextMesh.text = $"{landingSpeed}\n" +
                             $"{landingAngle}\n" +
                             $"x{e.ScoreMultiplier}\n" +
                             $"{e.Score}";
        Show();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }
}