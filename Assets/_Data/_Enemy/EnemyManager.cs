using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : TGSingleton<EnemyManager>

{
    [SerializeField] protected EnemySpawner enemySpawner;
    public EnemySpawner EnemySpawner => this.enemySpawner;

    [SerializeField] protected EnemyPrefabs enemyPrefabs;
    public EnemyPrefabs EnemyPrefabs => this.enemyPrefabs;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemySpawner();
        this.LoadEnemyPrefabs();
    }

    protected virtual void LoadEnemySpawner()
    {
        if (this.enemySpawner != null) return;
        this.enemySpawner = transform.GetComponentInChildren<EnemySpawner>();
        Debug.Log(transform.name + " Load Enemy Spawner", gameObject);
    }

    protected virtual void LoadEnemyPrefabs()
    {
        if (this.enemyPrefabs != null) return;
        this.enemyPrefabs = transform.GetComponentInChildren<EnemyPrefabs>();
        Debug.Log(transform.name + " Load Enemy Prefabs", gameObject);
    }
}
