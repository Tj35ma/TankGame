using UnityEngine;

public class RobotCtrl : EnemyController
{
    [SerializeField] protected EnemyEnum enemyEnum;
    public override string GetName() => this.enemyEnum.ToString();
}
