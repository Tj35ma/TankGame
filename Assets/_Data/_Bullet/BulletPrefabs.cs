using UnityEngine;

public class BulletPrefabs : PoolPrefabs<BulletController>
{
    public virtual BulletController GetBulletByEnum(BulletEnum bulletEnum)
    {
        return this.GetPrefabByName(bulletEnum.ToString());
    }
}
