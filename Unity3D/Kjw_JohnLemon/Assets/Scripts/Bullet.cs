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
        //������Ʈ�� 3d��ǥ�� 2d��ǥ(��ũ����ǥ)�� ��ȯ�Ͽ� GUI�� �׸���.
        Vector3 vPos = this.transform.position;
        Vector3 vPosToScreen = Camera.main.WorldToScreenPoint(vPos); //������ǥ�� ��ũ����ǥ�� ��ȯ�Ѵ�.
        vPosToScreen.y = Screen.height - vPosToScreen.y; //y��ǥ�� ���� �ϴ��� �������� ���ĵǹǷ� ������� ��ȯ�Ѵ�.
        int h = 40;
        int w = 200;
        Rect rectGUI = new Rect(vPosToScreen.x, vPosToScreen.y, w, h);
        //GUI.Box(rectGUI, "MoveBlock:" + isMoveBlock);
        GUI.Box(rectGUI, string.Format("Demag:{0}", m_nDemage));
    }
}
