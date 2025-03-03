using Cinemachine;

public class ScreenShakeManger : Singlton<ScreenShakeManger>
{
    private CinemachineImpulseSource impulseSource;

    protected override void Awake()
    {
        base.Awake();

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeScreen()
    {
        impulseSource.GenerateImpulse();
    }
}
