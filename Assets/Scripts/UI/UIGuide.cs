using UnityEngine;

public class UIGuide : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public void HideGuide()
    {
        player.SetActive(true);

        Destroy(gameObject);
    }
}
