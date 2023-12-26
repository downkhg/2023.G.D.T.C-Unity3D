using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIIemIventory : MonoBehaviour
{
    public List<GUIItemButton> listItemButtens;
    public RectTransform rectContent;

    public void AddButton(ItemData itemData, GameObject prefab) //기존에 생성된 프리팹정보를 이용하여 초기화
    {
        //GameObject prefab = Resources.Load("Prefabs/GUI/" + item_kind.ToString()) as GameObject;
        GameObject objButton = Instantiate(prefab, rectContent.transform);
        GUIItemButton guiItemButton = objButton.GetComponent<GUIItemButton>();
        guiItemButton.Set(itemData);
        listItemButtens.Add(guiItemButton);
    }

    public void RemoveButton(GUIItemButton guiItemButton)
    {
        listItemButtens.Remove(guiItemButton);
    }

    public void SetIventory(ItemIventory itemIventory)
    {
        GameObject prefab = Resources.Load("Prefabs/GUI/ItemButton") as GameObject;
        foreach (var item in itemIventory.listItems)
        {
            AddButton(item,prefab);
        }
        ResizeContent();
    }

    public void ClearIventory()
    {
        foreach(var btn in listItemButtens)
        {
            Destroy(btn.gameObject);
        }
        listItemButtens.Clear();
    }

    public void ResizeContent()
    {
        GridLayoutGroup gridLayoutGroup = rectContent.GetComponent<GridLayoutGroup>();
        Vector2 vCellSize = gridLayoutGroup.cellSize;
        Vector2 vSpacing = gridLayoutGroup.spacing;
        Vector2 vArea = vCellSize + vSpacing;
        Vector2 vContentSize = rectContent.sizeDelta;
        int nButtonCount = listItemButtens.Count;
        //500 // 95+5 = 5 //18/5 = 3 // 18 % 5 = 3 //3++ 4
        int nCow = (int)vContentSize.x / (int)(vArea.x);
        int nRaw = nButtonCount / nCow;
        if (nButtonCount % nCow > 0) nRaw++;
        float fHeight = nRaw * vArea.y;
        vContentSize.y = fHeight;
       //rectContent.sizeDelta.y = fHeight;//구조체는 정적할당되므로 참조할수없다.
        rectContent.sizeDelta = vContentSize;
    }

    // Start is called before the first frame update
    void Start()
    {
        //int nItemKind = 0;
        //for (int i = 0; i < 3; i++)
        //{
        //    //if (nItemKind > 3) nItemKind = 0;
        //    AddButton((Item.ITEM_KIND)i);
        //    //nItemKind++;
        //}
        //ResizeContent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
