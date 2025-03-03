using UnityEngine;

public class InventarySlot : MonoBehaviour
{
    [SerializeField] private WeponInfoSO weponInfoSO;
    [SerializeField] private PotionInfoSO potionInfoSO;

    public WeponInfoSO GetWeaponInfo()
    {
        return weponInfoSO;
    }

    public PotionInfoSO GetPotionInfo()
    {
        return potionInfoSO;
    }
}
