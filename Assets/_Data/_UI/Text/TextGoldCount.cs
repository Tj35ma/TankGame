using TMPro;
using UnityEngine;

public class TextGoldCount : TGMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textGoldCount;

    protected virtual void FixedUpdate()
    {
        this.LoadGoldCount();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTextPro();
    }

    protected virtual void LoadTextPro()
    {
        if (textGoldCount != null) return;
        this.textGoldCount = GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadTextPro", gameObject);
    }

    protected virtual void LoadGoldCount()
    {
        ItemInventory item = InventoryManager.Instance.Currencies().FindItem(ItemEnum.Gold);
        string goldCount;
        if (item == null) goldCount = "0";
        else goldCount = item.itemCount.ToString();
        this.textGoldCount.text = goldCount;
    }
}
