using UnityEngine;

public abstract class DamageSender : TGMonoBehaviour
{
    [SerializeField] protected int damage = 2;   

    protected virtual void Send(DamageReceiver damageReicever)
    {
        damageReicever.Deduct(this.damage);
    }

   
}