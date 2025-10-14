using UnityEngine;

public class Level : MonoBehaviour {
    [SerializeField] private int levelNumber;
    [SerializeField] private Transform landerStartingPosition;

    public int GetLevelNumber() {
        return levelNumber;
    }

    public Vector3 GetLanderStartingPosition() {
        return landerStartingPosition.position;
    }
}