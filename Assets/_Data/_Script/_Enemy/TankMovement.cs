using UnityEngine;

// Y�u c?u GameObject ph?i c� Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class TankMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("T?c ?? di chuy?n ti?n/l�i t?i ?a.")]
    public float moveSpeed = 10f; // Gi? public ?? NPCPatrol c� th? ??c (ho?c t?o getter)
    [Tooltip("T?c ?? xoay c?a xe (??/gi�y).")]
    public float turnSpeed = 180f; // Gi? public ?? NPCPatrol c� th? ??c (ho?c t?o getter)

    [Header("Components")]
    [Tooltip("Rigidbody c?a xe t?ng. S? t? ??ng l?y n?u b? tr?ng.")]
    [SerializeField] public Rigidbody rb; // ?? public ?? NPCPatrol c� th? truy c?p (ho?c t?o getter)

    // Bi?n l?u tr? input hi?n t?i (c� th? ???c ??t b?i Player ho?c AI)
    private float currentMoveInput = 0f;
    private float currentTurnInput = 0f;

    void Awake()
    {
        // L?y component Rigidbody n?u ch?a ???c g�n
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (rb == null)
        {
            Debug.LogError("TankMovement: Rigidbody component not found!", gameObject);
            enabled = false; // V� hi?u h�a script n?u kh�ng c� Rigidbody
        }
        else
        {
            // ??m b?o Rigidbody ???c thi?t l?p ?�ng c�ch (v� d?: kh�ng b? kinematic n?u mu?n di chuy?n b?ng velocity/force)
            if (rb.isKinematic)
            {
                Debug.LogWarning("TankMovement: Rigidbody is set to Kinematic. Movement using velocity might not work as expected. Consider using MovePosition/MoveRotation or changing Rigidbody settings.", gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        // Th?c hi?n di chuy?n v� xoay d?a tr�n input hi?n t?i trong FixedUpdate
        Move(currentMoveInput);
        Turn(currentTurnInput);

        // Quan tr?ng: Reset input sau m?i FixedUpdate n?u b?n mu?n input ph?i ???c cung c?p li�n t?c
        // N?u kh�ng reset, xe t?ng s? ti?p t?c di chuy?n/xoay v?i input cu?i c�ng ???c nh?n
        // ResetAIInput(); // B? comment d�ng n�y n?u mu?n xe d?ng khi kh�ng c� input m?i t? AI
    }

    /// <summary>
    /// ??t gi� tr? input cho di chuy?n v� xoay (d�ng cho Player Input ho?c AI).
    /// </summary>
    /// <param name="move">Input di chuy?n (-1 ??n 1).</param>
    /// <param name="turn">Input xoay (-1 ??n 1).</param>
    public void SetAIInput(float move, float turn)
    {
        currentMoveInput = Mathf.Clamp(move, -1f, 1f);
        currentTurnInput = Mathf.Clamp(turn, -1f, 1f);
    }

    /// <summary>
    /// Reset input v? 0 (d�ng ?? d?ng xe khi kh�ng c� l?nh).
    /// </summary>
    public void ResetAIInput()
    {
        currentMoveInput = 0f;
        currentTurnInput = 0f;
    }

    /// <summary>
    /// X? l� di chuy?n ti?n/l�i b?ng c�ch ??t v?n t?c Rigidbody.
    /// </summary>
    /// <param name="moveInput">Gi� tr? input di chuy?n (-1 ??n 1).</param>
    private void Move(float moveInput)
    {
        if (rb == null) return;

        // T�nh to�n vector v?n t?c mong mu?n d?a tr�n input v� t?c ??
        Vector3 targetVelocity = transform.forward * moveInput * moveSpeed;

        // Gi? nguy�n v?n t?c hi?n t?i theo tr?c Y (?? tr?ng l?c/l?c kh�c v?n ho?t ??ng)
        targetVelocity.y = rb.linearVelocity.y;

        // ??t v?n t?c cho Rigidbody
        rb.linearVelocity = targetVelocity;
    }

    /// <summary>
    /// X? l� xoay xe t?ng b?ng Rigidbody.MoveRotation.
    /// </summary>
    /// <param name="turnInput">Gi� tr? input xoay (-1 ??n 1).</param>
    private void Turn(float turnInput)
    {
        if (rb == null) return;

        // T�nh to�n g�c xoay cho frame n�y
        float turnAmount = turnInput * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);

        // �p d?ng ph�p xoay v�o Rigidbody (an to�n h?n khi d�ng trong FixedUpdate)
        // Ph�p nh�n Quaternion: xoay t? g�c hi?n t?i th�m m?t g�c m?i
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    /// <summary>
    /// H�m d?ng ho�n to�n chuy?n ??ng v� xoay (c� th? g?i t? b�n ngo�i).
    /// </summary>
    public void StopMovementCompletely()
    {
        ResetAIInput(); // ??t input v? 0
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero; // D?ng v?n t?c tuy?n t�nh
            rb.angularVelocity = Vector3.zero; // D?ng v?n t?c g�c
        }
    }
}
