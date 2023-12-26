using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string strVer = "Ver.0.00.00";
    public Text textVer;

    public CameraTracker cameraTracker;

    public Responner responnerPlayer;
    public Responner responnerOpossum;
    public Responner responnerEagle;

    public float DeathZoneY = -1;

    public ItemIventory itemIventory;

    public MobileController mobileController;

    public GUIManager guiManager;
    public ItemDataManager itemDataManager;
    public EffectDataManager effectDataManager;

    public void EventEatItem(ItemData itemData)
    {
        itemIventory.SetIventory(itemData);
    }

    public void EventItemUse(ItemData itemData, GameObject obj)
    {
        itemData.item_effect.Use(obj);
        itemIventory.RemoveIventory(itemData);
    }

    public void EventItemUsePlayer(ItemData itemData)
    {
        if (responnerPlayer.objPlayer)
        {
            itemData.item_effect.Use(responnerPlayer.objPlayer);
            itemIventory.RemoveIventory(itemData);
        }
    }

    public void DeathZoneGizmo()
    {
        Vector3 vStartPos = new Vector3(-9999999999, DeathZoneY, 0);
        Vector3 vEndPos = new Vector3(9999999999, DeathZoneY, 0);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(vStartPos, vEndPos);
    }

    //싱글톤: 게임관리자를 어떤객체든 쉽게 접근하게 만들수있다.
    static GameManager instance;
    public static GameManager GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        textVer.text = strVer;
        //itemDataManager.InitItemData(effectDataManager);
        effectDataManager.InitEffectData();
        itemDataManager.InitItemDataAsset(effectDataManager);
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraTracker.objTarget == null)
            cameraTracker.objTarget = responnerPlayer.objPlayer;

        if (mobileController.dynamic == null && responnerPlayer.objPlayer)
            mobileController.dynamic = responnerPlayer.objPlayer.GetComponent<Dynamic>();

        UpdateEagleRetrunPointCheck();  
    }

    private void UpdateEagleRetrunPointCheck()
    {
        if(responnerEagle.objPlayer)
        {
            Eagle eagle = responnerEagle.objPlayer.GetComponent<Eagle>();
            if(eagle.objResponPoint ==null)
                eagle.objResponPoint = responnerEagle.gameObject;
        }
    }

    private void OnDrawGizmos()
    {
        DeathZoneGizmo();
    }  
}