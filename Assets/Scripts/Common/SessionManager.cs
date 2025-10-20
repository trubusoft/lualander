using UnityEngine;

public class SessionManager : MonoBehaviour {
    private static int _totalScore;

    public static SessionManager instance { get; private set; }

    private void Awake() {
        InitializeSingleton();
    }

    private void InitializeSingleton() {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetTotalScore() {
        return _totalScore;
    }

    public void AddTotalScore(int score) {
        _totalScore += score;
    }
}