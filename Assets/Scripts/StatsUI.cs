using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class StatsUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI statsTextMeshUGUI;
    [SerializeField] private GameObject leftArrowGameObject;
    [SerializeField] private GameObject rightArrowGameObject;
    [SerializeField] private GameObject upArrowGameObject;
    [SerializeField] private GameObject downArrowGameObject;

    private void Awake() {
        Assert.IsNotNull(statsTextMeshUGUI);
        Assert.IsNotNull(leftArrowGameObject);
        Assert.IsNotNull(rightArrowGameObject);
        Assert.IsNotNull(upArrowGameObject);
        Assert.IsNotNull(downArrowGameObject);
    }

    private void Update() {
        UpdateStatsTextMesh();
        UpdateDirectionArrow();
    }

    private void UpdateStatsTextMesh() {
        int score = GameManager.instance.GetScore();
        float time = Mathf.Round(GameManager.instance.GetTime());
        float speedX = Mathf.Abs(Mathf.Round(Lander.instance.GetSpeedX() * 10f));
        float speedY = Mathf.Abs(Mathf.Round(Lander.instance.GetSpeedY() * 10f));
        string fuel = Lander.instance.GetFuel().ToString("0.00");
        string finalString = $"{score}\n" +
                             $"{time}\n" +
                             $"{speedX}\n" +
                             $"{speedY}\n" +
                             $"{fuel}";
        statsTextMeshUGUI.text = finalString;
    }

    private void UpdateDirectionArrow() {
        upArrowGameObject.SetActive(Lander.instance.GetSpeedY() > 0.5f);
        downArrowGameObject.SetActive(Lander.instance.GetSpeedY() < -0.5f);
        rightArrowGameObject.SetActive(Lander.instance.GetSpeedX() > 0.5f);
        leftArrowGameObject.SetActive(Lander.instance.GetSpeedX() < -0.5f);
    }
}