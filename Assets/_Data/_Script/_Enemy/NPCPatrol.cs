using UnityEngine;
using System.Collections.Generic;

// Yêu cầu có NPCTankAI và TankMovement để hoạt động
[RequireComponent(typeof(NPCTankAI))]
[RequireComponent(typeof(TankMovement))] // Hoặc một component di chuyển tương tự cho AI
public class NPCPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    [Tooltip("Danh sách các Transform của các điểm tuần tra (Waypoints).")]
    public List<Transform> waypoints;
    [Tooltip("Khoảng cách tối thiểu đến waypoint để coi như đã đến nơi.")]
    public float waypointReachedThreshold = 1.5f;
    [Tooltip("Thời gian dừng tại mỗi waypoint (giây).")]
    public float waitAtWaypointTime = 2.0f;

    [Header("References")]
    [SerializeField] private TankMovement tankMovement; // Component điều khiển di chuyển
    // Hoặc dùng NavMeshAgent nếu bạn muốn dùng hệ thống Navigation của Unity
    // [SerializeField] private UnityEngine.AI.NavMeshAgent agent;

    private int currentWaypointIndex = 0;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    void Awake()
    {
        if (tankMovement == null) tankMovement = GetComponent<TankMovement>();
        // if (agent == null) agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (tankMovement == null /*&& agent == null*/)
        {
            Debug.LogError($"NPCPatrol trên {gameObject.name} không tìm thấy TankMovement!", this);
            enabled = false;
            return;
        }
        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError($"NPCPatrol trên {gameObject.name} không có waypoints nào được gán!", this);
            enabled = false;
            return;
        }
    }

    void OnEnable()
    {
        // Khi component được kích hoạt (chuyển sang trạng thái Patrolling)
        isWaiting = false; // Reset trạng thái chờ
        waitTimer = 0f;
        // Có thể cần bắt đầu di chuyển đến waypoint đầu tiên nếu chưa
        // MoveTowardsWaypoint();
    }

    void Update()
    {
        if (waypoints.Count == 0) return;

        // Nếu đang chờ tại waypoint
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            // Hết thời gian chờ, di chuyển đến waypoint tiếp theo
            if (waitTimer >= waitAtWaypointTime)
            {
                isWaiting = false;
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count; // Chuyển sang waypoint tiếp theo (vòng lặp)
                MoveTowardsWaypoint();
            }
            else
            {
                // Đứng yên trong khi chờ
                StopMovement();
            }
        }
        // Nếu không chờ, kiểm tra xem đã đến waypoint chưa
        else
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            if (targetWaypoint == null)
            {
                Debug.LogWarning($"Waypoint tại index {currentWaypointIndex} bị null!", this);
                // Bỏ qua waypoint này hoặc xử lý lỗi khác
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
                if (waypoints.Count > 0) MoveTowardsWaypoint(); // Thử di chuyển đến điểm tiếp theo
                return;
            }

            float distanceToWaypoint = Vector3.Distance(transform.position, targetWaypoint.position);

            // Nếu đã đến đủ gần waypoint
            if (distanceToWaypoint <= waypointReachedThreshold)
            {
                isWaiting = true; // Bắt đầu chờ
                waitTimer = 0f; // Reset bộ đếm thời gian chờ
                StopMovement(); // Dừng lại tại waypoint
            }
            else
            {
                // Nếu chưa đến, tiếp tục di chuyển và xoay về phía waypoint
                MoveTowardsWaypoint();
            }
        }
    }

    void MoveTowardsWaypoint()
    {
        if (waypoints.Count == 0 || currentWaypointIndex >= waypoints.Count || waypoints[currentWaypointIndex] == null) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 directionToWaypoint = (targetWaypoint.position - transform.position).normalized;
        directionToWaypoint.y = 0; // Chỉ di chuyển trên mặt phẳng ngang

        // --- Điều khiển TankMovement ---
        // Tính toán input di chuyển và xoay dựa trên hướng đến waypoint
        // 1. Xoay: Tính góc cần xoay
        float angleToTarget = Vector3.SignedAngle(transform.forward, directionToWaypoint, Vector3.up);
        float turnInput = Mathf.Clamp(angleToTarget / 45f, -1f, 1f); // Chia cho một góc để chuẩn hóa input (-1 đến 1), 45 là ví dụ

        // 2. Di chuyển: Nếu đã gần đúng hướng thì mới tiến tới
        float moveInput = 0f;
        if (Mathf.Abs(angleToTarget) < 10f) // Chỉ di chuyển nếu góc lệch nhỏ (ví dụ: dưới 10 độ)
        {
            moveInput = 1f; // Tiến tới
        }

        // **Cần sửa đổi TankMovement để nhận input từ AI thay vì InputManager**
        // Ví dụ: tankMovement.SetAIInput(moveInput, turnInput);
        // Hoặc gọi trực tiếp các hàm bên trong TankMovement nếu có thể
        // Dưới đây là ví dụ giả định bạn có thể gọi hàm tương tự Move/Turn
        SimulateMovement(moveInput, turnInput);

        // --- Hoặc dùng NavMeshAgent ---
        // if (agent != null && agent.isActiveAndEnabled)
        // {
        //     agent.SetDestination(targetWaypoint.position);
        // }
    }

    void StopMovement()
    {
        // Cần hàm để dừng xe tăng trong TankMovement
        // Ví dụ: tankMovement.SetAIInput(0f, 0f);
        SimulateMovement(0f, 0f);

        // --- Hoặc dùng NavMeshAgent ---
        // if (agent != null && agent.isActiveAndEnabled && agent.hasPath)
        // {
        //     agent.ResetPath(); // Hoặc agent.isStopped = true;
        // }
    }

    // Hàm giả lập việc truyền input vào TankMovement (CẦN THAY THẾ BẰNG CÁCH GỌI THỰC TẾ)
    void SimulateMovement(float move, float turn)
    {
        if (tankMovement != null)
        {
            // Giả sử TankMovement có thể được điều khiển như sau (CẦN SỬA TankMovement)
            // tankMovement.SimulateMove(move);
            // tankMovement.SimulateTurn(turn);
            // Hoặc nếu bạn sửa TankMovement để có hàm SetAIInput:
            // tankMovement.SetAIInput(move, turn);

            // Tạm thời gọi trực tiếp logic tính toán (không lý tưởng vì lặp code)
            if (tankMovement.rb != null) // Truy cập Rigidbody từ TankMovement
            {
                // Move
                Vector3 movement = transform.forward * move * tankMovement.moveSpeed * Time.deltaTime; // Giả sử moveSpeed public hoặc có getter
                tankMovement.rb.MovePosition(tankMovement.rb.position + movement);
                // Turn
                float turnAmount = turn * tankMovement.turnSpeed * Time.deltaTime; // Giả sử turnSpeed public hoặc có getter
                Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
                tankMovement.rb.MoveRotation(tankMovement.rb.rotation * turnRotation);
            }
        }
    }

    // Vẽ đường tuần tra trong Editor
    void OnDrawGizmosSelected()
    {
        if (waypoints == null || waypoints.Count < 2) return;

        Gizmos.color = Color.yellow;
        Vector3 previousWaypoint = waypoints[0].position;

        for (int i = 1; i < waypoints.Count; i++)
        {
            if (waypoints[i] != null)
            {
                Gizmos.DrawLine(previousWaypoint, waypoints[i].position);
                previousWaypoint = waypoints[i].position;
            }
        }
        // Vẽ đường nối từ điểm cuối về điểm đầu
        if (waypoints[0] != null && waypoints[waypoints.Count - 1] != null)
        {
            Gizmos.DrawLine(waypoints[waypoints.Count - 1].position, waypoints[0].position);
        }

        // Vẽ các điểm waypoint
        Gizmos.color = Color.red;
        foreach (Transform wp in waypoints)
        {
            if (wp != null)
            {
                Gizmos.DrawWireSphere(wp.position, waypointReachedThreshold);
            }
        }
    }
}
