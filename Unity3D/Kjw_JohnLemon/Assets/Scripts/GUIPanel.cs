using System.Collections;
using System.Collections.Generic;
using TextRPG;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIPanel : MonoBehaviour
{
    [SerializeField] Image m_imgIcon;
    [SerializeField] TextMeshProUGUI m_textComment;

    public void SetItemInfo(TextRPG.Item item)
    {
        string strComment = string.Format("{0}\n°¡°Ý: {1}", item.m_strName, item.m_nPrice);
        m_textComment.text = strComment;
        if(m_imgIcon.sprite) Destroy(m_imgIcon.sprite);
        m_imgIcon.sprite = Resources.Load<Sprite>("RPG_inventory_icons/" + item.m_strIcon);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Item item = GameManager.GetInstance().ItemManager.GetItem(TextRPG.ItemManager.E_ITEM.WOOD_WEAPON);
        //SetItemInfo(item);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
