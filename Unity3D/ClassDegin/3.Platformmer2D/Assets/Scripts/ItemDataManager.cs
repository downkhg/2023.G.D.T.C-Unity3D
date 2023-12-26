using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class ItemData //DTO
{
    public string name;
    public string comment;
    public IEffect item_effect;
    public string icon;
    public string effect;
    public string obj;
    public ItemData(string _name, string _conment, IEffect _item_effect, string _icon, string _effect, string _obj)
    {
        name = _name;
        comment = _conment;
        item_effect = _item_effect;
        icon = _icon;
        effect = _effect;
        obj = _obj;
    }

    public ItemData(string itemdata, EffectDataManager effectDataManager)
    {
        string[] textItemDatas = itemdata.Split(',');
        Debug.Log("ItemData["+textItemDatas.Length+"]:"+ itemdata);
        if (textItemDatas.Length >= (int)7)
        {
            name = textItemDatas[1];
            comment = textItemDatas[2];
            int idx = int.Parse(textItemDatas[3]) - 1;
            item_effect = effectDataManager.GetEffect(idx);
            icon = textItemDatas[4];
            effect = textItemDatas[5];
            obj = textItemDatas[6];
        }
    }

    public ItemData(string[] textItemDatas, EffectDataManager effectDataManager)
    {
        if (textItemDatas.Length >= (int)7)
        {
            name = textItemDatas[1];
            comment = textItemDatas[2];
            int idx = int.Parse(textItemDatas[3]) - 1;
            item_effect = effectDataManager.GetEffect(idx);
            icon = textItemDatas[4];
            effect = textItemDatas[5];
            obj = textItemDatas[6];
        }
    }
}

public class ItemDataManager : MonoBehaviour
{
    public enum E_ITEMDATA { SUPER_MODE, CHERRY, BULLET, LASER, GRENADE,MAX }
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

    public ItemData GetItemData(E_ITEMDATA item_data)
    {
        return listItemDatas[(int)item_data];
    }

    public ItemData GetItemData(string name)
    {
        return listItemDatas.Find(item => item.name == name);
    }

    public void InitItemData(EffectDataManager effectDataManager)
    {
        //리스트사용시 최대크기를 지정하고 사용하는 것이 좋다.
        listItemDatas = new List<ItemData>((int)E_ITEMDATA.MAX);
        listItemDatas.Add(new ItemData("무적", "일정시간동안 데미지를 받지않는다.", effectDataManager.GetEffect(EffectDataManager.EffectFucntion.SUPER_MODE), "yellow_gem","supe_mode_eff","supermode_obj"));
        listItemDatas.Add(new ItemData("체리", "체력을 모두 회복한다.", effectDataManager.GetEffect(EffectDataManager.EffectFucntion.RECOVERY), "cherry","eat_eff","recovery_cherry_obj"));
        listItemDatas.Add(new ItemData("총알", "총알을 1개씩 발사한다.", effectDataManager.GetEffect(EffectDataManager.EffectFucntion.BULLET), "bullet","hit_bullet","bullet_obj"));
        listItemDatas.Add(new ItemData("레이져", "직선상에 적에게 데미지를 준다.", effectDataManager.GetEffect(EffectDataManager.EffectFucntion.LASER), "laser_gem", "hit_laser", "laser_obj"));
        listItemDatas.Add(new ItemData("수류탄", "수류탄을 던저 주변에 데미지를 준다", effectDataManager.GetEffect(EffectDataManager.EffectFucntion.GRENADE), "grenade", "explosion_eff", "grenade_obj"));
    }

    public bool InitItemDataAsset(EffectDataManager effectDataManager)
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
                if (lines.Length >= 7)
                    listItemDatas.Add(new ItemData(lines,effectDataManager));
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
