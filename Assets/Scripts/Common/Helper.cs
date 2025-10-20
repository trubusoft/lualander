using UnityEngine;

public class Helper : MonoBehaviour {
    public static Vector3 GetCameraPositionOrOrigin() {
        Camera mainCamera = Camera.main;
        if (mainCamera != null) {
            return mainCamera.transform.position;
        }

        return Vector3.zero;
    }

    /// <summary>
    /// Compares two floating-point numbers for approximate equality.
    /// </summary>
    /// <param name="a">The first floating-point number.</param>
    /// <param name="b">The second floating-point number.</param>
    /// <param name="epsilon">The maximum allowed absolute difference for equality.</param>
    /// <returns>True if the numbers are considered almost equal, False otherwise.</returns>
    public static bool AlmostEqual(float a, float b, float epsilon = 0.1f) {
        return Mathf.Abs(a - b) < epsilon;
    }
}