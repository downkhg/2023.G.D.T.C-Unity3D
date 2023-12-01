using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Responner : MonoBehaviour
{
    public GameObject m_prefabObject;
    public GameObject m_objInstance;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (m_objInstance == null)
            m_objInstance = Instantiate(m_prefabObject, this.transform.position, Quaternion.identity);
    }
}
