using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public TextRPG.Player m_cPlayer;

    int m_CurrentWaypointIndex;
    public bool m_isPatrol = false;

    public bool m_isAuto = false;

    public float m_fAngle = 90;
    public float m_fSite = 3;
    public LayerMask m_LayerMask;
    public GameObject m_objTarget = null;

    bool m_isHit;

    void ArcTrigger()
    {
        float fHalfAngle = m_fAngle / 2;
        Vector3 vPos = transform.position;
        Vector3 vForward = transform.forward;
        Quaternion quaternionRight = Quaternion.AngleAxis(fHalfAngle, transform.up);
        Quaternion quaternionLeft = Quaternion.AngleAxis(fHalfAngle, transform.up * -1);
        Vector3 vRight = quaternionRight * vForward;
        Vector3 vLeft = quaternionLeft * vForward;
        Vector3 vRightPos = vPos + vRight * m_fSite;
        Vector3 vLeftPos = vPos + vLeft * m_fSite;


        Debug.DrawLine(vPos, vLeftPos, Color.red);
        Debug.DrawLine(vPos, vRightPos, Color.red);
        Debug.DrawRay(vPos, vForward * m_fSite, Color.yellow);
        int nLayer = 1 << LayerMask.NameToLayer("Player");
        Collider[] colliders =
            Physics.OverlapSphere(vPos, m_fSite, m_LayerMask);

        foreach (Collider collider in colliders)
        {
            //if (collider.tag == "Enemy")
            {
                Vector3 vTargetPos = collider.transform.position;
                Vector3 vToTarget = vTargetPos - vPos;

                float fTargetAngle = Vector3.Angle(vForward, vToTarget);
                float fRightAngle = Vector3.Angle(vForward, vRight);
                float fLeftAngle = Vector3.Angle(vForward, vLeft);

                Debug.Log(collider.gameObject.name + " TargetAngle:" + fTargetAngle + "/" + fHalfAngle + "(" + fRightAngle + "/" + fLeftAngle + ")");
                if (fTargetAngle < fHalfAngle)
                {
                    Debug.DrawLine(vPos, vTargetPos, Color.green);
                    m_objTarget = collider.gameObject;
                    m_isPatrol = false;
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

    private void FixedUpdate()
    {
        ArcTrigger();
    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (m_isAuto)
        {
            if (m_isPatrol)
            {
                m_objTarget = waypoints[0].gameObject;
                Vector3 vFirstWayPointPos = m_objTarget.transform.position;
                navMeshAgent.SetDestination(vFirstWayPointPos);
            }
            else
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isAuto)
        {
            if (m_isPatrol)
            {
                if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
                {
                    int nNextIdx = (m_CurrentWaypointIndex + 1); //다음인덱스
                    int nUseIdx = nNextIdx % waypoints.Length; //0%2 = 0, 1%2= 1 , 2%2 = 0
                    m_objTarget = waypoints[nUseIdx].gameObject;
                    navMeshAgent.SetDestination(m_objTarget.transform.position);
                    //navMeshAgent.SetDestination(waypoints[nUseIdx].position);
                    m_CurrentWaypointIndex = nUseIdx;
                }
            }
            else
            {
                if (m_objTarget)
                    navMeshAgent.SetDestination(m_objTarget.transform.position);
                else
                    m_isPatrol = true;
            }
        }
    }

    

    private void OnDrawGizmos()
    {
        if (m_isPatrol && navMeshAgent && waypoints.Length > 0)
        {
            //remainingDistance를 시각적으로 표현되도록 만들어보기
            Vector3 vPos = this.transform.position;
            Gizmos.color = Color.yellow;
            //Gizmos.DrawWireSphere(vPos, navMeshAgent.remainingDistance);

            Vector3 vWayPointPos = m_objTarget.transform.position;
            Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(vWayPointPos, navMeshAgent.stoppingDistance);

            for (int i = 0; i < waypoints.Length; i++)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(waypoints[i].position, navMeshAgent.stoppingDistance);
            }
        }

        Gizmos.DrawWireSphere(transform.position, m_fSite);
    }

    //private void OnTriggerEnter(Collider other)
    private void OnTriggerStay(Collider other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        if (playerMovement != null)
            m_cPlayer.Attack(playerMovement.m_cPlayer);
    }
}
