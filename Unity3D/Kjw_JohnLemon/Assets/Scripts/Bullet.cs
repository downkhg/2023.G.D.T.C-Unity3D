using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextRPG;

public class Bullet : MonoBehaviour
{
    int m_nDemage;

    public void SetDemage(int damage)
    {
        m_nDemage = damage;
    }

    //public int GetDemage()
    //{
    //    return m_nDemage;
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(this.gameObject.name + "OnCollisionEnter:" + collision.gameObject.name);
        if (collision.gameObject.tag == "Enemy")
        {
            WaypointPatrol waypointPatrol = collision.gameObject.GetComponent<WaypointPatrol>();
             waypointPatrol.observer.m_cPlayer.Demeged(m_nDemage);
        }

        Destroy(this.gameObject);
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
        GUI.Box(rectGUI, string.Format("Demag:{0}", m_nDemage));
    }
}
