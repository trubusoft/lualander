using Unity.Cinemachine;
using UnityEngine;

public class CinemachineZoom : MonoBehaviour {
    private const float NormalOrthographicSize = 12f;
    private const float ZoomSpeed = 1f;

    [SerializeField] private GameObject landerGameObject;
    private CinemachineCamera _cinemachineCamera;

    private Lander _lander;

    private void Awake() {
        _cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    private void Start() {
        _lander = landerGameObject.GetComponent<Lander>();

        _lander.OnStateChanged += LanderOnStateChanged;
    }

    private void Update() {
        HandleSmoothZoom();
    }

    private void LanderOnStateChanged(object sender, Lander.OnStateChangedArgs e) {
        if (e.State == Lander.State.Playing) {
            StartTrackingLander();
        }
    }

    private void StartTrackingLander() {
        _cinemachineCamera.Follow = _lander.transform;
        _cinemachineCamera.LookAt = _lander.transform;
    }

    private void HandleSmoothZoom() {
        bool cameraHasTarget = _cinemachineCamera.Target.TrackingTarget != null;
        bool zoomMatched = Helper.AlmostEqual(_cinemachineCamera.Lens.OrthographicSize, NormalOrthographicSize);
        bool isZoomNeeded = cameraHasTarget && !zoomMatched;

        if (isZoomNeeded) {
            _cinemachineCamera.Lens.OrthographicSize =
                Mathf.Lerp(
                    _cinemachineCamera.Lens.OrthographicSize,
                    NormalOrthographicSize,
                    Time.deltaTime * ZoomSpeed)
                ;
        }
    }
}