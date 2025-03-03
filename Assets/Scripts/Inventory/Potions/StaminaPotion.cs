using UnityEngine;

public class StaminaPotion : MonoBehaviour, IPotion
{
    public void UsePotion()
    {
        Stamina.Instanse.RefreshStamina();
    }
}
