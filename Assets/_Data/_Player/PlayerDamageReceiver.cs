using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerDamageReceiver : DamageReceiver
{
    [SerializeField] protected BoxCollider boxCollider;
    protected int playerCurrentHp;
    protected int playerMaxHp;

    protected override void ResetValue()
    {
        base.ResetValue();
        this.currentHP = 10;
        this.maxHP = 10;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoxCollider();
    }
    protected virtual void LoadBoxCollider()
    {
        if (boxCollider != null) return;
        this.boxCollider = GetComponent<BoxCollider>();        
        Debug.Log(transform.name + ": LoadBoxCollider", gameObject);
    }

    protected override void OnDead()
    {
        base.OnDead();
        transform.parent.gameObject.SetActive(false);
        Debug.Log("EndGame", gameObject);
    }

    protected override void OnHurt()
    {
        base.OnHurt();
        //Animation Hurt
    }   

    protected override void OnReborn()
    {
        base.OnReborn();
        //for reborn
    }

    protected virtual int GetPlayerCurrentHp()
    {
        this.playerCurrentHp = this.currentHP;
        return this.playerCurrentHp;
    }
}
