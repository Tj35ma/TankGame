using UnityEngine;

public class RobotCtrl : EnemyController
{
    [SerializeField] protected EnemyEnum enemyEnum;
    public override string GetName() => this.enemyEnum.ToString();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.enemyEnum = EnemyEnum.Robot;
    }
}
