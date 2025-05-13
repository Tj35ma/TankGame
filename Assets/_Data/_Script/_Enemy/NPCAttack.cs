using UnityEngine;

//[RequireComponent(typeof(TankTurretControl))]
//[RequireComponent(typeof(TankBarrelControl))]
//[RequireComponent(typeof(TankShooting))]
public class NPCAttack : MonoBehaviour
{
    //[Header("Attack Settings")]
    //[Tooltip("Sai s? khi nh?m (??). NPC s? b?n khi g�c l?ch nh? h?n gi� tr? n�y.")]
    //public float aimTolerance = 5.0f;
    //[Tooltip("T?c ?? xoay th�p ph�o c?a NPC.")]
    //public float turretRotationSpeed = 100f; // C� th? l?y t? TankTurretControl
    //// T?c ?? n�ng n�ng ph�o n?u c�
    //// public float barrelElevationSpeed = 50f;

    //[Header("References")]
    //[SerializeField] private TankTurretControl turretControl;
    //[SerializeField] private TankBarrelControl barrelControl; // T�y ch?n
    //[SerializeField] private TankShooting shootingControl;

    private Transform currentTarget = null;

    //void Awake()
    //{
    //    if (turretControl == null) turretControl = GetComponent<TankTurretControl>();
    //    if (barrelControl == null) barrelControl = GetComponent<TankBarrelControl>(); // C� th? null
    //    if (shootingControl == null) shootingControl = GetComponent<TankShooting>();

    //    if (turretControl == null || shootingControl == null)
    //    {
    //        Debug.LogError($"NPCAttack tr�n {gameObject.name} thi?u TankTurretControl ho?c TankShooting!", this);
    //        enabled = false;
    //        return;
    //    }
    //    // Ghi ?� t?c ?? xoay n?u c?n
    //    // turretControl.rotationSpeed = this.turretRotationSpeed; // C?n s?a TankTurretControl ?? cho ph�p ghi ?�
    //}

    //void OnDisable()
    //{
    //    // Khi component b? v� hi?u h�a (kh�ng c�n t?n c�ng)
    //    currentTarget = null; // X�a m?c ti�u
    //}

    public void SetTarget(Transform target)
    {
        currentTarget = target;
    }

    //void Update()
    //{
    //    if (currentTarget == null)
    //    {
    //        // Kh�ng c� m?c ti�u, kh�ng l�m g� c?
    //        // C� th? xoay th�p ph�o v? v? tr� m?c ??nh n?u mu?n
    //        return;
    //    }

    //    // 1. Nh?m th�p ph�o v�o m?c ti�u
    //    AimTurret();

    //    // 2. (T�y ch?n) Nh?m n�ng ph�o
    //    // AimBarrel();

    //    // 3. B?n n?u ?� nh?m ?? ch�nh x�c
    //    if (IsAimingAccurate())
    //    {
    //        // G?i h�m b?n c?a TankShooting
    //        // C?n s?a TankShooting ?? c� th? k�ch ho?t b?n t? script kh�c
    //        // V� d?: shootingControl.TryFire();
    //        SimulateFire(); // H�m gi? l?p
    //    }
    //}

    //void AimTurret()
    //{
    //    Vector3 directionToTarget = currentTarget.position - turretControl.turretTransform.position; // Gi? s? turretTransform public ho?c c� getter
    //    directionToTarget.y = 0; // Ch? xoay tr�n m?t ph?ng ngang

    //    if (directionToTarget != Vector3.zero)
    //    {
    //        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
    //        // Xoay m??t m�
    //        turretControl.turretTransform.rotation = Quaternion.RotateTowards(turretControl.turretTransform.rotation, targetRotation, turretRotationSpeed * Time.deltaTime);
    //        // Ho?c d�ng Slerp n?u mu?n hi?u ?ng kh�c:
    //        // turretControl.turretTransform.rotation = Quaternion.Slerp(turretControl.turretTransform.rotation, targetRotation, turretRotationSpeed * Time.deltaTime);
    //    }
    //}


    //bool IsAimingAccurate()
    //{
    //    if (currentTarget == null || turretControl == null || turretControl.turretTransform == null) return false;

    //    Vector3 directionToTarget = currentTarget.position - turretControl.turretTransform.position;
    //    directionToTarget.y = 0;

    //    float angleDifference = Vector3.Angle(turretControl.turretTransform.forward, directionToTarget);

    //    return angleDifference <= aimTolerance;
    //}

    //// H�m gi? l?p vi?c g?i b?n (C?N THAY TH? B?NG C�CH G?I TH?C T?)
    //void SimulateFire()
    //{
    //    if (shootingControl != null)
    //    {
    //        // Gi? s? TankShooting c� h�m TryFire() ho?c t??ng t?
    //        // shootingControl.TryFire();

    //        // T?m th?i g?i tr?c ti?p logic b?n (kh�ng l� t??ng)
    //        // C?n ki?m tra fire rate b�n trong TankShooting
    //        shootingControl.SimulateFire(); // Gi? s? c� h�m n�y
    //    }
    //}
}