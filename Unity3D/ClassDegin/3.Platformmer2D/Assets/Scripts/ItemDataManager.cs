using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class ItemData //DTO
{
    public string name;
    public string comment;
    public Item.ITEM_KIND kind;
    public string icon;
    public string effect;
    public string obj;
    public ItemData(string _name, string _conment, Item.ITEM_KIND _kind, string _icon, string _effect, string _obj)
    {
        name = _name;
        comment = _conment;
        kind = _kind;
        icon = _icon;
        effect = _effect;
        obj = _obj;
    }

    public ItemData(string itemdata)
    {
        string[] textItemDatas = itemdata.Split(',');
        Debug.Log("ItemData["+textItemDatas.Length+"]:"+ itemdata);
        if (textItemDatas.Length >= (int)7)
        {
            name = textItemDatas[1];
            comment = textItemDatas[2];
            kind = (Item.ITEM_KIND)int.Parse(textItemDatas[3]) - 1;
            icon = textItemDatas[4];
            effect = textItemDatas[5];
            obj = textItemDatas[6];
        }
    }

    public ItemData(string[] textItemDatas)
    {
        if (textItemDatas.Length >= (int)7)
        {
            name = textItemDatas[1];
            comment = textItemDatas[2];
            kind = (Item.ITEM_KIND)int.Parse(textItemDatas[3]) - 1;
            icon = textItemDatas[4];
            effect = textItemDatas[5];
            obj = textItemDatas[6];
        }
    }
}

public class ItemDataManager : MonoBehaviour
{
    [SerializeField]//프라이빗변수를 인스펙터에 노출할때 사용함.
    List<ItemData> listItemDatas;

    public List<ItemData> ListItemData //인덱서: 프리이빗변수에 인터페이스를 제공할때 사용함.
    { 
        //set { listItemDatas = value; } 
        get{ return listItemDatas; } 
    }

    public ItemData GetItemData(int idx)
    {
        return listItemDatas[idx];
    }

    public ItemData GetItemData(Item.ITEM_KIND item_kind)
    {
        return listItemDatas[(int)item_kind];
    }

    public ItemData GetItemData(string name)
    {
        return listItemDatas.Find(item => item.name == name);
    }

    public void InitItemData()
    {
        //리스트사용시 최대크기를 지정하고 사용하는 것이 좋다.
        listItemDatas = new List<ItemData>((int)Item.ITEM_KIND.MAX);
        listItemDatas.Add(new ItemData("무적", "일정시간동안 데미지를 받지않는다.", Item.ITEM_KIND.SUPER_MODE, "yellow_gem","supe_mode_eff","supermode_obj"));
        listItemDatas.Add(new ItemData("체리", "체력을 모두 회복한다.", Item.ITEM_KIND.RECOVERY, "cherry","eat_eff","recovery_cherry_obj"));
        listItemDatas.Add(new ItemData("총알", "총알을 1개씩 발사한다.", Item.ITEM_KIND.BULLET, "bullet","hit_bullet","bullet_obj"));
        listItemDatas.Add(new ItemData("레이져", "직선상에 적에게 데미지를 준다.", Item.ITEM_KIND.LASER, "laser_gem", "hit_laser", "laser_obj"));
        listItemDatas.Add(new ItemData("수류탄", "수류탄을 던저 주변에 데미지를 준다", Item.ITEM_KIND.GRENADE, "grenade", "explosion_eff", "grenade_obj"));
    }

    public bool InitItemDataAsset()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/ItemData");
        if (textAsset)
        {
            string[] textItemDatas = textAsset.text.Split('\n');
            Debug.Log("InitItemDataAsset["+textItemDatas.Length+"]:"+textItemDatas);
            listItemDatas = new List<ItemData>(textItemDatas.Length);
            for(int i = 1; i < textItemDatas.Length-1; i++)
            {
                string[] lines = textItemDatas[i].Split(',');
                Debug.Log("ItemData[" + lines.Length + "/"+i+"]:" + textItemDatas[i]);
                if (lines.Length >= (int)Item.ITEM_KIND.MAX)
                    listItemDatas.Add(new ItemData(lines));
            }
            return true;
        }
        else
            Debug.LogError("InitItemDataAsset is Fail!!!!");
        return false;
    }

    public void ReleaseItemData()
    {
        listItemDatas.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
