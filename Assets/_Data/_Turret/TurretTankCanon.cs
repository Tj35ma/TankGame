using UnityEngine;

public class TurretTankCanon : TurretController
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.attackRange = 10f;
    }
}
