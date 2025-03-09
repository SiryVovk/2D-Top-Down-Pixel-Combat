using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject dialogWindow;

    public GameObject GetDialogWindow()
    {
        return dialogWindow;
    }
}
