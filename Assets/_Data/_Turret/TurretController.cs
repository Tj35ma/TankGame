using UnityEngine;

public abstract class TurretController : TGMonoBehaviour
{
    [SerializeField] protected float attackRange = 10f;
    [SerializeField] protected SphereCollider sphereCollider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSphereCollider();   
    }

    protected virtual void LoadSphereCollider()
    {
        if (this.sphereCollider != null) return;
        this.sphereCollider = TurretManager.Instance.TurretTargeting.SphereCollider;
        this.sphereCollider.radius = this.attackRange;
        this.sphereCollider.isTrigger = true;
        Debug.Log(transform.name + ": LoadSphereCollider", gameObject);
    }
}
