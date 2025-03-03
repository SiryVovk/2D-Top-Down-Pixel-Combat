using UnityEngine;

public class LevlEnterence : MonoBehaviour
{
    [SerializeField] private string exitSide;

    private void Start()
    {
        if (SceneManagment.Instanse.TransisionName == exitSide)
        {
            PlayerControl.Instanse.transform.position = gameObject.transform.position;
            Cinemachin.Instanse.SetCameraFollow();
            UIFade.Instanse.FadeToClear();
        }
    }
}
