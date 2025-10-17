using UnityEngine.SceneManagement;

public static class SceneLoader {
    public enum Scene {
        Menu,
        Level1,
        Level2,
        Game
    }

    public static void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
}