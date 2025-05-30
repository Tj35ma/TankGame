using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class EnemyDamageReceiver : DamageReceiver
{
    [SerializeField] protected CapsuleCollider capsuleCollider;
    [SerializeField] protected EnemyController enemyController;
    [SerializeField] protected int goldReward = 1;
    [SerializeField] protected int expReward = 5;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCapsuleCollider();
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadCapsuleCollider()
    {
        if (capsuleCollider != null) return;
        this.capsuleCollider = GetComponent<CapsuleCollider>();
        this.capsuleCollider.center = new Vector3(0f, 1.37f, -0.04f);
        this.capsuleCollider.radius = 0.4f;
        this.capsuleCollider.height = 3f;
        Debug.Log(transform.name + ": LoadCapsuleCollider", gameObject);
    }

    protected virtual void LoadEnemyCtrl()
    {

        if (this.enemyController != null) return;
        this.enemyController = transform.parent.GetComponent<EnemyController>();
        Debug.Log(transform.name + ": LoadEnemyCtrl", gameObject);
    }

    protected override void OnDead()
    {
        base.OnDead();        
        this.enemyController.Animator.SetBool("isDead", this.isDead);
        this.capsuleCollider.enabled = false;
        this.RewardOnDead();
        Invoke(nameof(this.Disappear), 3f);
    }

    protected override void OnHurt()
    {
        base.OnHurt();
        this.enemyController.Animator.SetTrigger("isHurt");
    }

    protected virtual void Disappear()
    {
        this.enemyController.EnemyDespawn.DoDespawn();
    }

    protected override void OnReborn()
    {
        base.OnReborn();
        this.capsuleCollider.enabled = true;
    }

    protected virtual void RewardOnDead()
    {        
        ItemDropManager.Instance.DropMany(ItemEnum.Gold, this.goldReward, transform.position);
        ItemDropManager.Instance.DropMany(ItemEnum.Exp, this.expReward, transform.position);
    }
}
