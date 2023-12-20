using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIventory : MonoBehaviour
{
    public List<Item.ITEM_KIND> listItems = new List<Item.ITEM_KIND>();

    public void SetIventory(Item.ITEM_KIND item)
    {
        listItems.Add(item);
    }

    public void RemoveIventory(Item.ITEM_KIND item)
    {
        listItems.Remove(item);
    }

    private void OnGUI()
    {
        int w = 100;
        int h = 20;
        Rect rect = new Rect(0,0,w,h);

        for (int i = 0; i< listItems.Count; i++)
        {
            rect.y = h * i;
            if(GUI.Button(rect,i+":"+listItems[i]))
            {
                GameManager.GetInstance().EventItemUsePlayer(listItems[i]);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetIventory(Item.ITEM_KIND.BULLET);
        SetIventory(Item.ITEM_KIND.LASER);
        SetIventory(Item.ITEM_KIND.RECOVERY);
        //SetIventory(Item.ITEM_KIND.GRENADE);
        SetIventory(Item.ITEM_KIND.SUPER_MODE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
