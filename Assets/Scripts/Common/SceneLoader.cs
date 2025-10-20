using UnityEngine.SceneManagement;

public enum Scene {
    Menu,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
    Fin,
}

public static class SceneLoader {
    public static void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
}