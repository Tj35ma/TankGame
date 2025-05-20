using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ItemDropController : PoolObj
{
    [SerializeField] protected Rigidbody rigidItem;
    public Rigidbody RigidItem => rigidItem;
    [SerializeField] protected InventoryEnum inventoryEnum = InventoryEnum.Items;
    public InventoryEnum InventoryEnum => inventoryEnum;
    
    [SerializeField] protected int itemCount = 1;
    public int ItemCount => itemCount;

    [SerializeField] protected ItemEnum itemEnum;
    public ItemEnum ItemEnum => itemEnum;
    public override string GetName() => this.itemEnum.ToString();

    public virtual void SetValue(ItemEnum itemEnum, int itemCount)
    {
        this.itemEnum = itemEnum;
        this.itemCount = itemCount;
    }
    public virtual void SetValue(ItemEnum itemEnum, int itemCount, InventoryEnum inventoryEnum)
    {
        this.itemEnum = itemEnum;
        this.itemCount = itemCount;
        this.inventoryEnum = inventoryEnum;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
    }

    protected virtual void LoadRigidbody()
    {
        if (this.rigidItem != null) return;
        this.rigidItem = GetComponent<Rigidbody>();
        Debug.Log(transform.name + ": LoadRigibody", gameObject);
    }
}
