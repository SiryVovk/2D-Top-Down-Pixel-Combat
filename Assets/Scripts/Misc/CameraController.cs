using Cinemachine;
public class CameraController : Singlton<CameraController>
{
    private CinemachineVirtualCamera cam;

    private void Start()
    {
        SetPlayerCameraFollow();
    }

    private void SetPlayerCameraFollow()
    {
        cam = FindFirstObjectByType<CinemachineVirtualCamera>();
        cam.Follow = PlayerControl.Instanse.transform;
    }
}
