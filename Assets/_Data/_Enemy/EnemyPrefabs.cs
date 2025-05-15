using UnityEngine;

public class EnemyPrefabs : PoolPrefabs<EnemyController>
{
    public virtual EnemyController GetEnemyByEnum(EnemyEnum enemyEnum)
    {
        return this.GetPrefabByName(enemyEnum.ToString());
    }
}

