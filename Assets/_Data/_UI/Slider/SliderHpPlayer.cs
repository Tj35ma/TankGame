using UnityEngine;

public class SliderHpPlayer : SliderController
{
    protected override float GetValue()
    {
        int currentHp = PlayerManager.Instance.PlayerDamageReceiver.CurrentHp;
        int maxHP = PlayerManager.Instance.PlayerDamageReceiver.MaxHp;

        return (float)currentHp / (float)maxHP;
    }
}
