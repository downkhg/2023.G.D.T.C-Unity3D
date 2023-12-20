using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : MonoBehaviour
{
    public float Speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Attack();
    }

    void Attack()
    {
        Vector2 vPos = transform.position;
        BoxCollider2D boxCollider = this.GetComponent<BoxCollider2D>();
        int nLayer = 1 << LayerMask.NameToLayer("Player");
        Collider2D collider =
            Physics2D.OverlapBox(vPos + boxCollider.offset, boxCollider.size, 0, nLayer);

        if (collider)//콜라이더가 있을때
        {
            Player me = this.GetComponent<Player>();
            Player target = collider.gameObject.GetComponent<Player>();
            SuperMode superMode = target.GetComponent<SuperMode>();
            if (superMode && superMode.isUes == false)
            {
                me.Attack(target);
                if (target.Death()) me.StillExp(target);
                superMode.OnMode();
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        //Destroy(collision.gameObject);
    //        Player me = this.GetComponent<Player>();
    //        Player target = collision.gameObject.GetComponent<Player>();

    //        me.Attack(target);
    //    }
    //}
}
