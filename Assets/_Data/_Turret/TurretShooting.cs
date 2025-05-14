using UnityEngine;

public class TurretShooting : TGMonoBehaviour
{
    [SerializeField] protected TurretController turretController;
    [SerializeField] protected float targetLoadSpeed = 1f;
    [SerializeField] protected int currentFirePoint = 0;
    [SerializeField] protected float shootSpeed = 0.1f;
    [SerializeField] protected float rotationSpeed = 2f;
    [SerializeField] protected EnemyController target;
    [SerializeField] protected BulletController bullet;
    [SerializeField] public int totalKill = 0;
    [SerializeField] public int killCount = 0;
    public int KillCount => killCount;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTurretController();
    }

    protected virtual void LoadTurretController()
    {
        if (this.turretController != null) return;
        this.turretController = GetComponentInParent<TurretController>();
        Debug.Log(transform.name + ": LoadTurretController", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        Invoke(nameof(this.TargetLoading), this.targetLoadSpeed);
        Invoke(nameof(this.Shooting), this.shootSpeed);
    }

    protected void FixedUpdate()
    {
        this.Looking();
        this.IsTargetDead();
    }

    protected virtual void TargetLoading()
    {
        Invoke(nameof(this.TargetLoading), this.targetLoadSpeed);
        this.target = this.turretController.TurretTargeting.Nearest;
    }

    protected virtual void Looking()
    {
        if (this.target == null) return;
        Vector3 targetPosition = this.target.TurretTargetable.transform.position;
        Vector3 direction = targetPosition - this.turretController.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        this.turretController.transform.rotation = Quaternion.Lerp(this.turretController.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }


    protected virtual void Shooting()
    {
        Invoke(nameof(this.Shooting), this.shootSpeed);
        if (this.target == null) return;

        FirePoint firePoint = this.turretController.FirePoint;
        BulletController bullet = BulletManager.Instance.BulletPrefabs.GetBulletByEnum(BulletEnum.Bullet_2);
        BulletController newBullet = this.turretController.BulletSpawner.Spawn(bullet, firePoint.transform.position);
        Vector3 rotatorDirection = this.turretController.transform.forward;
        newBullet.transform.forward = rotatorDirection;
        newBullet.gameObject.SetActive(true);
    }

    //protected virtual FirePoint GetFirePoint()
    //{
    //    FirePoint firePoint = this.turretController.FirePoint[this.currentFirePoint];
    //    this.currentFirePoint++;
    //    if (this.currentFirePoint == this.turretController.FirePoint.Count) this.currentFirePoint = 0;
    //    return firePoint;
    //}

    protected virtual bool IsTargetDead()
    {
        if (this.target == null) return true;
        if (!this.target.EnemyDamageReceiver.IsDead()) return false;
        this.killCount++;
        this.totalKill++;
        this.target = null;
        return true;
    }

    public virtual bool DeductKillCount(int count)
    {
        if (this.killCount < count) return false;
        this.killCount -= count;
        return true;
    }
}
