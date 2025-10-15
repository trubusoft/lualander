using UnityEngine.SceneManagement;

public static class SceneLoader {
    public enum Scene {
        MainMenu,
        Game,
    }

    public static void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
}