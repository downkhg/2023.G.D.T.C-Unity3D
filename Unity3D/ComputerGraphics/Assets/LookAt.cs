/*##################################
����Ƽ ������ �������ؿ� Ȱ�� ��ũ��Ʈ(������)
���ϸ�: MeshInfo.cs
�ۼ���: ��ȫ�� (downkhg@gmail.com)
������������¥: 2024.01.17
����: 0.1
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

        ////���� Ȱ���� �Է°���� ���� �����ϱ�: ������ ��ƴٸ� ������ �����ϱ� ����.
        //float fRot = Vector3.Angle(vForward, vToTarget);//�κ����� ������ ���Ѵ�.
        //Vector3 vAsix = RotAsix(vForward, vToTarget);//�ٶ󺸴� ���Ϳ� Ÿ�ٱ����� �Ÿ����͸� Ȱ���Ͽ� ȸ������ ���Ѵ�.

        //���������� �����ϱ�: �ﰢ�Լ��� �����Ѵٸ� ���� ��ü�� ��ġ�� �����ϰ� �پ��� ������ �� �� �ִ�.
        float fRot = Vector3.Dot(vForward, vToTarget) * Mathf.Deg2Rad;//������ �� ������ ������ cos(t)�� ���Ѵ�.
        Vector3 vAsix = Vector3.Cross(vForward, vToTarget); //������ �� ���� ��� ������ ���͸� ���Ѵ�.

        Quaternion qRot = Quaternion.AngleAxis(fRot, vAsix);

        float size = 2;
        Debug.DrawLine(vPos, vPos + vForward * size, Color.red);
        Debug.DrawLine(vPos, vPos + vAsix * size, Color.green);
        Debug.DrawLine(vPos, vPos + vToTarget * size, Color.blue);

        //transform.localRotation *= qRot;//���� ȸ������ �ݿ�(�̰�� ������ ��ġ�̵��� �����ϱ� ����� �ּ���)

        //transform.LookAt(trTarget); //���� �̷��� ��ư� ������ ���������ʾƵ� �Լ��� ���� ���� ����� �����Ǿ��ִ�.
    }
    //��ü�� ����� Ÿ�ٱ����� ���ͷ� ȸ���ϴ� ���� ���� �� �ִ�.
    Vector3 RotAsix(Vector3 forward, Vector3 toTarget)
    {
        return Vector3.Cross(forward, toTarget);
    }
}
