using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemDropPicker : TGMonoBehaviour
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
        this.sphereCollider.center = new Vector3(0f, -1f, 0f);
        this.sphereCollider.radius = 1f;
        this.sphereCollider.isTrigger = true;
        Debug.Log(transform.name + ": LoadSphereCollider", gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null) return;
        ItemDropController itemDropCtrl = other.transform.parent.GetComponent<ItemDropController>();
        if (itemDropCtrl == null) return;
        itemDropCtrl.DespawnBase.DoDespawn();
    }
}
