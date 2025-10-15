using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Assertions;

public class CinemachineCameraZoom : MonoBehaviour {
    private const float NormalOrthographicSize = 12f;
    private const float ZoomSpeed = 2f;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    private float _targetOrthographicSize;

    public static CinemachineCameraZoom instance { get; private set; }

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Assert.IsNotNull(cinemachineCamera);
        cinemachineCamera.Lens.OrthographicSize = NormalOrthographicSize;
    }

    private void Update() {
        cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize,
            _targetOrthographicSize, Time.deltaTime * ZoomSpeed);
    }

    public void ResetOrthographicSize() {
        _targetOrthographicSize = NormalOrthographicSize;
    }

    public void SetOrthographicSize(float targetOrthographicSize) {
        _targetOrthographicSize = targetOrthographicSize;
    }
}