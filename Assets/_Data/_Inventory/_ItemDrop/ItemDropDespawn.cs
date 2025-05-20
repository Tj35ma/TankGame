using UnityEngine;

public class ItemDropDespawn : Despawn<ItemDropController>   
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.isDespawnByTime = false;
    }

    public override void DoDespawn()    
    {
        ItemDropController itemDropCtrl = (ItemDropController)this.parent;
        InventoryManager.Instance.AddItem(itemDropCtrl.ItemEnum, itemDropCtrl.ItemCount);

        base.DoDespawn();
    }
}
