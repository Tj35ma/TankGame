using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class BulletDamageSender : DamageSender
{
    [SerializeField] protected BulletController bulletController;
    [SerializeField] protected SphereCollider sphereCollider;
    [SerializeField] protected Rigidbody bulletRigid;

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
        this.LoadRigidbody();
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
    protected virtual void LoadRigidbody()
    {
        if (this.bulletRigid != null) return;
        this.bulletRigid = GetComponent<Rigidbody>();
        Debug.Log(transform.name + ": LoadRigidbody", gameObject);
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        DamageReceiver damageReceiver = collider.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        this.Send(damageReceiver);
        Debug.Log("OnTriggerEnter: " + collider.name);
    }

    protected override void Send(DamageReceiver damageReceiver)
    {
        if(damageReceiver == PlayerManager.Instance.PlayerDamageReceiver) return;
        base.Send(damageReceiver);
        this.DespawnBullet();
    }

    protected virtual void DespawnBullet()
    {
        this.bulletController.DespawnBase.DoDespawn();
    }
}
