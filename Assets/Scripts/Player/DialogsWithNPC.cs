using UnityEngine;

public class DialogsWithNPC : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    private float speakRadius = 2f;

    private PlayerControls playerControls;

    private const string NPC_TAG_STRING = "NPC";

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Dialogue.Talk.performed += _ =>
        {
            TalkWithNPC();
        };
    }

    private void TalkWithNPC()
    {
        Collider2D[] collidersInRadius = Physics2D.OverlapCircleAll(transform.position, speakRadius);
        Collider2D closestNPC = null;
        float closestDistance = float.MaxValue;

        foreach (Collider2D col in collidersInRadius)
        {
            if (col.CompareTag(NPC_TAG_STRING))
            {
                float distance = Vector2.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestNPC = col;
                }
            }
        }

        if (closestNPC != null)
        {
            NPC npc = closestNPC.GetComponent<NPC>();

            if (npc == null)
            {
                return;
            }

            GameObject dialogWindow = npc.GetDialogWindow();

            if (dialogWindow == null)
            {
                return;
            }

            Instantiate(dialogWindow, canvas.transform);
        }
    }
}
