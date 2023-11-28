using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextRPG;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public Player m_cPlayer = null;
    public Gun m_cGun;
    
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;

    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void OnGUI()
    {
        List<Item> listInventory = m_cPlayer.m_listIventory;

        for (int i = 0; i < listInventory.Count; i++)
        {
            if(GUI.Button(new Rect(0, 20 * i, 100, 20), string.Format("[{0}]:{1}", i, listInventory[i].m_strName)))
            {
                listInventory[i].Use(m_cPlayer);
                listInventory.Remove(listInventory[i]);
            }
        }

        //오브젝트의 3d좌표를 2d좌표(스크린좌표)로 변환하여 GUI를 그린다.
        Vector3 vPos = this.transform.position;
        Vector3 vPosToScreen = Camera.main.WorldToScreenPoint(vPos); //월드좌표를 스크린좌표로 변환한다.
        vPosToScreen.y = Screen.height - vPosToScreen.y; //y좌표의 축이 하단을 기준으로 정렬되므로 상단으로 변환한다.
        int h = 40;
        int w = 100;
        Rect rectGUI = new Rect(vPosToScreen.x, vPosToScreen.y, w, h);
        //GUI.Box(rectGUI, "MoveBlock:" + isMoveBlock);
        GUI.Box(rectGUI, string.Format("HP:{1}\nMP:{0}", m_cPlayer.m_nMp, m_cPlayer.m_nHp));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_cPlayer.m_nMp > 0)
            {
                m_cGun.Shot(m_cPlayer.m_sStatus.nStr);
                m_cPlayer.m_nMp--;
            }
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        m_Movement.Set(horizontal,0f,vertical);
        m_Movement.Normalize();

        // 2개의 값이 유사하면 true , 아니면 false
        // 움직인다면 0이 아니라서 fale지만 !를 썻기때문에 true가 된다.
        // input은 움직일경우 true가 된다.
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        if(hasHorizontalInput || hasVerticalInput)
        {
            m_Rigidbody.velocity = Vector3.zero;    
        }

        m_Animator.SetBool("IsWalking", isWalking);
        if(isWalking)
        {
            if(!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }


        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);

        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
