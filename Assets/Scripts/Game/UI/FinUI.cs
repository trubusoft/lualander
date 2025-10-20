using UnityEngine;
using UnityEngine.UI;

public class FinUI : MonoBehaviour {
    private Button _playButton;

    private void Awake() {
        _playButton = GetComponentInChildren<Button>();
        _playButton.onClick.AddListener(() => { SceneLoader.LoadScene(Scene.Menu); });
    }
}