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
        float fHalfAngle = m_fAngle / 2;
        Vector3 vPos = transform.position;
        Vector3 vForward = transform.forward;
        Quaternion quaternionRight = Quaternion.AngleAxis(fHalfAngle, transform.up);
        Quaternion quaternionLeft = Quaternion.AngleAxis(fHalfAngle, transform.up * -1);
        Vector3 vRight = quaternionRight * vForward;
        Vector3 vLeft = quaternionLeft * vForward;
        Vector3 vRightPos = vPos + vRight * m_fRadius;
        Vector3 vLeftPos = vPos + vLeft * m_fRadius;
      

        Debug.DrawLine(vPos, vLeftPos, Color.red);
        Debug.DrawLine(vPos, vRightPos, Color.red);
        Debug.DrawRay(vPos, vForward*m_fRadius, Color.yellow);
        int nLayer = 1 << LayerMask.NameToLayer("Enemy");
        Collider[] colliders = 
            Physics.OverlapSphere(vPos, m_fRadius, nLayer);

        foreach(Collider collider in colliders)
        {
            //if (collider.tag == "Enemy")
            {
                Vector3 vTargetPos = collider.transform.position;
                Vector3 vToTarget = vTargetPos - vPos;
      
                float fTargetAngle = Vector3.Angle(vForward, vToTarget);
                float fRightAngle = Vector3.Angle(vForward, vRight);
                float fLeftAngle = Vector3.Angle(vForward, vLeft);

                Debug.Log(collider.gameObject.name+" TargetAngle:"+ fTargetAngle + "/"+ fHalfAngle + "("+fRightAngle+"/"+fLeftAngle+")");
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

                Debug.DrawRay(vPos, vToTarget, Color.green);//방향이 반대로 나옴. 원인 확인 필요
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_fRadius);
    }
}
