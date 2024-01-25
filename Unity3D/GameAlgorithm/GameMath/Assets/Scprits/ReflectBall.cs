using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBall : MonoBehaviour
{
    public Transform trTarget;
    public Vector3 vTargetDir;
    public float Speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 vDist = trTarget.position - transform.position;
        vTargetDir = vDist.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += vTargetDir * Speed * Time.deltaTime;
    }


    Vector3 Reflect(Vector3 vIn, Vector3 vNor)
    {
        Vector3 vReflect = new Vector3();
        return vReflect;
    }


    private void OnCollisionEnter(Collision collision)
    {
        RelfectPlane relfectPlane = collision.gameObject.GetComponent<RelfectPlane>();

        if(relfectPlane)
        {
            vTargetDir = Vector3.Reflect(vTargetDir, relfectPlane.m_Plane.normal);
        }
    }
}
