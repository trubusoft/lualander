using UnityEngine;

public class LanderInput : MonoBehaviour {
    private LanderInputActions _landerInputActions;

    private void Awake() {
        _landerInputActions = new LanderInputActions();
    }

    public bool IsThrusting() {
        return _landerInputActions.Lander.Up.IsPressed();
    }

    public bool IsRotatingLeft() {
        return _landerInputActions.Lander.Left.IsPressed();
    }

    public bool IsRotatingRight() {
        return _landerInputActions.Lander.Right.IsPressed();
    }

    public void EnableInput() {
        _landerInputActions.Enable();
    }

    public void DisableInput() {
        _landerInputActions.Disable();
    }
}