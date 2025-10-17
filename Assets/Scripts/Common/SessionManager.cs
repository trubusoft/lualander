using UnityEngine;

public class SessionManager : MonoBehaviour {
    private static int _levelNumber = 1;
    private static int _totalScore = 0;

    public static SessionManager instance { get; private set; }

    private void Awake() {
        InitializeSingleton();
    }

    public void Reset() {
        _levelNumber = 1;
    }

    private void InitializeSingleton() {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddScore(int score) {
        _totalScore += score;
    }
}