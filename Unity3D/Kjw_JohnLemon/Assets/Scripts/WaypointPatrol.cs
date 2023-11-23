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
    public TextRPG.Player m_cPlayer;

    public Observer observer;

    int m_CurrentWaypointIndex;

    public bool isMoveBlock = false; //이동금지 아님.
    public bool isStun = false; 

    public float moveBlockTime = 1; //타이머작동시간
    public float curTime = -1; //타이머작동까지 시간을 누적하는 변수

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
        //이동금지를 취소되도록 만들기
    }

  

    void Start()
    {
        //player = new TextRPG.Player(this.gameObject.name, 100, 0, 10, 0, 0); //게임관리자에서 설정해주므로 처리할 필요가 없다.
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
                int nNextIdx = (m_CurrentWaypointIndex + 1); //다음인덱스
                int nUseIdx = nNextIdx % waypoints.Length; //0%2 = 0, 1%2= 1 , 2%2 = 0
                navMeshAgent.SetDestination(waypoints[nUseIdx].position);
                m_CurrentWaypointIndex = nUseIdx;
            }
        }

       if( m_cPlayer.Death())
        {
            navMeshAgent.SetDestination(this.gameObject.transform.position); //맞은위치를 도착위지로 변경하여 이동이 되지않도록한다.
            //isMoveBlock = true; //이동금지 발동
            //ActiveMoveBlock();
            ActiveStun();
        }
    }

    private void OnDrawGizmos()
    {
        //remainingDistance를 시각적으로 표현되도록 만들어보기
        Vector3 vPos = this.transform.position;
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(vPos, navMeshAgent.remainingDistance);

        Vector3 vWayPointPos = waypoints[m_CurrentWaypointIndex].position;
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(vWayPointPos, navMeshAgent.stoppingDistance);

        for(int i = 0;  i < waypoints.Length; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(waypoints[i].position, navMeshAgent.stoppingDistance);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet") //총알에 맞았을때,
        {
           
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
        //오브젝트의 3d좌표를 2d좌표(스크린좌표)로 변환하여 GUI를 그린다.
        Vector3 vPos = this.transform.position;
        Vector3 vPosToScreen = Camera.main.WorldToScreenPoint(vPos); //월드좌표를 스크린좌표로 변환한다.
        vPosToScreen.y = Screen.height - vPosToScreen.y; //y좌표의 축이 하단을 기준으로 정렬되므로 상단으로 변환한다.
        int h = 40;
        int w = 200;
        Rect rectGUI = new Rect(vPosToScreen.x, vPosToScreen.y, w, h);
        //GUI.Box(rectGUI, "MoveBlock:" + isMoveBlock);
        GUI.Box(rectGUI, string.Format("[{0}]:{1}/{2}\nHP:{3}", isMoveBlock, curTime, moveBlockTime, m_cPlayer.m_nHp));
    }
}
