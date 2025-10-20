using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour {
    private bool _active;
    private Image _fuelBarImage;
    private Lander _lander;

    private void Awake() {
        _fuelBarImage = GetComponentInChildren<Image>();
        _active = false;
    }

    private void Start() {
        _lander = FindAnyObjectByType<Lander>();

        _lander.OnStateChanged += LanderOnOnStateChanged;
    }

    private void Update() {
        UpdateFuelBar();
    }

    private void LanderOnOnStateChanged(object sender, Lander.OnStateChangedArgs e) {
        switch (e.State) {
            case Lander.State.Playing:
                _active = true;
                break;
            default:
                _active = false;
                break;
        }
    }

    private void UpdateFuelBar() {
        if (_active) {
            _fuelBarImage.fillAmount = _lander.GetFuelNormalized();
        }
    }
}