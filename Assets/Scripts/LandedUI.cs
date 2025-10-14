using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI titleTextMesh;
    [SerializeField] TextMeshProUGUI statsTextMesh;
    [SerializeField] Button nextButton;

    private void Awake() {
        // restart scene
        nextButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });
    }

    private void Start() {
        Lander.instance.OnLanding += LanderOnLanding;
        Hide();
    }

    private void LanderOnLanding(object sender, Lander.OnLandingArgs e) {
        if (e.LandingType == Lander.LandingType.Success) {
            titleTextMesh.text = "Successful Landing";
        } else if (e.LandingType == Lander.LandingType.LandedTooFast) {
            titleTextMesh.text = "Landed too Fast";
        } else if (e.LandingType == Lander.LandingType.LandedTooSteep) {
            titleTextMesh.text = "Landed too Steep";
        } else if (e.LandingType == Lander.LandingType.LandedOnTerrain) {
            titleTextMesh.text = "Crashed";
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