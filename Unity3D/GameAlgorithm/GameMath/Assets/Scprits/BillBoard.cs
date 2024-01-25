using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public GameObject m_objCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(m_objCamera.transform);

        //Matrix4x4 matCam = m_objCamera.transform.localToWorldMatrix;
        //Matrix4x4 mat = this.transform.localToWorldMatrix;

        //mat.m00 = matCam.m00;
        //mat.m02 = matCam.m02;
        //mat.m20 = matCam.m20;
        //mat.m20 = matCam.m20;

        ////this.transform.localToWorldMatrix = mat; //setter가 없어 변경이 불가능하다.
    }
}
