public class SceneManagment : Singlton<SceneManagment>
{
    public string TransisionName { get; private set; }

    public void SetTransisionName(string transisionName)
    {
        this.TransisionName = transisionName;
    }
}
