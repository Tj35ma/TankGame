using UnityEngine;

public class ItemDropManager : TGSingleton<ItemDropManager>
{
    [SerializeField] protected ItemDropSpawner itemDropSpawner;
    [SerializeField] protected ItemDropPrefabs itemDropPrefabs;

    protected float spawnHeight = 1.0f;
    protected float forceAmount = 5.0f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemDropSpawner();
        this.LoadItemDropPrefabs();
    }

    protected virtual void LoadItemDropSpawner()
    {
        if (this.itemDropSpawner != null) return;
        this.itemDropSpawner = GetComponentInChildren<ItemDropSpawner>();        
        Debug.Log(transform.name + ": LoadItemDropSpawner", gameObject);
    }

    protected virtual void LoadItemDropPrefabs()
    {
        if (this.itemDropPrefabs != null) return;
        this.itemDropPrefabs = GetComponentInChildren<ItemDropPrefabs>();
        Debug.Log(transform.name + ": LoadItemDropPrefabs", gameObject);
    }

    public virtual void DropMany(ItemEnum itemEnum, int dropCount, Vector3 dropPosition)
    {
        for (int i = 0; i < dropCount; i++)
        {
            this.Drop( itemEnum, 1, dropPosition);
        }
    }

    public virtual void Drop(ItemEnum itemEnum, int dropCount, Vector3 dropPos)
    {
        Vector3 spawnPosition = dropPos + new Vector3(Random.Range(-0.5f, 0.5f), spawnHeight, Random.Range(-0.5f, 0.5f));
        ItemDropController itemPrefab = this.itemDropPrefabs.GetPrefabByName(itemEnum.ToString());
        if (itemPrefab == null) itemPrefab = this.itemDropPrefabs.GetPrefabByName("DefaultDrop");

        ItemDropController newItem = this.itemDropSpawner.Spawn(itemPrefab, spawnPosition);
        newItem.SetValue(itemEnum, dropCount);

        newItem.gameObject.SetActive(true);

        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = Mathf.Abs(randomDirection.y);
        newItem.RigidItem.AddForce(randomDirection * forceAmount, ForceMode.Impulse);
    }
}
