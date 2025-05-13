using UnityEngine;
using System.Collections.Generic; // Để sử dụng List

public class WheelsController : TGMonoBehaviour
{
    [SerializeField] protected float wheelRadius = 0.4f;

    [SerializeField] protected Vector3 localRotationAxisForward = Vector3.right;

    [SerializeField]
    private List<Transform> wheelTransforms = new List<Transform>();
    // Lưu trữ component Wheel tương ứng để không phải GetComponent mỗi frame    
    private List<Wheel> wheelComponents = new List<Wheel>();
    // Lưu trữ góc xoay tiến/lùi tích lũy cho từng bánh xe
    

    [Header("References")]
    [Tooltip("Rigidbody của xe tăng. Nếu để trống, script sẽ cố gắng tự tìm.")]
    [SerializeField] protected Rigidbody tankRigidbody;

    // Biến nội bộ
    [SerializeField] private float circumference;
    

    protected override void Start()
    {
        circumference = 2f * Mathf.PI * wheelRadius;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTankRigidbody();
        this.LoadWheels();
    }

    protected virtual void LoadTankRigidbody()
    {
        if (this.tankRigidbody != null) return;
        this.tankRigidbody = transform.parent.GetComponent<Rigidbody>();
        Debug.Log(transform.name + " :LoadTankRigidbody", gameObject);
    }

    protected virtual void LoadWheels()
    {
        wheelTransforms.Clear();
        wheelComponents.Clear();


        // GetComponentsInChildren<Wheel>(true) sẽ tìm tất cả component Wheel
        // trong GameObject này và tất cả các con của nó, kể cả các con đang không active.
        Wheel[] foundWheels = GetComponentsInChildren<Wheel>(true);
        foreach (Wheel wheelComp in foundWheels)
        {
            if (wheelComp != null && wheelComp.transform != null)
            {
                Transform wheelTransform = wheelComp.transform; // Lấy transform ở đây
                wheelTransforms.Add(wheelTransform);               
            }
        }
        Debug.Log(transform.name + " :LoadWheels", gameObject);
    }

    void Update()
    {
        if (tankRigidbody == null || wheelTransforms.Count == 0 || circumference <= 0)
        {
            // Kiểm tra lại ở Update phòng trường hợp danh sách trống và chưa bị disable ở Awake
            if (wheelTransforms.Count == 0)
            {                
                return;
            }
            return;
        }

        float forwardSpeed = Vector3.Dot(this.tankRigidbody.linearVelocity, transform.parent.forward); // Updated to use linearVelocity
        float deltaRotationAngle = (forwardSpeed / circumference) * 360f * Time.deltaTime;

        foreach (Transform wheelTransform in wheelTransforms)
        {
            if (wheelTransform != null) // Kiểm tra null phòng trường hợp bánh xe bị hủy trong runtime
            {
                // Xoay bánh xe quanh trục cục bộ đã chọn
                wheelTransform.Rotate(localRotationAxisForward, deltaRotationAngle, Space.Self);
            }
        }
    }
       
}