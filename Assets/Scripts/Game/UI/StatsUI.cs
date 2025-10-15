using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI statsTextMeshUGUI;

    [FormerlySerializedAs("leftArrowGameObject")] [SerializeField]
    private GameObject speedLeftArrowGameObject;

    [FormerlySerializedAs("rightArrowGameObject")] [SerializeField]
    private GameObject speedRightArrowGameObject;

    [FormerlySerializedAs("upArrowGameObject")] [SerializeField]
    private GameObject speedUpArrowGameObject;

    [FormerlySerializedAs("downArrowGameObject")] [SerializeField]
    private GameObject speedDownArrowGameObject;

    [SerializeField] private Image fuelImage;

    private void Awake() {
        Assert.IsNotNull(statsTextMeshUGUI);
        Assert.IsNotNull(speedLeftArrowGameObject);
        Assert.IsNotNull(speedRightArrowGameObject);
        Assert.IsNotNull(speedUpArrowGameObject);
        Assert.IsNotNull(speedDownArrowGameObject);
        Assert.IsNotNull(fuelImage);
    }

    private void Update() {
        UpdateStatsTextMesh();
        UpdateDirectionArrow();
        UpdateFuelBarImage();
    }

    private void UpdateStatsTextMesh() {
        int levelNumber = GameManager.instance.GetLevelNumber();
        int score = GameManager.instance.GetScore();
        float time = Mathf.Round(GameManager.instance.GetTime());
        float speedX = Mathf.Abs(Mathf.Round(Lander.instance.GetSpeedX() * 10f));
        float speedY = Mathf.Abs(Mathf.Round(Lander.instance.GetSpeedY() * 10f));
        string finalString = $"{levelNumber}\n" +
                             $"{score}\n" +
                             $"{time}\n" +
                             $"{speedX}\n" +
                             $"{speedY}";
        statsTextMeshUGUI.text = finalString;
    }

    private void UpdateDirectionArrow() {
        speedUpArrowGameObject.SetActive(Lander.instance.GetSpeedY() > 0.01f);
        speedDownArrowGameObject.SetActive(Lander.instance.GetSpeedY() < -0.01f);
        speedRightArrowGameObject.SetActive(Lander.instance.GetSpeedX() > 0.01f);
        speedLeftArrowGameObject.SetActive(Lander.instance.GetSpeedX() < -0.01f);
    }

    private void UpdateFuelBarImage() {
        fuelImage.fillAmount = Lander.instance.GetFuelNormalized();
    }
}