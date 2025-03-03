using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevlExit : MonoBehaviour
{
    [SerializeField] private string levlToLoad;
    [SerializeField] private string exitSide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>())
        {
            UIFade.Instanse.FadeToBlack();
            SceneManagment.Instanse.SetTransisionName(exitSide);
            StartCoroutine(LoadSceneRoutin());
        }
    }

    private IEnumerator LoadSceneRoutin()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(levlToLoad);
    }
}
