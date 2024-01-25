/*##################################
유니티 내적과 외적이해와 활용 스크립트(수업용)
파일명: MeshInfo.cs
작성자: 김홍규 (downkhg@gmail.com)
마지막수정날짜: 2024.01.17
버전: 0.1
###################################*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform trTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vPos = transform.position;
        Vector3 vTargetPos = trTarget.position;

        Vector3 vToTarget = vTargetPos - vPos;
        Vector3 vForward = transform.forward;

        ////실제 활용의 입력결과를 통한 이해하기: 수학이 어렵다면 이쪽이 이해하기 쉽다.
        //float fRot = Vector3.Angle(vForward, vToTarget);//두벡터의 내각을 구한다.
        //Vector3 vAsix = RotAsix(vForward, vToTarget);//바라보는 벡터와 타겟까지의 거리벡터를 활용하여 회전축을 구한다.

        //수학적으로 이해하기: 삼각함수를 이해한다면 실제 물체의 위치를 이해하고 다양한 응용을 할 수 있다.
        float fRot = Vector3.Dot(vForward, vToTarget) * Mathf.Deg2Rad;//내적은 두 벡터의 내각을 cos(t)를 구한다.
        Vector3 vAsix = Vector3.Cross(vForward, vToTarget); //외적은 두 벡터 모두 수직인 벡터를 구한다.

        Quaternion qRot = Quaternion.AngleAxis(fRot, vAsix);

        float size = 2;
        Debug.DrawLine(vPos, vPos + vForward * size, Color.red);
        Debug.DrawLine(vPos, vPos + vAsix * size, Color.green);
        Debug.DrawLine(vPos, vPos + vToTarget * size, Color.blue);

        //transform.localRotation *= qRot;//실제 회전값을 반영(이경우 벡터의 위치이동을 관찰하기 어려워 주석함)

        //transform.LookAt(trTarget); //굳이 이렇게 어렵게 수학을 이해하지않아도 함수를 통해 같은 기능이 구현되어있다.
    }
    //객체의 방향과 타겟까지의 벡터로 회전하는 축을 구할 수 있다.
    Vector3 RotAsix(Vector3 forward, Vector3 toTarget)
    {
        return Vector3.Cross(forward, toTarget);
    }
}
