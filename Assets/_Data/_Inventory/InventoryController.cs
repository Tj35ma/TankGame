using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryController : TGMonoBehaviour
{
    [SerializeField] protected List<ItemInventory> items = new();
    public List<ItemInventory> Items => items;
    public abstract InventoryEnum GetName();


    public virtual void AddItem(ItemInventory item)
    {
        ItemInventory itemExist = this.FindItem(item.ItemProfile.itemEnum); //error
        if (!item.ItemProfile.isStackable || itemExist == null)
        {
            item.SetId(Random.Range(0, 999999999));
            this.items.Add(item);
            return;
        }

        itemExist.itemCount += item.itemCount;
    }
    public virtual bool RemoveItem(ItemInventory item)
    {
        ItemInventory itemExist = this.FindItemNotEmpty(item.ItemProfile.itemEnum);
        if (itemExist == null) return false;
        if (itemExist.itemCount < item.itemCount) return false;
        itemExist.itemCount -= item.itemCount;
        if (itemExist.itemCount == 0) this.items.Remove(itemExist);
        return true;
    }

    public virtual ItemInventory FindItem(ItemEnum itemEnum)
    {
        foreach (ItemInventory itemInventory in this.items)
        {
            if (itemInventory.ItemProfile.itemEnum == itemEnum) return itemInventory;
        }
        return null;
    }

    public virtual ItemInventory FindItemNotEmpty(ItemEnum itemEnum)
    {
        foreach (ItemInventory itemInventory in this.items)
        {
            if (itemInventory.ItemProfile.itemEnum != itemEnum) continue;
            if (itemInventory.itemCount > 0) return itemInventory;
        }
        return null;
    }
}