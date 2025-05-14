using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]

public class TurretTargeting : TGMonoBehaviour
{
    [SerializeField] protected SphereCollider sphereCollider;
    [SerializeField] protected Rigidbody rigid;

    [SerializeField] protected EnemyController nearest;
    public EnemyController Nearest => nearest;

    [SerializeField] protected LayerMask obstacleLayerMask;

    [SerializeField] protected List<EnemyController> enemies = new();


    protected virtual void FixedUpdate()
    {
        this.FindNearest();
        this.RemoveDeadEnemy();
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        this.AddEnemy(collider);
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        this.RemoveEnemy(collider);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSphereCollider();
        this.LoadRigidbody();
    }

    protected virtual void LoadSphereCollider()
    {
        if (this.sphereCollider != null) return;
        this.sphereCollider = GetComponent<SphereCollider>();
        this.sphereCollider.radius = 10f;
        this.sphereCollider.isTrigger = true;
        Debug.Log(transform.name + ": LoadSphereCollider", gameObject);
    }

    protected virtual void LoadRigidbody()
    {
        if (this.rigid != null) return;
        this.rigid = transform.parent.GetComponentInParent<Rigidbody>();
        Debug.Log(transform.name + ": LoadRigidbody", gameObject);
    }


    protected virtual void AddEnemy(Collider collider)
    {
        if (collider.name != "TurretTargetable") return;
        EnemyController enemyCtrl = collider.transform.parent.GetComponent<EnemyController>();
        //if (enemyCtrl.EnemyDamageReceiver.IsDead()) return;
        this.enemies.Add(enemyCtrl);
    }

    protected virtual void RemoveEnemy(Collider collider)
    {
        foreach (EnemyController enemyCtrl in this.enemies)
        {
            if (collider.transform.parent == enemyCtrl.transform)
            {
                if (enemyCtrl == this.nearest) this.nearest = null;
                this.enemies.Remove(enemyCtrl);
                return;
            }
        }
    }

    protected virtual void FindNearest()
    {
        float nearestDistance = Mathf.Infinity;
        float enemyDistance;
        foreach (EnemyController enemyCtrl in this.enemies)
        {
            if (!this.CanSeeTarget(enemyCtrl)) continue;

            enemyDistance = Vector3.Distance(transform.position, enemyCtrl.transform.position);
            if (enemyDistance < nearestDistance)
            {
                nearestDistance = enemyDistance;
                this.nearest = enemyCtrl;
            }
        }
    }

    protected virtual bool CanSeeTarget(EnemyController target)
    {
        Vector3 directionToTarget = target.transform.position - transform.parent.position;
        float distanceToTarget = directionToTarget.magnitude;

        if (Physics.Raycast(transform.parent.position, directionToTarget, out RaycastHit hitInfo, distanceToTarget, this.obstacleLayerMask))
        {
            Vector3 directionToCollider = hitInfo.point - transform.parent.position;
            float distanceToCollider = directionToCollider.magnitude;

            Debug.DrawRay(transform.parent.position, directionToCollider.normalized * distanceToCollider, Color.red);
            return false;
        }

        Debug.DrawRay(transform.parent.position, directionToTarget.normalized * distanceToTarget, Color.green);
        return true;
    }


    protected virtual void RemoveDeadEnemy()
    {
        //foreach (EnemyController enemyCtrl in this.enemies)
        //{
        //    if (enemyCtrl.EnemyDamageReceiver.IsDead())
        //    {
        //        if (enemyCtrl == this.nearest) this.nearest = null;
        //        this.enemies.Remove(enemyCtrl);
        //        return;
        //    }
        //}
    }
}
