using UnityEngine;

public class EnemyAttacking : EnemyAbstract
{
    [SerializeField] protected bool isAttacking = false;

    [SerializeField] protected float attackRange = 2f; // How close to attack
    [SerializeField] protected float attackCooldown = 1.5f; // Time between attacks
    [SerializeField] protected int attackDamage = 10; // Damage per attack
    [SerializeField] protected float lookAtSpeed = 8f; // How fast to turn to face player

    private float lastAttackTime; // Time of the last attack

    protected override void Start()
    {
        base.Start();
        this.lastAttackTime = Time.time - attackCooldown;
    }

    protected void FixedUpdate()
    {
        this.CheckAttacking();
    }
    protected virtual void CheckAttacking()
    {
        if (this.enemyController.EnemyMoving.DistanceToPlayer <= this.attackRange)

        {
            this.enemyController.Agent.isStopped = true;
            FacePlayer(); // Turn to face the player

            // Check if attack is off cooldown
            if (Time.time >= this.lastAttackTime + this.attackCooldown)
            {
                Attacking();
                this.lastAttackTime = Time.time; // Reset cooldown timer
            }
             return;
        }
        return;
    }

    protected virtual void Attacking()
    {
        this.enemyController.Animator.SetTrigger("isAttacking");

    }
    protected virtual void FacePlayer()
    {
        if (this.enemyController.PlayerTarget == null) return;
        Vector3 direction = (this.enemyController.PlayerTarget.position - transform.parent.position).normalized;
        if (direction == Vector3.zero) return; // Avoid issues if at the exact same position

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // Look on Y-axis only
        transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, lookRotation, Time.deltaTime * this.lookAtSpeed);
    }
}
