using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneObject : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetInstance().DeathZoneY > this.transform.position.y)
            Destroy(this.gameObject);
    }
}
