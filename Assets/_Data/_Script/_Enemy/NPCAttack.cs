using UnityEngine;

//[RequireComponent(typeof(TankTurretControl))]
//[RequireComponent(typeof(TankBarrelControl))]
//[RequireComponent(typeof(TankShooting))]
public class NPCAttack : MonoBehaviour
{
    //[Header("Attack Settings")]
    //[Tooltip("Sai s? khi nh?m (??). NPC s? b?n khi góc l?ch nh? h?n giá tr? này.")]
    //public float aimTolerance = 5.0f;
    //[Tooltip("T?c ?? xoay tháp pháo c?a NPC.")]
    //public float turretRotationSpeed = 100f; // Có th? l?y t? TankTurretControl
    //// T?c ?? nâng nòng pháo n?u có
    //// public float barrelElevationSpeed = 50f;

    //[Header("References")]
    //[SerializeField] private TankTurretControl turretControl;
    //[SerializeField] private TankBarrelControl barrelControl; // Tùy ch?n
    //[SerializeField] private TankShooting shootingControl;

    private Transform currentTarget = null;

    //void Awake()
    //{
    //    if (turretControl == null) turretControl = GetComponent<TankTurretControl>();
    //    if (barrelControl == null) barrelControl = GetComponent<TankBarrelControl>(); // Có th? null
    //    if (shootingControl == null) shootingControl = GetComponent<TankShooting>();

    //    if (turretControl == null || shootingControl == null)
    //    {
    //        Debug.LogError($"NPCAttack trên {gameObject.name} thi?u TankTurretControl ho?c TankShooting!", this);
    //        enabled = false;
    //        return;
    //    }
    //    // Ghi ?è t?c ?? xoay n?u c?n
    //    // turretControl.rotationSpeed = this.turretRotationSpeed; // C?n s?a TankTurretControl ?? cho phép ghi ?è
    //}

    //void OnDisable()
    //{
    //    // Khi component b? vô hi?u hóa (không còn t?n công)
    //    currentTarget = null; // Xóa m?c tiêu
    //}

    public void SetTarget(Transform target)
    {
        currentTarget = target;
    }

    //void Update()
    //{
    //    if (currentTarget == null)
    //    {
    //        // Không có m?c tiêu, không làm gì c?
    //        // Có th? xoay tháp pháo v? v? trí m?c ??nh n?u mu?n
    //        return;
    //    }

    //    // 1. Nh?m tháp pháo vào m?c tiêu
    //    AimTurret();

    //    // 2. (Tùy ch?n) Nh?m nòng pháo
    //    // AimBarrel();

    //    // 3. B?n n?u ?ã nh?m ?? chính xác
    //    if (IsAimingAccurate())
    //    {
    //        // G?i hàm b?n c?a TankShooting
    //        // C?n s?a TankShooting ?? có th? kích ho?t b?n t? script khác
    //        // Ví d?: shootingControl.TryFire();
    //        SimulateFire(); // Hàm gi? l?p
    //    }
    //}

    //void AimTurret()
    //{
    //    Vector3 directionToTarget = currentTarget.position - turretControl.turretTransform.position; // Gi? s? turretTransform public ho?c có getter
    //    directionToTarget.y = 0; // Ch? xoay trên m?t ph?ng ngang

    //    if (directionToTarget != Vector3.zero)
    //    {
    //        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
    //        // Xoay m??t mà
    //        turretControl.turretTransform.rotation = Quaternion.RotateTowards(turretControl.turretTransform.rotation, targetRotation, turretRotationSpeed * Time.deltaTime);
    //        // Ho?c dùng Slerp n?u mu?n hi?u ?ng khác:
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

    //// Hàm gi? l?p vi?c g?i b?n (C?N THAY TH? B?NG CÁCH G?I TH?C T?)
    //void SimulateFire()
    //{
    //    if (shootingControl != null)
    //    {
    //        // Gi? s? TankShooting có hàm TryFire() ho?c t??ng t?
    //        // shootingControl.TryFire();

    //        // T?m th?i g?i tr?c ti?p logic b?n (không lý t??ng)
    //        // C?n ki?m tra fire rate bên trong TankShooting
    //        shootingControl.SimulateFire(); // Gi? s? có hàm này
    //    }
    //}
}