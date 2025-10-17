using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(() => { SceneLoader.LoadScene(SceneLoader.Scene.Game); });
        quitButton.onClick.AddListener(Application.Quit);
    }
}