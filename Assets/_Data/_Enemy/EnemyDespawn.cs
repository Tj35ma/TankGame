using UnityEngine;

public class EnemyDespawn : Despawn<EnemyController>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.isDespawnByTime = false;
    }
}
