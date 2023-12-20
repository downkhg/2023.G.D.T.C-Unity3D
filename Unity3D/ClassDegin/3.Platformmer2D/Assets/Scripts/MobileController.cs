using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    public Dynamic dynamic;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public string jumpButton = "Jump";
    public string aButton = "A";


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("MobileController.Start():"+gameObject.name);
        dynamic = GetComponent<Dynamic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == false) return;

        float inputHorizontal = SimpleInput.GetAxis(horizontalAxis);
        float inputVertical = SimpleInput.GetAxis(verticalAxis);

        if(inputHorizontal > 0)
        {
            dynamic.Move(Vector3.right);
            Debug.Log("SimpleInput.horizontalAxis!");
        }
        else if (inputHorizontal < 0)
        {
            dynamic.Move(Vector3.left) ;
            Debug.Log("SimpleInput.verticalAxis!");
        }

        if (SimpleInput.GetButtonDown(jumpButton))
        {
            dynamic.Jump();
            Debug.Log("SimpleInput.Jump!");
        }

        AttackKeyProcess(dynamic.gun);
    }

    public void AttackKeyProcess(Gun gun)
    {
        switch(gun.eGunState)
        {
            case Gun.E_GUN_STATE.BULLET_GUN:
                if (SimpleInput.GetButtonDown(aButton))
                {
                    gun.Shot(dynamic.dir);
                    Debug.Log("SimpleInput.aButton!");
                }
                break;
            case Gun.E_GUN_STATE.LASER_GUN:
                if (SimpleInput.GetButtonDown(aButton))
                {
                    gun.Shot(dynamic.dir);
                    Debug.Log("SimpleInput.aButton!");
                }
                if (SimpleInput.GetButtonUp(aButton))
                {
                    gun.Shot(dynamic.dir);
                    Debug.Log("SimpleInput.aButton!");
                }
                break;
        }
    }
}
