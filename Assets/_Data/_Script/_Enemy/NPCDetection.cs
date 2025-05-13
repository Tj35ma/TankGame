using UnityEngine;

public class NPCDetection : MonoBehaviour
{
    [Header("Detection Settings")]
    [Tooltip("Ph?m vi t?i ?a ?? ph�t hi?n m?c ti�u.")]
    public float detectionRadius = 20f;
    [Tooltip("G�c nh�n ph�a tr??c ?? ph�t hi?n (??). 360 ?? nh�n xung quanh.")]
    [Range(0f, 360f)]
    public float detectionAngle = 90f;
    [Tooltip("Layer Mask ch?a ??i t??ng Player.")]
    public LayerMask playerLayerMask;
    [Tooltip("Layer Mask ch?a c�c v?t c?n (t??ng, ?�,...).")]
    public LayerMask obstacleLayerMask;
    [Tooltip("Transform g?c ?? th?c hi?n ki?m tra t?m nh�n (th??ng l� m?t ho?c th�p ph�o). N?u null, d�ng transform c?a GameObject n�y.")]
    public Transform sightOrigin;

    private Transform detectedTarget = null;

    void Awake()
    {
        if (sightOrigin == null)
        {
            sightOrigin = transform; // D�ng v? tr� c?a GameObject n�y n?u kh�ng c� ?i?m nh�n c? th?
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

            // 1. Ki?m tra g�c nh�n (n?u kh�ng ph?i 360 ??)
            if (detectionAngle < 360f)
            {
                float angleToTarget = Vector3.Angle(sightOrigin.forward, directionToTarget);
                if (angleToTarget > detectionAngle / 2f)
                {
                    continue; // M?c ti�u n?m ngo�i g�c nh�n
                }
            }

            // 2. Ki?m tra v?t c?n (Line of Sight)
            float distanceToTarget = Vector3.Distance(sightOrigin.position, targetTransform.position);
            // B?n m?t tia Raycast t? ?i?m nh�n ??n m?c ti�u
            if (!Physics.Raycast(sightOrigin.position, directionToTarget, distanceToTarget, obstacleLayerMask))
            {
                // Kh�ng c� v?t c?n -> Ph�t hi?n th�nh c�ng!
                detectedTarget = targetTransform;
                break; // D?ng l?i khi t�m th?y m?c ti�u ??u ti�n h?p l?
            }
            // N?u c� v?t c?n, ti?p t?c v�ng l?p ?? ki?m tra c�c m?c ti�u kh�c trong ph?m vi
        }

        return detectedTarget;
    }       
}
