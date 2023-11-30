using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextRPG;

public class ItemBox : MonoBehaviour
{
    public TextRPG.ItemManager.E_ITEM m_eItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

       if(playerMovement)
        {
            Item item = GameManager.GetInstance().ItemManager.GetItem(m_eItem);
            playerMovement.m_cPlayer.SetIventoryItem(item);
            Destroy(gameObject);
        }
    }
}
