using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextRPG;

public class Gun : MonoBehaviour
{
    public GameObject prefabBullet;
    public float ShotPower = 200;

    public void Shot(int demage)
    {
        GameObject objBullet = Instantiate(prefabBullet);
        Bullet bullet = objBullet.GetComponent<Bullet>();
        bullet.SetDemage(demage);
        objBullet.transform.position = this.gameObject.transform.position;
        objBullet.name = prefabBullet.name;
        objBullet.GetComponent<Rigidbody>().AddForce(transform.forward * ShotPower);
    }
}
