using UnityEngine;

public class NPCDetection : MonoBehaviour
{
    [Header("Detection Settings")]
    [Tooltip("Ph?m vi t?i ?a ?? phát hi?n m?c tiêu.")]
    public float detectionRadius = 20f;
    [Tooltip("Góc nhìn phía tr??c ?? phát hi?n (??). 360 ?? nhìn xung quanh.")]
    [Range(0f, 360f)]
    public float detectionAngle = 90f;
    [Tooltip("Layer Mask ch?a ??i t??ng Player.")]
    public LayerMask playerLayerMask;
    [Tooltip("Layer Mask ch?a các v?t c?n (t??ng, ?á,...).")]
    public LayerMask obstacleLayerMask;
    [Tooltip("Transform g?c ?? th?c hi?n ki?m tra t?m nhìn (th??ng là m?t ho?c tháp pháo). N?u null, dùng transform c?a GameObject này.")]
    public Transform sightOrigin;

    private Transform detectedTarget = null;

    void Awake()
    {
        if (sightOrigin == null)
        {
            sightOrigin = transform; // Dùng v? trí c?a GameObject này n?u không có ?i?m nhìn c? th?
        }
    }       
    
    public Transform FindTarget()
    {
        detectedTarget = null; // Reset m?i l?n ki?m tra
        Collider[] targetsInRadius = Physics.OverlapSphere(sightOrigin.position, detectionRadius, playerLayerMask);

        foreach (Collider targetCollider in targetsInRadius)
        {
            Transform targetTransform = targetCollider.transform;
            Vector3 directionToTarget = (targetTransform.position - sightOrigin.position).normalized;

            // 1. Ki?m tra góc nhìn (n?u không ph?i 360 ??)
            if (detectionAngle < 360f)
            {
                float angleToTarget = Vector3.Angle(sightOrigin.forward, directionToTarget);
                if (angleToTarget > detectionAngle / 2f)
                {
                    continue; // M?c tiêu n?m ngoài góc nhìn
                }
            }

            // 2. Ki?m tra v?t c?n (Line of Sight)
            float distanceToTarget = Vector3.Distance(sightOrigin.position, targetTransform.position);
            // B?n m?t tia Raycast t? ?i?m nhìn ??n m?c tiêu
            if (!Physics.Raycast(sightOrigin.position, directionToTarget, distanceToTarget, obstacleLayerMask))
            {
                // Không có v?t c?n -> Phát hi?n thành công!
                detectedTarget = targetTransform;
                break; // D?ng l?i khi tìm th?y m?c tiêu ??u tiên h?p l?
            }
            // N?u có v?t c?n, ti?p t?c vòng l?p ?? ki?m tra các m?c tiêu khác trong ph?m vi
        }

        return detectedTarget;
    }       
}
