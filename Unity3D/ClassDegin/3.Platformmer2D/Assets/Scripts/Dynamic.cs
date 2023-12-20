using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic : MonoBehaviour
{
    public float Speed;
    public float JumpPower;
    public bool isJump;

    public bool isLodder;
    public bool isTakeLodder;

    public int Score;

    public Gun gun;
    public Vector3 dir = Vector3.right;

    Rigidbody2D rigidbody;

    public LayerMask jumpLayer;
    public float rayDist = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        gun.master = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        Vector3 vOrigin = this.transform.position;
        RaycastHit2D raycastHit = 
            Physics2D.Raycast(vOrigin, Vector3.down, rayDist, jumpLayer);

        if (raycastHit)
        {
            //Debug.Log("hit:"+raycastHit.collider.gameObject.name);
            isJump = false;
            Debug.DrawLine(vOrigin, vOrigin + Vector3.down * rayDist, Color.red);
        }
        else
        {
            isJump = true;
            Debug.DrawLine(vOrigin, vOrigin + Vector3.down * rayDist, Color.blue);
        }
    }

    public void Move(Vector3 dir)
    {
        transform.position += dir * Speed * Time.deltaTime;
        //transform.Rotate(Vector3.up * 0);
        if (dir == Vector3.right)
            transform.rotation = Quaternion.Euler(Vector3.up * 0);
        else if(dir == Vector3.left)
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        this.dir = dir;
    }

    public void Jump()
    {
        if (isJump == false)
        {
            Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
            rigidbody.gravityScale = 1;
            rigidbody.velocity = Vector2.zero;//기존속도제거
            rigidbody.AddForce(Vector3.up * JumpPower);
            isJump = true;
        }
    }

    public void Attack()
    {
        gun.Shot(dir);
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position += Vector3.right * Speed * Time.deltaTime;
            ////transform.Rotate(Vector3.up * 0);
            //transform.rotation = Quaternion.Euler(Vector3.up * 0);
            //dir = Vector3.right;
            Move(Vector3.right);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position += Vector3.left * Speed * Time.deltaTime;
            ////transform.Rotate(Vector3.up * 180);
            //transform.rotation = Quaternion.Euler(Vector3.up * 180);
            //dir = Vector3.left;
            Move(Vector3.left);
        }
        if (isLodder)
        {
            if (isTakeLodder)
            {
                //위아래로 움직이도록 만들기
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.position += Vector3.up * Speed * Time.deltaTime;
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.position += Vector3.down * Speed * Time.deltaTime;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("Input.GetKeyDown(KeyCode.UpArrow)");
                rigidbody.velocity = Vector2.zero;
                rigidbody.gravityScale = 0;
                isTakeLodder = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJump == false)
            {
                Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
                rigidbody.gravityScale = 1;
                rigidbody.velocity = Vector2.zero;//기존속도제거
                rigidbody.AddForce(Vector3.up * JumpPower);
                isJump = true;
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
            transform.position += Vector3.down * Speed * Time.deltaTime;
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 20), "Score:" + Score);
        GUI.Box(new Rect(0, 20, 100, 20), "Jump:" + isJump);
        //GUI.Box(new Rect(0, 20, 100, 20), "Lodder:" + isLodder);
        //GUI.Box(new Rect(0, 40, 100, 20), "Gravity:" + GetComponent<Rigidbody2D>().gravityScale);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //isJump = false;
        //Debug.Log("OnCollisionEnter2D:"+collision.gameObject);
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{

    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Item")
    //    {
    //        Score++;
    //        Destroy(collision.gameObject);
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerEnter2D:"+collision.name);
        if(collision.gameObject.tag == "Lodder")
        {
            isLodder = true;
            rigidbody.velocity = Vector2.zero;//기존속도제거
            //GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerEnter2D:" + collision.name);
        if (collision.gameObject.tag == "Lodder")
        {
            isLodder = false;
            isTakeLodder = false;
            rigidbody.gravityScale = 1;
            rigidbody.velocity = Vector2.zero;//기존속도제거
        }
    }
}
