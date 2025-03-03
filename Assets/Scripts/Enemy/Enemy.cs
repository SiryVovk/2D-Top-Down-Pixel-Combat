using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyInfoSO enemyInfo;

    private void Start()
    {

    }

    public EnemyInfoSO GetEnemyInfo()
    {
        return enemyInfo;
    }
}
