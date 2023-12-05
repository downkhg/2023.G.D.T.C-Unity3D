using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_textName;
    [SerializeField] Button m_btnButton;

    public void SetItemInfo(GUIPanel guiPanel, TextRPG.Item item)
    {
        m_textName.text = item.m_strName;
        m_btnButton.onClick.AddListener(() => { guiPanel.SetItemInfo(item); });
    }

    // Start is called before the first frame update
    void Start()
    {
        //TextRPG.Item item = GameManager.GetInstance().ItemManager.GetItem(TextRPG.ItemManager.E_ITEM.WOOD_WEAPON);
        //SetItemInfo(item);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
