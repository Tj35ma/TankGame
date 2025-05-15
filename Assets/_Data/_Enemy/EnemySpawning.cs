using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : EnemyManagerAbstract
{
    [SerializeField] protected EnemySpawnPoints enemySpawnPoints;
    [SerializeField] protected float spawnSpeed = 1f;
    [SerializeField] protected int maxSpawn = 5;
    protected List<EnemyController> spawnedEnemy = new();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemySpawnPoints();
    }

    protected virtual void LoadEnemySpawnPoints()
    {
        if (this.enemySpawnPoints != null) return;
        this.enemySpawnPoints = transform.GetComponent<EnemySpawnPoints>();
        Debug.Log(transform.name + ": LoadEnemySpawnPoints", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        Invoke(nameof(this.Spawning), this.spawnSpeed);
    }

    protected virtual void FixedUpdate()
    {
        this.RemoveDeadOne();

    }

    protected virtual void Spawning()
    {
        Invoke(nameof(this.Spawning), this.spawnSpeed);
        if (this.enemyManager.EnemySpawner.enemySpawnCount >= this.maxSpawn) return;

        EnemyController prefab = this.enemyManager.EnemyPrefabs.GetRandom();
        Transform posSpawn = this.enemySpawnPoints.GetRandom();
        EnemyController newEnemy = this.enemyManager.EnemySpawner.Spawn(prefab, posSpawn.position);
        newEnemy.gameObject.SetActive(true);

        this.spawnedEnemy.Add(newEnemy);
    }

    protected virtual void RemoveDeadOne()
    {
        foreach (EnemyController enemyCtrl in this.spawnedEnemy)
        {
            if (enemyCtrl.EnemyDamageReceiver.IsDead())
            {
                this.spawnedEnemy.Remove(enemyCtrl);
                return;
            }
        }
    }
}
