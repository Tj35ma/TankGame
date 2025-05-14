using UnityEngine;

public class BulletManager : TGSingleton<BulletManager>
{
    [SerializeField] protected BulletSpawner bulletSpawner;
    public BulletSpawner BulletSpawner => bulletSpawner;
    [SerializeField] protected BulletPrefabs bulletPrefabs;
    public BulletPrefabs BulletPrefabs => bulletPrefabs;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletPrefabs();
        this.LoadBulletSpawner();        
    }

    protected virtual void LoadBulletSpawner()
    {
        if (this.bulletSpawner != null) return;
        this.bulletSpawner = transform.GetComponentInChildren<BulletSpawner>();
        Debug.Log(transform.name + ": LoadBulletSpawner", gameObject);
    }

    protected virtual void LoadBulletPrefabs()
    {
        if (this.bulletPrefabs != null) return;
        this.bulletPrefabs = transform.GetComponentInChildren<BulletPrefabs>();
        Debug.Log(transform.name + ": LoadBulletPrefabs", gameObject);
    }    
}
