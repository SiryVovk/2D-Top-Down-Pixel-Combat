using UnityEngine;

public class UIShop : MonoBehaviour
{
    private int staminaCost = 2;
    private int healthCost = 5;
    public void ExitShop()
    {
        Destroy(gameObject);
    }

    public void BuyHealthPotion()
    {
        if (EconomyManager.Instanse.CurrentMoney - healthCost >= 0)
        {
            EconomyManager.Instanse.GoldPayment(healthCost);
            ActiveInventory.Instanse.IncreseNumberOfHealthPotions();
        }
    }

    public void BuyStaminaPotion()
    {
        if (EconomyManager.Instanse.CurrentMoney - staminaCost >= 0)
        {
            EconomyManager.Instanse.GoldPayment(staminaCost);
            ActiveInventory.Instanse.IncreseNumberOfStaminaPotions();
        }
    }
}
