using UnityEngine;

[CreateAssetMenu(fileName = "WeponInfoSO", menuName = "Scriptable Objects/WeponInfoSO")]
public class WeponInfoSO : ScriptableObject
{
    public GameObject weaponPrefab;
    public float attackRate;
    public int damage;
    public float range;
}
