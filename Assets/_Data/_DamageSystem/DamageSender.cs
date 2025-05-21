using UnityEngine;

public abstract class DamageSender : TGMonoBehaviour
{
    [SerializeField] protected int damage = 2;

    public virtual void OnTriggerEnter(Collider collider)
    {
        DamageReceiver damageReceiver = collider.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        this.Send(damageReceiver);
        Debug.Log("OnTriggerEnter2D: " + collider.name);
    }

    protected virtual void Send(DamageReceiver damageRecever)
    {
        damageRecever.Deduct(this.damage);
    }

   
}