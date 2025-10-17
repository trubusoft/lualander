using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour {
    [SerializeField] private GameObject landerGameObject;
    private Image _fuelBarImage;
    private Lander _lander;

    private void Awake() {
        _fuelBarImage = GetComponentInChildren<Image>();
    }

    private void Start() {
        _lander = landerGameObject.GetComponent<Lander>();
    }

    private void Update() {
        UpdateFuelBar();
    }

    private void UpdateFuelBar() {
        _fuelBarImage.fillAmount = _lander.GetFuelNormalized();
    }
}