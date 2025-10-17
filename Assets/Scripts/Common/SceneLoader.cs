using UnityEngine.SceneManagement;

public enum Scene {
    Menu,
    Level1,
    Level2,
    Game
}

public static class SceneLoader {
    public static void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
}