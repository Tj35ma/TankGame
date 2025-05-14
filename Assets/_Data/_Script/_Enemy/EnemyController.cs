using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]

public class EnemyController : TGMonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    public NavMeshAgent Agent => agent;

    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    [SerializeField] protected EnemyMoving enemyMoving;
    public EnemyMoving EnemyMoving => enemyMoving;

    [SerializeField] protected EnemyAttacking enemyAttacking;
    public EnemyAttacking EnemyAttacking => enemyAttacking;

    [SerializeField] protected Transform player;
    public Transform PlayerTarget => player;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNavMeshAgent();
        this.LoadAnimator();
        this.LoadEnemyMoving();
        this.LoadEnemyAttacking();
        this.LoadPlayer();
    }
    protected virtual void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.Find("PlayerManager").transform;
        Debug.Log(transform.name + ": LoadPlayer", gameObject);
    }

    protected virtual void LoadNavMeshAgent()
    {
        if (this.agent != null) return;
        this.agent = GetComponent<NavMeshAgent>();
        this.agent.speed = 2f;
        this.agent.angularSpeed = 200f;
        this.agent.acceleration = 150f;
        Debug.Log(transform.name + ": LoadNavMeshAgent", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponentInChildren<Animator>();
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadEnemyMoving()
    {
        if (this.enemyMoving != null) return;
        this.enemyMoving = GetComponentInChildren<EnemyMoving>();
        Debug.Log(transform.name + ": LoadEnemyMoving", gameObject);
    }

    protected virtual void LoadEnemyAttacking()
    {
        if (this.enemyAttacking != null) return;
        this.enemyAttacking = GetComponentInChildren<EnemyAttacking>();
        Debug.Log(transform.name + ": LoadEnemyAttacking", gameObject);
    }
}
