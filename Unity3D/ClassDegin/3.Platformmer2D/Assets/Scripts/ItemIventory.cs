using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIventory : MonoBehaviour
{
    public List<ItemData> listItems = new List<ItemData>();

    public void SetIventory(ItemData item)
    {
        listItems.Add(item);
    }

    public void RemoveIventory(ItemData item)
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
        ItemDataManager itemDataManager = GameManager.GetInstance().itemDataManager;

        SetIventory(itemDataManager.GetItemData(ItemDataManager.E_ITEMDATA.BULLET));
        SetIventory(itemDataManager.GetItemData(ItemDataManager.E_ITEMDATA.SUPER_MODE));
        SetIventory(itemDataManager.GetItemData(ItemDataManager.E_ITEMDATA.LASER));
        SetIventory(itemDataManager.GetItemData(ItemDataManager.E_ITEMDATA.CHERRY));
        //SetIventory(Item.ITEM_KIND.GRENADE);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
