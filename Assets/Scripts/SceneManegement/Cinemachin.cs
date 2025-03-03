using Cinemachine;

public class Cinemachin : Singlton<Cinemachin>
{
    private CinemachineVirtualCamera cam;

    public void SetCameraFollow()
    {
        cam = FindFirstObjectByType<CinemachineVirtualCamera>();
        cam.Follow = PlayerControl.Instanse.transform;
    }
}
