using UnityEngine;

public abstract class EnemyAbstract : TGMonoBehaviour
{
    [SerializeField] protected EnemyController enemyController;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyController();
    }

    protected virtual void LoadEnemyController()
    {
        if (this.enemyController != null) return;
        this.enemyController = transform.GetComponentInParent<EnemyController>();
        Debug.Log(transform.name + " Load EnemyController", gameObject);
    }
}
