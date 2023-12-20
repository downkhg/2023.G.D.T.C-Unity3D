using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    public GameObject objTarget;

    // Update is called once per frame
    void Update()
    {
        if (objTarget)
        {
            Vector3 vPos = this.transform.position;
            Vector3 vTargetPos = objTarget.transform.position;
            vTargetPos.z = vPos.z;

            this.transform.position = vTargetPos;
        }
    }
}
