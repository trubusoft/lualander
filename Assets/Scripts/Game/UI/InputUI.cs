using UnityEngine;

public class InputUI : MonoBehaviour {
    private Lander _lander;

    void Start() {
        _lander = FindAnyObjectByType<Lander>();
        _lander.OnStateChanged += LanderOnStateChanged;
    }

    private void LanderOnStateChanged(object sender, Lander.OnStateChangedArgs e) {
        switch (e.State) {
            case Lander.State.GameOver:
                gameObject.SetActive(false);
                break;
        }
    }
}