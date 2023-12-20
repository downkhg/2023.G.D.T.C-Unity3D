using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIItemButton : MonoBehaviour
{
    public Text textItemName;
    public Image imgItemSprite;

    public bool Set(Item.ITEM_KIND item_kind)
    {
        Debug.Log("GUIItemButton.Set:" + item_kind);

        ItemData itemData = GameManager.GetInstance().itemDataManager.GetItemData(item_kind);

        if (itemData != null)
        {
            textItemName.text = itemData.name;
            Sprite sprite = Resources.Load<Sprite>("Image/" + itemData.icon);
            if (sprite) imgItemSprite.sprite = sprite;
            Button button = this.GetComponent<Button>();
            button.onClick.AddListener(() => OnClickEvent(item_kind));
            return true;
        }
        return false;
    }

    public void OnClickEvent(Item.ITEM_KIND item_kind)
    {
        Debug.Log("GUIItemButton.OnClickEvent:" + item_kind);
        GameManager.GetInstance().EventItemUsePlayer(item_kind);
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
