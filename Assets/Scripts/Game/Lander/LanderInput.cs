using UnityEngine;

public class LanderInput : MonoBehaviour {
    private LanderInputActions _landerInputActions;

    private void Awake() {
        _landerInputActions = new LanderInputActions();
    }

    public bool IsLanderUp() {
        return _landerInputActions.Lander.Up.IsPressed();
    }

    public bool IsLanderLeft() {
        return _landerInputActions.Lander.Left.IsPressed();
    }

    public bool IsLanderRight() {
        return _landerInputActions.Lander.Right.IsPressed();
    }

    public void EnableInputAction() {
        _landerInputActions.Enable();
    }

    public void DisableInputAction() {
        _landerInputActions.Disable();
    }
}