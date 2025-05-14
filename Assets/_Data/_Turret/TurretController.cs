using UnityEngine;

public class TurretController : TGMonoBehaviour
{
    [SerializeField] protected TurretTargeting turretTargeting;
    public TurretTargeting TurretTargeting => turretTargeting;
    [SerializeField] protected TurretShooting turretShooting;

    [SerializeField] protected BulletSpawner bulletSpawner;
    public BulletSpawner BulletSpawner => bulletSpawner;

    [SerializeField] protected BulletPrefabs bulletPrefabs;
    public BulletPrefabs BulletPrefabs => bulletPrefabs;

    [SerializeField] protected BulletController bullet;
    public BulletController Bullet => bullet;

    [SerializeField] protected FirePoint firePoint;
    public FirePoint FirePoint => firePoint;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTuretTargeting();
        this.LoadTurretShooting();
    }

    protected virtual void LoadTuretTargeting()
    {
        if (this.turretTargeting != null) return;
        this.turretTargeting = transform.GetComponentInChildren<TurretTargeting>();
        Debug.Log(transform.name + ": LoadTuretTargeting", gameObject);
    }

    protected virtual void LoadTurretShooting()
    {
        if (this.turretShooting != null) return;
        this.turretShooting = transform.GetComponentInChildren<TurretShooting>();
        Debug.Log(transform.name + ": LoadTurretShooting", gameObject);
    }
    
    protected virtual void LoadBulletSpawner()
    {
        if (this.bulletSpawner != null) return;
        this.bulletSpawner = BulletManager.Instance.BulletSpawner;
        Debug.Log(transform.name + ": LoadBulletSpawner", gameObject);
    }
}
