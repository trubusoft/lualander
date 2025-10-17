using UnityEngine;

public class Helper : MonoBehaviour {
    public static Vector3 GetCameraPositionOrOrigin() {
        Camera mainCamera = Camera.main;
        if (mainCamera != null) {
            return mainCamera.transform.position;
        }

        return Vector3.zero;
    }
}