using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI titleTextMesh;
    [SerializeField] TextMeshProUGUI statsTextMesh;
    [SerializeField] TextMeshProUGUI nextButtonTextMesh;
    [SerializeField] Button nextButton;

    private Lander _lander;
    private LevelManager _levelManager;

    private Action _nextButtonAction;

    public void SetUp(LevelManager levelManager, Lander lander) {
        nextButton.onClick.AddListener(() => { _nextButtonAction(); });

        _levelManager = levelManager;
        _lander = lander;
        _lander.OnLanding += LanderOnLanding;
    }

    private void LanderOnLanding(object sender, Lander.OnLandingArgs e) {
        switch (e.LandingStatus) {
            case Lander.LandingStatus.Success:
                titleTextMesh.text = "Successful Landing";
                nextButtonTextMesh.text = "Next Level";
                _nextButtonAction = _levelManager.GoToNextLevel;
                break;
            case Lander.LandingStatus.LandedTooFast:
                titleTextMesh.text = "Landed too Fast";
                nextButtonTextMesh.text = "Retry";
                _nextButtonAction = _levelManager.RetryLevel;
                break;
            case Lander.LandingStatus.LandedTooSteep:
                titleTextMesh.text = "Landed too Steep";
                nextButtonTextMesh.text = "Retry";
                _nextButtonAction = _levelManager.RetryLevel;
                break;
            case Lander.LandingStatus.LandedOnTerrain:
                titleTextMesh.text = "Crashed";
                nextButtonTextMesh.text = "Retry";
                _nextButtonAction = _levelManager.RetryLevel;
                break;
        }

        var landingSpeed = Mathf.Round(e.LandingSpeed * 2f);
        var landingAngle = Mathf.Round(e.LandingAngle * 100f);

        statsTextMesh.text = $"{landingSpeed}\n" +
                             $"{landingAngle}\n" +
                             $"x{e.ScoreMultiplier}\n" +
                             $"{e.Score}";

        // show UI
        gameObject.SetActive(true);
    }
}