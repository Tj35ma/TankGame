using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class BulletDamageSender : DamageSender
{
    [SerializeField] protected BulletController bulletController;
    [SerializeField] protected SphereCollider sphereCollider;

    protected override void ResetValue()
    {
        base.ResetValue();
        this.damage = 2;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletController();
        this.LoadSphereCollider();
    }

    protected virtual void LoadBulletController()
    {
        if (this.bulletController != null) return;
        this.bulletController = GetComponentInParent<BulletController>();
        Debug.Log(transform.name + ": LoadBulletController", gameObject);
    }

    protected virtual void LoadSphereCollider()
    {
        if(this.sphereCollider != null) return;
        this.sphereCollider = GetComponent<SphereCollider>();
        Debug.Log(transform.name + ": LoadSphereCollider", gameObject);
    }

    protected override void Send(DamageReceiver damageReceiver)
    {
        base.Send(damageReceiver);
        
    }
}
