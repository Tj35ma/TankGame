using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObj : TGMonoBehaviour
{
    [SerializeField] protected DespawnBase despawnBase;
    public DespawnBase DespawnBase => despawnBase;
    public abstract string GetName();
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDespawnBase();
    }

    protected virtual void LoadDespawnBase()
    {
        if (this.despawnBase != null) return;
        this.despawnBase = transform.GetComponentInChildren<DespawnBase>();
        Debug.Log(transform.name + ": Load DespawnBase", gameObject);
    }
}