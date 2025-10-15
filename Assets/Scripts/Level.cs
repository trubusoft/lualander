using UnityEngine;

public class Level : MonoBehaviour {
    [SerializeField] private int levelNumber;
    [SerializeField] private Transform landerStartingPosition;
    [SerializeField] private Transform cameraStartingPosition;

    public int GetLevelNumber() {
        return levelNumber;
    }

    public Vector3 GetLanderStartingPosition() {
        return landerStartingPosition.position;
    }

    public Transform GetCameraStartingPosition() {
        return cameraStartingPosition;
    }
}