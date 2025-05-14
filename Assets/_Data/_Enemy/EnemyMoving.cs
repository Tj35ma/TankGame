using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyMoving : EnemyAbstract
{
    
    [SerializeField] protected float stopDistance = 2f;
    [SerializeField] protected bool canMove = true;
    [SerializeField] protected bool isMoving = false;
    public bool IsMoving => isMoving;
    [SerializeField] protected float distanceToPlayer = Mathf.Infinity;
    public float DistanceToPlayer => distanceToPlayer;    

    protected override void OnEnable()
    {
        this.OnReborn();
    }   

    private void FixedUpdate()
    {
        this.Moving();
        this.CheckMoving();
    }
 
    protected virtual void Moving()
    {
        if (!this.canMove)
        {
            this.enemyController.Agent.isStopped = true;
            return;
        }

        //if (this.enemyController.EnemyDamageReceiver.IsDead())
        //{
        //    this.enemyController.Agent.isStopped = true;
        //    return;
        //}
        

        if (this.enemyController.PlayerTarget == null)
        {
            this.enemyController.Agent.isStopped = true;
            return;
        }
        
        this.enemyController.Agent.isStopped = false;
        this.enemyController.Agent.SetDestination(this.enemyController.PlayerTarget.transform.position);
        this.distanceToPlayer = Vector3.Distance(this.enemyController.PlayerTarget.transform.position, transform.parent.position);

        if (this.distanceToPlayer <= this.stopDistance)
        {
            this.enemyController.Agent.isStopped = true;
            return;
        }

    }    

    protected virtual void CheckMoving()
    {
        if (this.enemyController.Agent.velocity.magnitude > 0.1f) this.isMoving = true;
        else this.isMoving = false;

        this.enemyController.Animator.SetBool("isMoving", this.isMoving);
    }

    protected virtual void OnReborn()
    {
       
    }

}