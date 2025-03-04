using UnityEngine;

public class StaminaPotion : MonoBehaviour, IPotion
{
    public void UsePotion()
    {
        if (ActiveInventory.Instanse.NumberOfStaminaPotions > 0)
        {
            Stamina.Instanse.RefreshAllStamina();
            ActiveInventory.Instanse.DecreceNumberOfStaminaPotions();
        }
    }
}
