using UnityEngine;

public class InputManager : TGSingleton<InputManager>
{   
    // --- Input Values ---
    // Các thuộc tính này sẽ được các component khác (như TankMovement) đọc
    public float MovementInput { get; private set; } // Giá trị cho di chuyển tiến/lùi
    public float TurnInput { get; private set; }     // Giá trị cho việc xoay xe

    // --- Configuration ---
    [Header("Input Axes Names")]
    [Tooltip("Tên của trục input cho di chuyển tiến/lùi (mặc định: Vertical)")]
    [SerializeField] private string verticalAxisName = "Vertical";

    [Tooltip("Tên của trục input cho xoay xe (mặc định: Horizontal)")]
    [SerializeField] private string horizontalAxisName = "Horizontal";

    // Update được gọi mỗi frame
    void Update()
    {
        // Đọc giá trị từ các trục input đã định nghĩa
        MovementInput = Input.GetAxis(verticalAxisName);
        TurnInput = Input.GetAxis(horizontalAxisName);
    }
}