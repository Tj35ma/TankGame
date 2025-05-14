using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawn<T> : DespawnBase where T : PoolObj
{
    [SerializeField] protected T parent;
    [SerializeField] protected Spawner<T> spawner;
    [SerializeField] protected bool isDespawnByTime = true;
    [SerializeField] protected float timeLife = 2f;
    [SerializeField] protected float currentTime = 2f;

    protected virtual void FixedUpdate()
    {
        this.DespawnByTime();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadParent();
        this.LoadSpawner();
    }

    protected virtual void LoadParent()
    {
        if (this.parent != null) return;
        this.parent = transform.parent.GetComponent<T>();
        Debug.Log(transform.name + ": LoadParent", gameObject);
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GameObject.FindAnyObjectByType<Spawner<T>>();
        Debug.Log(transform.name + ": LoadSpawner", gameObject);
    }

    protected virtual void DespawnByTime()
    {
        if (!this.isDespawnByTime) return;

        this.currentTime -= Time.fixedDeltaTime;
        if (this.currentTime > 0) return;

        this.DoDespawn();
        this.RebornByTime();
    }

    public override void DoDespawn()
    {
        this.spawner.Despawn(this.parent);

    }

    public virtual void RebornByTime()
    {
        this.currentTime = this.timeLife;
        Debug.Log("Current time reset to timeLife: " + this.currentTime);
    }
}