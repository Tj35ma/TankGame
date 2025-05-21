using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyDamageSender : DamageSender
{
    [SerializeField] protected EnemyController enemyController;
    [SerializeField] protected SphereCollider sphereCollider;
    [SerializeField] protected Rigidbody enemyRigid;

    protected override void ResetValue()
    {
        base.ResetValue();
        this.damage = 2;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyController();
        this.LoadSphereCollider();
        this.LoadRigidbody();
    }

    protected virtual void LoadEnemyController()
    {
        if (this.enemyController != null) return;
        this.enemyController = GetComponentInParent<EnemyController>();
        Debug.Log(transform.name + ": LoadEnemyController", gameObject);
    }

    protected virtual void LoadSphereCollider()
    {
        if (this.sphereCollider != null) return;
        this.sphereCollider = GetComponent<SphereCollider>();
        this.sphereCollider.isTrigger = true;
        this.sphereCollider.radius = 1f;
        this.sphereCollider.center = new Vector3(0f, 1f, 0f);
        Debug.Log(transform.name + ": LoadSphereCollider", gameObject);
    }
    protected virtual void LoadRigidbody()
    {
        if (this.enemyRigid != null) return;
        this.enemyRigid = transform.GetComponentInParent<Rigidbody>();
        Debug.Log(transform.name + ": LoadRigidbody", gameObject);
    }
}
