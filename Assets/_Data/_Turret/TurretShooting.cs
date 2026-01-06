using UnityEngine;

public class TurretShooting : TGMonoBehaviour
{
    [SerializeField] protected TurretManager turretController;
    [SerializeField] protected float targetLoadSpeed = 1f;
    [SerializeField] protected int currentFirePoint = 0;
    [SerializeField] protected float shootSpeed = 0.5f;
    [SerializeField] protected float rotationSpeed = 10f;
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
        this.turretController = GetComponentInParent<TurretManager>();
        Debug.Log(transform.name + ": LoadTurretController", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        //InvokeRepeating(nameof(this.TargetLoading), this.targetLoadSpeed, this.targetLoadSpeed);
        InvokeRepeating(nameof(this.Shooting), this.shootSpeed, this.shootSpeed);
        //Invoke(nameof(this.TargetLoading), this.targetLoadSpeed);
        //Invoke(nameof(this.Shooting), this.shootSpeed);
    }

    protected void FixedUpdate()
    {
        this.Looking();
        this.IsTargetDead();
        this.TargetLoading();
    }

    protected virtual void TargetLoading()
    {
        //Invoke(nameof(this.TargetLoading), this.targetLoadSpeed);
        this.target = this.turretController.TurretTargeting.Nearest;
    }

    protected virtual void Looking()
    {
        if (this.target == null) return;      

        Vector3 targetPosition = this.target.TurretTargetable.transform.position;
        Vector3 direction = targetPosition - this.turretController.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        float targetYRotationAngle = targetRotation.eulerAngles.y;
        Quaternion targetRotationYOnly = Quaternion.Euler(0f, targetYRotationAngle, 0f);
        this.turretController.transform.rotation = Quaternion.Slerp(this.turretController.transform.rotation,targetRotationYOnly,Time.deltaTime * rotationSpeed);
    }


    protected virtual void Shooting()
    {
        //Invoke(nameof(this.Shooting), this.shootSpeed);
        if (this.target == null) return;
        //Vector3 turretPositionXZ = new Vector3(this.turretController.transform.position.x, 0f, this.turretController.transform.position.z);
        //Vector3 targetPositionXZ = new Vector3(this.target.transform.position.x, 0f, this.target.transform.position.z);
        //Vector3 directionToTarget = targetPositionXZ - turretPositionXZ;
        //float angleToTarget = Vector3.Angle(this.turretController.transform.forward, directionToTarget);
        //if (angleToTarget > 7.0f) return;

        FirePoint firePoint = this.GetFirePoint();
        BulletController newBullet = BulletManager.Instance.BulletSpawner.Spawn(GetBullet(), firePoint.transform.position);
        Vector3 rotatorDirection = this.turretController.transform.forward;
        newBullet.transform.forward = rotatorDirection;
        newBullet.gameObject.SetActive(true);
    }

    protected virtual FirePoint GetFirePoint()
    {
        FirePoint firePoint = this.turretController.FirePoints[this.currentFirePoint];
        this.currentFirePoint++;
        if (this.currentFirePoint == this.turretController.FirePoints.Count) this.currentFirePoint = 0;
        return firePoint;
    }

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

    protected virtual BulletController GetBullet()
    {
        return BulletManager.Instance.BulletPrefabs.GetBulletByEnum(BulletEnum.Bullet_2);
    }
}
