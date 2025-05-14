using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]


public abstract class DamageSender : TGMonoBehaviour
{
    [SerializeField] protected int damage = 2;
    [SerializeField] protected Rigidbody rigid;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        DamageReceiver damageReceiver = collider.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        this.Send(damageReceiver);
        Debug.Log("OnTriggerEnter2D: " + collider.name);
    }

    protected virtual void Send(DamageReceiver damageRecever)
    {
        damageRecever.Deduct(this.damage);
    }

    protected virtual void LoadRigidbody()
    {
        if (rigid != null) return;
        this.rigid = GetComponent<Rigidbody>();
        Debug.Log(transform.name + ": LoadRigidbody", gameObject);
    }
}