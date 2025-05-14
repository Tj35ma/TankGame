using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TurretTargetable : TGMonoBehaviour
{
    [SerializeField] protected SphereCollider sphereCollider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSphereCollider();
    }

    protected virtual void LoadSphereCollider()
    {
        if (this.sphereCollider != null) return;
        this.sphereCollider = GetComponent<SphereCollider>();
        this.sphereCollider.radius = 0.5f;
        this.sphereCollider.isTrigger = true;
        this.sphereCollider.center = new Vector3(0f,1f,0f);
        Debug.Log(transform.name + ": LoadSphereCollider", gameObject);
    }
}
