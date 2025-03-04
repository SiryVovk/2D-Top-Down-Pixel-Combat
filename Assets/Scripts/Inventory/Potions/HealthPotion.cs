using UnityEngine;

public class HealthPotion : MonoBehaviour, IPotion
{
    private int healthRecovery = 3;

    public void UsePotion()
    {
        if (ActiveInventory.Instanse.NumberOfHealthPotions > 0)
        {
            PlayerHelth.Instanse.HealPlayer(healthRecovery);
            ActiveInventory.Instanse.DecreceNumberOfHealthPotions();
        }
    }
}
