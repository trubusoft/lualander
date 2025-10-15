using UnityEngine;

public class Input : MonoBehaviour {
    private InputActions _inputActions;
    public static Input instance { get; private set; }

    private void Awake() {
        instance = this;
        _inputActions = new InputActions();
    }

    public bool IsLanderUp() {
        return _inputActions.Lander.Up.IsPressed();
    }

    public bool IsLanderLeft() {
        return _inputActions.Lander.Left.IsPressed();
    }

    public bool IsLanderRight() {
        return _inputActions.Lander.Right.IsPressed();
    }

    public void EnableInputAction() {
        _inputActions.Enable();
    }

    public void DisableInputAction() {
        _inputActions.Disable();
    }
}