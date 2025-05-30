using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : TGSingleton<InventoryManager>
{
    [SerializeField] protected List<InventoryController> inventories;
    [SerializeField] protected List<ItemProfileSO> itemProfiles;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInventories();
        this.LoadItemProfiles();
    }    

    protected virtual void LoadInventories()
    {
        if (this.inventories.Count > 0) return;
        foreach (Transform obj in this.transform)
        {
            InventoryController inventoryController = obj.GetComponent<InventoryController>();
            if (inventoryController == null) continue;
            this.inventories.Add(inventoryController);
        }
        Debug.Log(transform.name + " Load Inventories ", gameObject);
    }

    public virtual InventoryController GetInventoryByEnum(InventoryEnum inventoryEnum)
    {
        if (this.inventories.Count <= 0) return null;
        foreach (InventoryController inventory in this.inventories)
        {
            if (inventory.GetName() == inventoryEnum) return inventory;
        }
        return null;
    }

    public virtual ItemProfileSO GetProfileByEnum(ItemEnum itemEnum)
    {
        if (this.itemProfiles.Count <= 0) return null;
        foreach (ItemProfileSO itemProfileSO in this.itemProfiles)
        {
            if (itemProfileSO.itemEnum == itemEnum) return itemProfileSO;
        }
        return null;
    }

    public virtual InventoryController Currencies() => this.GetInventoryByEnum(InventoryEnum.Currencies);
    public virtual InventoryController Items() => this.GetInventoryByEnum(InventoryEnum.Items);

    public virtual void AddItem(ItemInventory itemInventory)
    {
        InventoryEnum inventoryEnum = itemInventory.ItemProfile.inventoryEnum;
        InventoryController inventoryCtrl = InventoryManager.Instance.GetInventoryByEnum(inventoryEnum);
        inventoryCtrl.AddItem(itemInventory);
    }

    public virtual void AddItem(ItemEnum itemEnum, int itemCount)
    {
        ItemProfileSO itemProfile = InventoryManager.Instance.GetProfileByEnum(itemEnum);
        ItemInventory item = new(itemProfile, itemCount);
        this.AddItem(item);
    }

    public virtual void RemoveItem(ItemEnum itemEnum, int itemCount)
    {
        ItemProfileSO itemProfile = InventoryManager.Instance.GetProfileByEnum(itemEnum);
        ItemInventory item = new(itemProfile, itemCount);
        this.RemoveItem(item);
    }

    public virtual void RemoveItem(ItemInventory itemInventory)
    {
        InventoryEnum inventoryEnum = itemInventory.ItemProfile.inventoryEnum;
        InventoryController inventoryCtrl = InventoryManager.Instance.GetInventoryByEnum(inventoryEnum);
        inventoryCtrl.RemoveItem(itemInventory);
    }

    protected virtual void LoadItemProfiles()
    {
        if (this.itemProfiles.Count > 0) return;
        ItemProfileSO[] itemProfileSOs = Resources.LoadAll<ItemProfileSO>("/");
        this.itemProfiles = new List<ItemProfileSO>(itemProfileSOs);
        Debug.Log(transform.name + ": LoadItemProfiles", gameObject);
    }
}