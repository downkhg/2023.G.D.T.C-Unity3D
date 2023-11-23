using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public PlayerMovement playerMovent;
    //public GameEnding gameEnding;
    public bool isAtkBlock = false;
    public WaypointPatrol waypointPatrol;


    public void ActiveAtkBlock()
    {
        isAtkBlock = true;
    }

    public void CancleAtkBlock()
    {
        isAtkBlock = false;
    }

    private void Start()
    {
        waypointPatrol = transform.parent.GetComponent<WaypointPatrol>();
    }

    private void Update()
    {
        if(playerMovent)
        {
            if (isAtkBlock == false)
            {
                Vector3 direction = playerMovent.transform.position - transform.position + Vector3.up;
                Ray ray = new Ray(transform.position, direction);
                RaycastHit raycastHit;
                if (Physics.Raycast(ray, out raycastHit))
                {
                    if (raycastHit.collider.transform == playerMovent.transform)
                    {
                        waypointPatrol.m_cPlayer.Attack(playerMovent.m_cPlayer);

                        //if(playerMovent.m_cPlayer.Death())
                        //    gameEnding.CaughtPlayer();
                        Debug.DrawLine(this.transform.position, playerMovent.transform.position,Color.green);
                    }     
                }
            }

            Debug.DrawLine(this.transform.position, playerMovent.transform.position, Color.red);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerMovent = other.gameObject.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerMovent = null;
        }
    }
}
