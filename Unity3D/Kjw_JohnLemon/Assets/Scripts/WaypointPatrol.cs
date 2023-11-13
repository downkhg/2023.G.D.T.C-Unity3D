using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    public Observer observer;

    int m_CurrentWaypointIndex;

    public bool isMoveBlock = false; //�̵����� �ƴ�.
    public bool isStun = false; 

    public float moveBlockTime = 1; //Ÿ�̸��۵��ð�
    public float curTime = -1; //Ÿ�̸��۵����� �ð��� �����ϴ� ����

    void ActiveStun()
    {
        isStun = true;
        ActiveMoveBlock();
        observer.ActiveAtkBlock();
    }

    void CancleStun()
    {
        isStun = false;
        CancleMoveBlock();
        observer.CancleAtkBlock();
    }

    void UpdateTimmer()
    {
        if (curTime >= 0)
        { 
            // 0 >= 1 //F
            if (curTime >= moveBlockTime)
            {
                CancleStun();
                //isMoveBlock = false;
                //curTime = -1;
            }
            else
                curTime += Time.deltaTime;
        }
    }

    void ActiveMoveBlock()
    {
        //if (isMoveBlock == false)
        //    StartCoroutine(ProccessTimmer(moveBlockTime));
        isMoveBlock = true;
        curTime = 0;
    }

    void CancleMoveBlock()
    {
        curTime = -1;
        isMoveBlock = false;
        //�̵������� ��ҵǵ��� �����
    }

  

    void Start()
    {
        Vector3 vFirstWayPointPos = waypoints[0].position;
        navMeshAgent.SetDestination(vFirstWayPointPos);
    }

    void Update()
    {
        UpdateTimmer();
        if (!isMoveBlock)
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                int nNextIdx = (m_CurrentWaypointIndex + 1); //�����ε���
                int nUseIdx = nNextIdx % waypoints.Length; //0%2 = 0, 1%2= 1 , 2%2 = 0
                navMeshAgent.SetDestination(waypoints[nUseIdx].position);
                m_CurrentWaypointIndex = nUseIdx;
            }
        }
    }

    private void OnDrawGizmos()
    {
        //remainingDistance�� �ð������� ǥ���ǵ��� ������
        Vector3 vPos = this.transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(vPos, navMeshAgent.remainingDistance);

        Vector3 vWayPointPos = waypoints[m_CurrentWaypointIndex].position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(vWayPointPos, navMeshAgent.stoppingDistance);

        for(int i = 0;  i < waypoints.Length; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(waypoints[i].position, navMeshAgent.stoppingDistance);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet") //�Ѿ˿� �¾�����,
        {
            navMeshAgent.SetDestination(this.gameObject.transform.position); //������ġ�� ���������� �����Ͽ� �̵��� �����ʵ����Ѵ�.
            //isMoveBlock = true; //�̵����� �ߵ�
            //ActiveMoveBlock();
            ActiveStun();
        }
    }

    IEnumerator ProccessTimmer(float time)
    {
        isMoveBlock = true;
        yield return new WaitForSeconds(time);
        isMoveBlock = false;
    }

    private void OnGUI()
    {
        //������Ʈ�� 3d��ǥ�� 2d��ǥ(��ũ����ǥ)�� ��ȯ�Ͽ� GUI�� �׸���.
        Vector3 vPos = this.transform.position;
        Vector3 vPosToScreen = Camera.main.WorldToScreenPoint(vPos); //������ǥ�� ��ũ����ǥ�� ��ȯ�Ѵ�.
        vPosToScreen.y = Screen.height - vPosToScreen.y; //y��ǥ�� ���� �ϴ��� �������� ���ĵǹǷ� ������� ��ȯ�Ѵ�.
        int h = 20;
        int w = 200;
        Rect rectGUI = new Rect(vPosToScreen.x, vPosToScreen.y, w, h);
        //GUI.Box(rectGUI, "MoveBlock:" + isMoveBlock);
        GUI.Box(rectGUI, string.Format("[{0}]:{1}/{2}", isMoveBlock, curTime, moveBlockTime));
    }
}
