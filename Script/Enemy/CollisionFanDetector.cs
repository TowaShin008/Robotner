using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollisionFanDetector : MonoBehaviour
{
    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();
    private float m_fSightAngle = 30.0f;
    /// <summary>
    /// Is TriggerがONで他のColliderと重なっているときに呼ばれ続ける
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Robot"))
        {
            Vector3 positionDelta = other.transform.position - transform.position;
            float targetAngle = Vector3.Angle(transform.forward, positionDelta);
            if(targetAngle<m_fSightAngle)
            {
                if (Physics.Raycast(transform.position, new Vector3(positionDelta.x, 0f, positionDelta.z), out RaycastHit hit))
                {
                    if (hit.collider == other)
                    {
                        onTriggerStay.Invoke(other);
                    }
                }
            }
        }
    }

    // UnityEventを継承したクラスに[Serializable]属性を付与することで、Inspectorウインドウ上に表示できるようになる。
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {
    }
}