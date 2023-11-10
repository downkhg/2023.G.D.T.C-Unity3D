using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject prefabBullet;
    public float ShotPower = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed. 1");
            GameObject objBullet = Instantiate(prefabBullet);
            objBullet.transform.position = this.gameObject. transform.position;
            objBullet.name = prefabBullet.name;
            objBullet.GetComponent<Rigidbody>().AddForce(transform.forward * ShotPower);
            Debug.Log("Space key was pressed. 2");
        }
    }
}
