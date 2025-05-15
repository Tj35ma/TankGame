using System.Collections.Generic;
using UnityEngine;

public class TurretController : TGMonoBehaviour
{
    [SerializeField] protected TurretTargeting turretTargeting;
    public TurretTargeting TurretTargeting => turretTargeting;

    [SerializeField] protected TurretShooting turretShooting;

    [SerializeField] protected List<FirePoint> firePoints = new();
    public List<FirePoint> FirePoints => firePoints;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTuretTargeting();
        this.LoadTurretShooting();
        this.LoadFirePoint();
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
    
    protected virtual void LoadFirePoint()
    {
        if (this.firePoints.Count > 0) return;
        FirePoint[] points = transform.GetComponentsInChildren<FirePoint>();
        this.firePoints = new List<FirePoint>(points);
        Debug.Log(transform.name + ": LoadFirePoint", gameObject);
    }
}
