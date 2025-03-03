using UnityEngine;

public class SwingAnimation : MonoBehaviour
{
    private void OnFinishDestroy()
    {
        Destroy(gameObject);
    }
}
