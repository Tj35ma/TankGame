using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : TGMonoBehaviour
{
    [SerializeField] protected int maxHP = 6;
    public int MaxHp => maxHP;

    [SerializeField] protected int currentHP = 6;

    public int CurrentHp => currentHP;

    protected bool isDead = false;
    [SerializeField] protected bool isImmotal = false;

    protected override void OnEnable()
    {
        this.OnReborn();
    }

    public virtual int Deduct(int hp)
    {
        if (!this.isImmotal) this.currentHP -= hp;

        if (this.IsDead())
        {
            this.OnDead();
        }
        else
        {
            this.OnHurt();
        }

        if (this.currentHP < 0) this.currentHP = 0;
        return this.currentHP;
    }

    public virtual bool IsDead()
    {
        return this.isDead = this.currentHP <= 0;
    }

    protected virtual void OnDead()
    {

    }

    protected virtual void OnHurt()
    {

    }

    protected virtual void OnReborn()
    {
        this.currentHP = this.maxHP;
    }
}