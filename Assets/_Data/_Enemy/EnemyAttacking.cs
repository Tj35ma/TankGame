using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyAttacking : DamageSender
{
    [SerializeField] protected float attackRange = 2f; // How close to attack
    [SerializeField] protected float attackCooldown = 1.5f; // Time between attacks
    [SerializeField] protected int attackDamage = 10; // Damage per attack
    [SerializeField] protected float lookAtSpeed = 8f; // How fast to turn to face player
    private float lastAttackTime; // Time of the last attack

    [SerializeField] protected EnemyController enemyController;
    [SerializeField] protected SphereCollider sphereCollider;
    [SerializeField] protected Rigidbody enemyRigid;

    protected override void Start()
    {
        base.Start();
        this.lastAttackTime = Time.time - attackCooldown;
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.damage = 2;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyController();
        this.LoadSphereCollider();
        this.LoadRigidbody();
    }

    protected virtual void LoadEnemyController()
    {
        if (this.enemyController != null) return;
        this.enemyController = GetComponentInParent<EnemyController>();
        Debug.Log(transform.name + ": LoadEnemyController", gameObject);
    }

    protected virtual void LoadSphereCollider()
    {
        if (this.sphereCollider != null) return;
        this.sphereCollider = GetComponent<SphereCollider>();
        this.sphereCollider.isTrigger = true;
        this.sphereCollider.radius = 1f;
        this.sphereCollider.center = new Vector3(0f, 1f, 0f);
        Debug.Log(transform.name + ": LoadSphereCollider", gameObject);
    }
    protected virtual void LoadRigidbody()
    {
        if (this.enemyRigid != null) return;
        this.enemyRigid = transform.GetComponentInParent<Rigidbody>();
        Debug.Log(transform.name + ": LoadRigidbody", gameObject);
    }
    
    public virtual void OnTriggerStay(Collider collider)
    {
        DamageReceiver damageReceiver = collider.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        if (this.enemyController.EnemyDamageReceiver.IsDead()) return;
        this.enemyController.Agent.isStopped = true;
        FacePlayer(); // Turn to face the player
        // Check if attack is off cooldown
        if (Time.time >= this.lastAttackTime + this.attackCooldown)
        {
            this.enemyController.Animator.SetTrigger("isAttacking");
            this.Send(damageReceiver); // Send damage to the player
            this.lastAttackTime = Time.time; // Reset cooldown timer
        }
        return;        
    }
   
    protected virtual void FacePlayer()
    {
        Debug.Log("FacePlayer: ");
        if (this.enemyController.PlayerTarget == null) return;
        Vector3 direction = (this.enemyController.PlayerTarget.position - transform.parent.position).normalized;
        if (direction == Vector3.zero) return; // Avoid issues if at the exact same position

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // Look on Y-axis only
        transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, lookRotation, Time.deltaTime * this.lookAtSpeed);
    }    
}
