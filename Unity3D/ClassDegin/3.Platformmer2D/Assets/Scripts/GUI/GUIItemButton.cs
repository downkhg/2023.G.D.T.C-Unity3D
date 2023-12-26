using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIItemButton : MonoBehaviour
{
    public Text textItemName;
    public Image imgItemSprite;

    public bool Set(ItemData itemData)
    {
        Debug.Log("GUIItemButton.Set:" + itemData);

        if (itemData != null)
        {
            textItemName.text = itemData.name;
            Sprite sprite = Resources.Load<Sprite>("Image/" + itemData.icon);
            if (sprite) imgItemSprite.sprite = sprite;
            Button button = this.GetComponent<Button>();
            button.onClick.AddListener(() => OnClickEvent(itemData));
            return true;
        }
        return false;
    }

    public void OnClickEvent(ItemData itemData)
    {
        Debug.Log("GUIItemButton.OnClickEvent:" + itemData);
        GameManager.GetInstance().EventItemUsePlayer(itemData);
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
