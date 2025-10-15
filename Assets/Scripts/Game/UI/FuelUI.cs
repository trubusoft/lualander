using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour {
    [SerializeField] private Image fuelBar;

    private void Update() {
        UpdateFuelBar();
    }

    private void UpdateFuelBar() {
        fuelBar.fillAmount = Lander.instance.GetFuelNormalized();
    }
}