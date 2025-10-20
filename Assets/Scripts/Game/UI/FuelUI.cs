using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour {
    [SerializeField] private Image fuelBarImage;
    private Lander _lander;

    private void Start() {
        _lander = FindAnyObjectByType<Lander>();
        _lander.OnStateChanged += LanderOnStateChanged;
    }

    private void Update() {
        HandleFuelBarUpdate();
    }

    private void LanderOnStateChanged(object sender, Lander.OnStateChangedArgs e) {
        switch (e.State) {
            case Lander.State.GameOver:
                gameObject.SetActive(false);
                break;
        }
    }

    private void HandleFuelBarUpdate() {
        fuelBarImage.fillAmount = _lander.GetFuelNormalized();
    }
}