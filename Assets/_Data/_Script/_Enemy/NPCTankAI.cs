using UnityEngine;
using UnityEngine.AI; // Có thể cần nếu dùng NavMeshAgent

public class NPCTankAI : MonoBehaviour
{
    // Các trạng thái có thể có của NPC
    public enum AIState
    {
        Patrolling, // Đang tuần tra
        Chasing,    // Đang đuổi theo Player (tùy chọn)
        Attacking   // Đang tấn công Player
    }

    [Header("AI State")]
    [SerializeField] private AIState currentState = AIState.Patrolling; // Trạng thái hiện tại

    [Header("References")]
    [Tooltip("Component xử lý tuần tra")]
    [SerializeField] private NPCPatrol patrolComponent;
    [Tooltip("Component xử lý phát hiện")]
    [SerializeField] private NPCDetection detectionComponent;
    [Tooltip("Component xử lý tấn công")]
    [SerializeField] private NPCAttack attackComponent;
    [Tooltip("Component xử lý di chuyển của Tank (cần để dừng/di chuyển)")]
    [SerializeField] private TankMovement tankMovement; 

    [Header("Target")]
    [HideInInspector] public Transform currentTarget; // Mục tiêu hiện tại (Player)

    void Awake()
    {
        // Lấy các component nếu chưa gán
        if (patrolComponent == null) patrolComponent = GetComponent<NPCPatrol>();
        if (detectionComponent == null) detectionComponent = GetComponent<NPCDetection>();
        if (attackComponent == null) attackComponent = GetComponent<NPCAttack>();
        if (tankMovement == null) tankMovement = GetComponent<TankMovement>(); // Hoặc GetComponentInChildren

        // Kiểm tra các component cần thiết
        if (patrolComponent == null || detectionComponent == null || attackComponent == null || tankMovement == null)
        {
            Debug.LogError($"NPCTankAI trên {gameObject.name} thiếu một hoặc nhiều component tham chiếu!", this);
            enabled = false;
            return;
        }
    }

    void Update()
    {
        // Kiểm tra xem có phát hiện người chơi không
        currentTarget = detectionComponent.FindTarget();

        // Cập nhật trạng thái dựa trên việc có mục tiêu hay không
        UpdateState();

        // Thực hiện hành động dựa trên trạng thái hiện tại
        ExecuteCurrentState();
    }

    void UpdateState()
    {
        if (currentTarget != null)
        {
            // Nếu đang tuần tra mà thấy mục tiêu -> chuyển sang tấn công
            if (currentState == AIState.Patrolling)
            {
                ChangeState(AIState.Attacking);
            }
            // (Tùy chọn) Nếu muốn có trạng thái Chasing, bạn có thể thêm logic ở đây
            // Ví dụ: Nếu mục tiêu ở xa nhưng thấy được -> Chasing
            // Nếu mục tiêu đủ gần -> Attacking
        }
        else
        {
            // Nếu đang tấn công mà mất dấu mục tiêu -> quay lại tuần tra
            if (currentState == AIState.Attacking)
            {
                ChangeState(AIState.Patrolling);
            }
            // Nếu đang đuổi theo mà mất dấu -> quay lại tuần tra
            // if (currentState == AIState.Chasing)
            // {
            //     ChangeState(AIState.Patrolling);
            // }
        }
    }

    void ExecuteCurrentState()
    {
        switch (currentState)
        {
            case AIState.Patrolling:
                // Kích hoạt tuần tra, vô hiệu hóa tấn công
                patrolComponent.enabled = true;
                attackComponent.enabled = false;
                // Cho phép xe tăng di chuyển theo logic tuần tra
                // (TankMovement có thể cần được điều khiển bởi NPCPatrol)
                break;

            case AIState.Chasing:
                // (Logic cho trạng thái đuổi theo nếu có)
                // Ví dụ: Vô hiệu hóa tuần tra, kích hoạt di chuyển về phía target
                patrolComponent.enabled = false;
                attackComponent.enabled = false;
                // Gọi hàm di chuyển về phía target trong TankMovement (cần sửa TankMovement)
                break;

            case AIState.Attacking:
                // Vô hiệu hóa tuần tra, kích hoạt tấn công
                patrolComponent.enabled = false;
                attackComponent.enabled = true;
                // Có thể dừng xe tăng lại khi đang tấn công
                // tankMovement.StopMovement(); // Cần thêm hàm này vào TankMovement
                // Hoặc cho phép di chuyển chậm trong khi tấn công
                attackComponent.SetTarget(currentTarget); // Cung cấp mục tiêu cho component tấn công
                break;
        }
    }

    void ChangeState(AIState newState)
    {
        if (currentState == newState) return; // Không thay đổi nếu đã ở trạng thái đó

        Debug.Log($"{gameObject.name} changing state from {currentState} to {newState}");
        currentState = newState;

        // Có thể thêm logic khi chuyển trạng thái ở đây (ví dụ: reset animation,...)
    }

    // --- Getter cho trạng thái hiện tại (nếu cần từ script khác) ---
    public AIState GetCurrentState()
    {
        return currentState;
    }
}
