using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour {
    [SerializeField] private Image fuelBar;
    private Lander _lander;

    private void Awake() {
        _lander = GetComponentInParent<Lander>();
    }

    private void Update() {
        UpdateFuelBar();
    }

    private void UpdateFuelBar() {
        fuelBar.fillAmount = _lander.GetFuelNormalized();
    }
}