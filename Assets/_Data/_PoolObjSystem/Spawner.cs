using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : TGMonoBehaviour where T : PoolObj
{
    [SerializeField] protected int spawnCount = 0;
    [SerializeField] protected Transform poolHolder;
    public void SetParentHolder(T obj) => obj.transform.parent = this.poolHolder.transform;
    [SerializeField] protected List<T> inPoolObjs = new();

    [SerializeField] protected PoolPrefabs<T> poolPrefabs;
    public PoolPrefabs<T> PoolPrefabs => poolPrefabs;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoolHolder();
        this.LoadPoolPrefabs();
    }

    protected virtual void LoadPoolHolder()
    {
        if (this.poolHolder != null) return;
        this.poolHolder = transform.Find("PoolHolder");
        if (this.poolHolder == null)
        {
            this.poolHolder = new GameObject("PoolHolder").transform;
            this.poolHolder.parent = transform;
        }
        Debug.Log(transform.name + ": LoadPoolHolder", gameObject);
    }

    protected virtual void LoadPoolPrefabs()
    {
        if (this.poolPrefabs != null) return;
        this.poolPrefabs = GetComponentInChildren<PoolPrefabs<T>>();
        Debug.Log(transform.name + ": LoadPoolPrefabs", gameObject);
    }

    public virtual T Spawn(T prefab)
    {
        T newObj = this.GetObjFromPool(prefab);
        if (newObj == null)
        {
            newObj = Instantiate(prefab);
            newObj.name = prefab.name;

        }
        if (this.poolHolder != null) this.SetParentHolder(newObj);
        this.spawnCount++;
        return newObj;
    }

    public virtual T Spawn(T prefab, Vector3 position)
    {
        T newBullet = this.Spawn(prefab);
        newBullet.transform.position = position;
        return newBullet;
    }


    public virtual void Despawn(T obj)
    {
        if (obj == null) return;

        if (obj is MonoBehaviour monoBehaviour)
        {
            this.spawnCount--;
            this.AddObjectToPool(obj);
            monoBehaviour.gameObject.SetActive(false);
        }
    }

    protected virtual void AddObjectToPool(T obj)
    {
        this.inPoolObjs.Add(obj);
    }

    protected virtual void RemoveObjectFromPool(T obj)
    {
        this.inPoolObjs.Remove(obj);
    }

    protected virtual void UpdateName(Transform prefab, Transform newObject)
    {
        newObject.name = prefab.name + "_" + this.spawnCount;
    }

    protected virtual T GetObjFromPool(T prefab)
    {
        foreach (T inPoolObj in this.inPoolObjs)
        {
            if (prefab.GetName() == inPoolObj.GetName())
            {
                this.RemoveObjectFromPool(inPoolObj);
                return inPoolObj;
            }
        }

        return null;
    }

}