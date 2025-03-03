using UnityEngine;

public class Singlton<T> : MonoBehaviour where T : Singlton<T>
{
    private static T instance;
    public static T Instanse { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = (T)this;

        if (!gameObject.transform.parent && instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
