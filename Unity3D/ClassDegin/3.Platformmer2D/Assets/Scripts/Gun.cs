using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject objBullet;
    public Transform trMozzle;
    public float ShotPower;
    public int nBulletCount = 5;
    public bool isShot;
    public float Time;

    public enum E_GUN_STATE {NONE, BULLET_GUN, LASER_GUN }
    public E_GUN_STATE eGunState;

    public Player master;
    public Collider2D hitTarget;

    LineRenderer lineRenderer;

    IEnumerator ProcessShotTimmer()
    {
        isShot = true;
        while(isShot)
        {
            if (hitTarget)
            {
                Player target = hitTarget.GetComponent<Player>();
                SuperMode superMode = target.GetComponent<SuperMode>();

                if (target && superMode && superMode.isUes == false)
                {
                    master.Attack(target);
                    superMode.OnMode();
                }
            }
            yield return new WaitForSeconds(Time);
        }
        isShot = false;
    }

    public void Shot(Vector3 dir)
    {
        switch(eGunState)
        {
            case E_GUN_STATE.BULLET_GUN:
                BulletGun(dir, master);
                break;
            case E_GUN_STATE.LASER_GUN:
                lineRenderer.SetPosition(0, trMozzle.position);
                lineRenderer.SetPosition(1, trMozzle.position);
                if (isShot == false)
                {
                    isShot = true;
                    StartCoroutine(ProcessShotTimmer());
                }
                else
                {
                    isShot = false;
                }
                Debug.Log("Shot:" + isShot);
                break;
        }
    }

    public void FixedUpdate()
    {
        switch (eGunState)
        {
            case E_GUN_STATE.BULLET_GUN:
     
                break;
            case E_GUN_STATE.LASER_GUN:
                if (isShot)
                {
                    LaserGun(master.GetComponent<Dynamic>().dir, master);
                    Debug.Log("E_GUN_STATE.LASER_GUN");
                }
                break;
        }
    }

    public void BulletGun(Vector3 dir, Player player)
    {
        if (nBulletCount > 0)
        {
            GameObject objCopyBullet = Instantiate(objBullet);
            objCopyBullet.transform.position = trMozzle.position;
            Rigidbody2D rigidbody = objCopyBullet.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(dir * ShotPower);
            Bullet bullet = objCopyBullet.GetComponent<Bullet>();
            bullet.master = player;
            bullet.gun = this;
            nBulletCount--;
        }
    }

    public void LaserGun(Vector3 dir, Player player)
    {
        if (nBulletCount > 0)
        {
            float fDist = 999999;
            Vector3 vPos = trMozzle.position;
            int nLayer = 1 << LayerMask.NameToLayer("Monster");
            RaycastHit2D raycastHit = Physics2D.Raycast(vPos, dir, fDist, nLayer);
            Vector3 vEndDist = vPos + dir * fDist;

            if (raycastHit.collider)
            {
                Debug.DrawLine(vPos, raycastHit.point, Color.red);
                lineRenderer.SetPosition(0, vPos);
                lineRenderer.SetPosition(1, raycastHit.point);
                hitTarget = raycastHit.collider;
            }
            else
            {
                Debug.DrawLine(vPos, vEndDist, Color.blue);
                lineRenderer.SetPosition(0, vPos);
                lineRenderer.SetPosition(1, vEndDist);
                hitTarget = null;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Dynamic dynamic = master.GetComponent<Dynamic>();
        switch (eGunState)
        {
            case E_GUN_STATE.BULLET_GUN:
                if (Input.GetKeyDown(KeyCode.X))
                    Shot(dynamic.dir);
                break;
            case E_GUN_STATE.LASER_GUN:
                
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Shot(dynamic.dir);
                    Debug.Log("Gun.Udate.LASER_GUN Down");
                }
                if (Input.GetKeyUp(KeyCode.X))
                {
                    Shot(dynamic.dir);
                    Debug.Log("Gun.Udate.LASER_GUN Gun");
                }
                break;
        }
    }
}
