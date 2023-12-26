using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int Score;

    ItemData ItemData;

    public bool Use( GameObject obj)
    {
        ItemData.item_effect.Use(obj);
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.GetInstance().EventEatItem(ItemData);
            Destroy(this.gameObject);
        }
    }
}
