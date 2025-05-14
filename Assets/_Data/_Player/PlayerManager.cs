using UnityEngine;

public class PlayerManager : TGSingleton<PlayerManager>
{
    [SerializeField] protected Camera playerCamera;
    [SerializeField] protected GameObject playerModel;
    [SerializeField] protected PlayerMovement playerMovement;   
    [SerializeField] Rigidbody playerRigidbody;
    public Rigidbody PlayerRigidbody => playerRigidbody;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCamera();
        this.LoadModel();
        this.LoadPlayerMovement();
        this.LoadRigidBody();
    }

    protected virtual void LoadCamera()
    {
        if (this.playerCamera != null) return;
        this.playerCamera = transform.GetComponentInChildren<Camera>();
        Debug.Log(transform.name + ": LoadCamera", gameObject);
    }

    protected virtual void LoadModel()
    {
        if (this.playerModel != null) return;
        this.playerModel = GameObject.Find("PlayerModel");
        Debug.Log(transform.name + ": LoadModel", gameObject);
    }

    protected virtual void LoadPlayerMovement()
    {
        if(this.playerMovement != null) return;
        this.playerMovement = transform.GetComponentInChildren<PlayerMovement>();
        Debug.Log(transform.name + ": LoadPlayerMovement", gameObject);
    }

    protected virtual void LoadRigidBody()
    {
        if(this.playerRigidbody != null) return;
        this.playerRigidbody = transform.GetComponent<Rigidbody>();
        Debug.Log(transform.name + ": LoadRigidBody", gameObject);
    }
}
