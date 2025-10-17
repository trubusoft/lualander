using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private SceneLoader.Scene firstLevel;

    private void Awake() {
        playButton.onClick.AddListener(() => { SceneLoader.LoadScene(firstLevel); });
        quitButton.onClick.AddListener(Application.Quit);
    }

    private void Start() {
        SessionManager.instance.Reset();
    }
}