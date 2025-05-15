using UnityEngine;

public class EnemySpawner : Spawner<EnemyController>
{
    public int enemySpawnCount => this.spawnCount;

}
