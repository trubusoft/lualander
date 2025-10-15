using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Assertions;

public class CinemachineCameraZoom : MonoBehaviour {
    private const float NormalOrthographicSize = 12f;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    private float _targetOrthographicSize;

    public static CinemachineCameraZoom instance { get; private set; }

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Assert.IsNotNull(cinemachineCamera);
    }

    private void Update() {
        cinemachineCamera.Lens.OrthographicSize = _targetOrthographicSize;
    }

    public void ResetOrthographicSize() {
        _targetOrthographicSize = NormalOrthographicSize;
    }

    public void SetOrthographicSize(float targetOrthographicSize) {
        _targetOrthographicSize = targetOrthographicSize;
    }
}