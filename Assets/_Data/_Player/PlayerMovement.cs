using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : TGMonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float turnSpeed = 180f; // Degrees per second

    [SerializeField] protected Rigidbody rb;    

    void FixedUpdate()
    {        
        this.Move();
        this.Turn();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
    }

    protected virtual void LoadRigidbody()
    {
        if(this.rb != null) return;
        this.rb = transform.parent.GetComponent<Rigidbody>();
        this.rb.interpolation = RigidbodyInterpolation.Interpolate;
        Debug.Log(transform.name + " :LoadRigidbody", gameObject);
    }

    private void Move()
    {
        // Tính toán vector vận tốc mong muốn dựa trên input và tốc độ
        // Giữ nguyên vận tốc hiện tại theo trục Y (để trọng lực vẫn hoạt động)
        float currentYVelocity = this.rb.linearVelocity.y;

        // Vận tốc mới theo hướng phía trước của xe tăng
        Vector3 targetVelocity = transform.forward * InputManager.Instance.MovementInput * moveSpeed;

        // Kết hợp vận tốc mục tiêu với vận tốc Y hiện tại
        targetVelocity.y = currentYVelocity;

        // Đặt vận tốc cho Rigidbody
        this.rb.linearVelocity = targetVelocity;
    }

    private void Turn()
    {
        float turnAmount = InputManager.Instance.TurnInput * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
        this.rb.MoveRotation(this.rb.rotation * turnRotation);
    }
}