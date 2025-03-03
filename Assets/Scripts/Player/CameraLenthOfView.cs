using Cinemachine;
using UnityEngine;

public class CameraLenthOfView : MonoBehaviour
{
    [SerializeField] private float minLenthOfView = 5f;
    [SerializeField] private float maxLenthOfView = 10f;
    [SerializeField] private float zoomSpeed = 2f;

    private PlayerControls playerControls;
    private CinemachineVirtualCamera cam;

    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        float zoomValu = playerControls.Camera.Zoom.ReadValue<float>();

        float newDistance = cam.m_Lens.OrthographicSize - zoomValu * zoomSpeed;
        cam.m_Lens.OrthographicSize = Mathf.Clamp(newDistance, minLenthOfView, maxLenthOfView);

    }
}
