using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollisionFanDetector : MonoBehaviour
{
    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();
    private float m_fSightAngle = 30.0f;
    /// <summary>
    /// Is Trigger��ON�ő���Collider�Əd�Ȃ��Ă���Ƃ��ɌĂ΂ꑱ����
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

    // UnityEvent���p�������N���X��[Serializable]������t�^���邱�ƂŁAInspector�E�C���h�E��ɕ\���ł���悤�ɂȂ�B
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {
    }
}