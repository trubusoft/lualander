using TMPro;
using UnityEngine;
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
    private GameManager _gameManager;

    private Lander _lander;

    private void Awake() {
        _lander = GetComponentInParent<Lander>();
        _gameManager = GetComponentInParent<GameManager>();
    }

    private void Update() {
        UpdateStatsTextMesh();
        UpdateDirectionArrow();
        UpdateFuelBarImage();
    }

    private void UpdateStatsTextMesh() {
        int levelNumber = _gameManager.GetLevelNumber();
        int score = _gameManager.GetScore();
        float time = Mathf.Round(_gameManager.GetTime());
        float speedX = Mathf.Abs(Mathf.Round(_lander.GetSpeedX() * 10f));
        float speedY = Mathf.Abs(Mathf.Round(_lander.GetSpeedY() * 10f));
        string finalString = $"{levelNumber}\n" +
                             $"{score}\n" +
                             $"{time}\n" +
                             $"{speedX}\n" +
                             $"{speedY}";
        statsTextMeshUGUI.text = finalString;
    }

    private void UpdateDirectionArrow() {
        speedUpArrowGameObject.SetActive(_lander.GetSpeedY() > 0.01f);
        speedDownArrowGameObject.SetActive(_lander.GetSpeedY() < -0.01f);
        speedRightArrowGameObject.SetActive(_lander.GetSpeedX() > 0.01f);
        speedLeftArrowGameObject.SetActive(_lander.GetSpeedX() < -0.01f);
    }

    private void UpdateFuelBarImage() {
        fuelImage.fillAmount = _lander.GetFuelNormalized();
    }
}