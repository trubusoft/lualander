using UnityEngine.SceneManagement;

public static class SceneLoader {
    public enum Scene {
        Menu,
        Game,
    }

    public static void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
}