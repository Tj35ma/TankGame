using UnityEngine;

public class BulletController : PoolObj
{
    [SerializeField] protected float speed = 50f;

    [SerializeField] protected BulletEnum bulletEnum;
    public BulletEnum BulletEnum => bulletEnum;
    public override string GetName() => this.bulletEnum.ToString();

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    protected virtual void SetEnum()
    {
        this.bulletEnum = BulletEnum.Bullet_2;
    }
}
