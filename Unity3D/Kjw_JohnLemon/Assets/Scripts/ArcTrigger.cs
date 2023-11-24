using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcTrigger : MonoBehaviour
{
    public float m_fAngle = 90;
    public float m_fRadius = 3;

    bool m_isHit;

    public bool CheckHit() { return m_isHit; }

    private void FixedUpdate()
    {
        Vector3 vPos = transform.position;
        Vector3 vForward = transform.forward;
        Quaternion quaternionRight = Quaternion.AngleAxis(m_fAngle / 2, transform.up);
        Quaternion quaternionLeft = Quaternion.AngleAxis(m_fAngle / 2, transform.up * -1);
        Vector3 vRight = quaternionRight * vForward;
        Vector3 vLeft = quaternionLeft * vForward;
        Vector3 vRightPos = vPos + vRight * m_fRadius;
        Vector3 vLeftPos = vPos + vLeft * m_fRadius;

        Debug.DrawLine(vPos, vLeftPos, Color.red);
        Debug.DrawLine(vPos, vRightPos, Color.red);
        Debug.DrawRay(vPos, vForward, Color.red);

        Collider[] colliders = Physics.OverlapSphere(vPos, m_fRadius);

        foreach(Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Vector3 vTargetPos = collider.transform.position;
                Vector3 vToTarget = vTargetPos - vPos;
                Vector3 vToTargetDir = vTargetPos.normalized;
                float fTargetAngle = Vector3.Angle(vToTargetDir, vForward);
                Debug.DrawRay(vPos, vToTarget, Color.green);//방향이 반대로 나옴. 원인 확인 필요

                float fHalfAngle = m_fAngle / 2;
                Debug.Log("Angle:"+ fTargetAngle + "/"+ fHalfAngle);
                if (fTargetAngle < fHalfAngle)
                {
                    Debug.DrawLine(vPos, vTargetPos, Color.green);
                    m_isHit = true;
                }
                else
                {
                    Debug.DrawLine(vPos, vTargetPos, Color.blue);
                    m_isHit = false;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_fRadius);
    }
}
