using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing.Security;
using UnityEngine.UI;

public class GUIIventory : MonoBehaviour
{
    [SerializeField] List<GUIButton> m_guiButtons = new List<GUIButton>();
    [SerializeField] GUIPanel m_guiPanel;
    [SerializeField] GridLayoutGroup m_gridContent;

    public void SetConnentSize()
    {
        float height = m_guiButtons.Count * m_gridContent.cellSize.y;
        RectTransform rectTransform = m_gridContent.GetComponent<RectTransform>();
        //rectTransform.sizeDelta.y = height;
        Vector2 vSize = rectTransform.sizeDelta;
        vSize.y = height;
        rectTransform.sizeDelta = vSize;
    }

    public void SetIventory(TextRPG.Player player)
    {
        Object prefabItemButton = Resources.Load("Prefabs/ItemButton");
        foreach( var item in player.m_listIventory)
        {
            GameObject objButton = Instantiate(prefabItemButton, m_gridContent.transform) as GameObject;
            GUIButton guiButton = objButton.GetComponent<GUIButton>();
            guiButton.SetItemInfo(m_guiPanel, item);
            m_guiButtons.Add(guiButton);
        }
        //Destroy(prefabItemButton);
        SetConnentSize();
    }

    public void ResetIventoryButton()
    {
        foreach(var item in m_guiButtons)
        {
            Destroy(item.gameObject);
        }
        m_guiButtons.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        //TextRPG.Player player = GameManager.GetInstance().m_listPlayer[0].m_cPlayer;
        //SetIventory(player);
        //ResetIventoryButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
