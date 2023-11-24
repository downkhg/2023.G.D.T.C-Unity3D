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

    public bool m_isAuto = false;

    // Start is called before the first frame update
    void Start()
    {
        if (m_isAuto)
        {
            Vector3 vFirstWayPointPos = waypoints[0].position;
            navMeshAgent.SetDestination(vFirstWayPointPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isAuto)
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                int nNextIdx = (m_CurrentWaypointIndex + 1); //다음인덱스
                int nUseIdx = nNextIdx % waypoints.Length; //0%2 = 0, 1%2= 1 , 2%2 = 0
                navMeshAgent.SetDestination(waypoints[nUseIdx].position);
                m_CurrentWaypointIndex = nUseIdx;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (waypoints.Length > 0)
        {
            //remainingDistance를 시각적으로 표현되도록 만들어보기
            Vector3 vPos = this.transform.position;
            Gizmos.color = Color.yellow;
            //Gizmos.DrawWireSphere(vPos, navMeshAgent.remainingDistance);

            Vector3 vWayPointPos = waypoints[m_CurrentWaypointIndex].position;
            Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(vWayPointPos, navMeshAgent.stoppingDistance);

            for (int i = 0; i < waypoints.Length; i++)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(waypoints[i].position, navMeshAgent.stoppingDistance);
            }
        }
    }
}
