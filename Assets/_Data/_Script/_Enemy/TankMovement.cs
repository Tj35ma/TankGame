using UnityEngine;

// Yêu c?u GameObject ph?i có Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class TankMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("T?c ?? di chuy?n ti?n/lùi t?i ?a.")]
    public float moveSpeed = 10f; // Gi? public ?? NPCPatrol có th? ??c (ho?c t?o getter)
    [Tooltip("T?c ?? xoay c?a xe (??/giây).")]
    public float turnSpeed = 180f; // Gi? public ?? NPCPatrol có th? ??c (ho?c t?o getter)

    [Header("Components")]
    [Tooltip("Rigidbody c?a xe t?ng. S? t? ??ng l?y n?u b? tr?ng.")]
    [SerializeField] public Rigidbody rb; // ?? public ?? NPCPatrol có th? truy c?p (ho?c t?o getter)

    // Bi?n l?u tr? input hi?n t?i (có th? ???c ??t b?i Player ho?c AI)
    private float currentMoveInput = 0f;
    private float currentTurnInput = 0f;

    void Awake()
    {
        // L?y component Rigidbody n?u ch?a ???c gán
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (rb == null)
        {
            Debug.LogError("TankMovement: Rigidbody component not found!", gameObject);
            enabled = false; // Vô hi?u hóa script n?u không có Rigidbody
        }
        else
        {
            // ??m b?o Rigidbody ???c thi?t l?p ?úng cách (ví d?: không b? kinematic n?u mu?n di chuy?n b?ng velocity/force)
            if (rb.isKinematic)
            {
                Debug.LogWarning("TankMovement: Rigidbody is set to Kinematic. Movement using velocity might not work as expected. Consider using MovePosition/MoveRotation or changing Rigidbody settings.", gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        // Th?c hi?n di chuy?n và xoay d?a trên input hi?n t?i trong FixedUpdate
        Move(currentMoveInput);
        Turn(currentTurnInput);

        // Quan tr?ng: Reset input sau m?i FixedUpdate n?u b?n mu?n input ph?i ???c cung c?p liên t?c
        // N?u không reset, xe t?ng s? ti?p t?c di chuy?n/xoay v?i input cu?i cùng ???c nh?n
        // ResetAIInput(); // B? comment dòng này n?u mu?n xe d?ng khi không có input m?i t? AI
    }

    /// <summary>
    /// ??t giá tr? input cho di chuy?n và xoay (dùng cho Player Input ho?c AI).
    /// </summary>
    /// <param name="move">Input di chuy?n (-1 ??n 1).</param>
    /// <param name="turn">Input xoay (-1 ??n 1).</param>
    public void SetAIInput(float move, float turn)
    {
        currentMoveInput = Mathf.Clamp(move, -1f, 1f);
        currentTurnInput = Mathf.Clamp(turn, -1f, 1f);
    }

    /// <summary>
    /// Reset input v? 0 (dùng ?? d?ng xe khi không có l?nh).
    /// </summary>
    public void ResetAIInput()
    {
        currentMoveInput = 0f;
        currentTurnInput = 0f;
    }

    /// <summary>
    /// X? lý di chuy?n ti?n/lùi b?ng cách ??t v?n t?c Rigidbody.
    /// </summary>
    /// <param name="moveInput">Giá tr? input di chuy?n (-1 ??n 1).</param>
    private void Move(float moveInput)
    {
        if (rb == null) return;

        // Tính toán vector v?n t?c mong mu?n d?a trên input và t?c ??
        Vector3 targetVelocity = transform.forward * moveInput * moveSpeed;

        // Gi? nguyên v?n t?c hi?n t?i theo tr?c Y (?? tr?ng l?c/l?c khác v?n ho?t ??ng)
        targetVelocity.y = rb.linearVelocity.y;

        // ??t v?n t?c cho Rigidbody
        rb.linearVelocity = targetVelocity;
    }

    /// <summary>
    /// X? lý xoay xe t?ng b?ng Rigidbody.MoveRotation.
    /// </summary>
    /// <param name="turnInput">Giá tr? input xoay (-1 ??n 1).</param>
    private void Turn(float turnInput)
    {
        if (rb == null) return;

        // Tính toán góc xoay cho frame này
        float turnAmount = turnInput * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);

        // Áp d?ng phép xoay vào Rigidbody (an toàn h?n khi dùng trong FixedUpdate)
        // Phép nhân Quaternion: xoay t? góc hi?n t?i thêm m?t góc m?i
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    /// <summary>
    /// Hàm d?ng hoàn toàn chuy?n ??ng và xoay (có th? g?i t? bên ngoài).
    /// </summary>
    public void StopMovementCompletely()
    {
        ResetAIInput(); // ??t input v? 0
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero; // D?ng v?n t?c tuy?n tính
            rb.angularVelocity = Vector3.zero; // D?ng v?n t?c góc
        }
    }
}
