using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour {
    [SerializeField] private Image fuelBarImage;
    private bool _active = true;
    private Lander _lander;

    private void Start() {
        _lander = FindAnyObjectByType<Lander>();
        _lander.OnStateChanged += LanderOnOnStateChanged;
    }

    private void Update() {
        HandleFuelBarUpdate();
    }

    private void LanderOnOnStateChanged(object sender, Lander.OnStateChangedArgs e) {
        switch (e.State) {
            case Lander.State.GameOver:
                _active = false;
                break;
        }
    }

    private void HandleFuelBarUpdate() {
        if (_active) {
            fuelBarImage.fillAmount = _lander.GetFuelNormalized();
        }
    }
}