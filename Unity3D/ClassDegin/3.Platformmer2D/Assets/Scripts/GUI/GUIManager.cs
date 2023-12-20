using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public List<GameObject> listGUIScenes;
    public enum E_GUI_STATE { TITLE, THEEND, GAMEOVER, PLAY }
    public E_GUI_STATE curGUIState;

    public GameObject objPopupLayer;

    public GUIIemIventory guiItemIventory;

    public GameManager gameManager;

    public GUIPlayerInfo guiPlayerInfo;

    public void EventItemIventory()
    {
        if (objPopupLayer.activeSelf)
        {
            objPopupLayer.SetActive(false);
        }
        else
        {
            objPopupLayer.SetActive(true);
        }
    }

 
    public void ShowScenec(E_GUI_STATE state)
    {
        for (int i = 0; i < listGUIScenes.Count; i++)
        {
            if ((E_GUI_STATE)i == state)
                listGUIScenes[i].SetActive(true);
            else
                listGUIScenes[i].SetActive(false);
        }
    }
    public void SetGUIScene(E_GUI_STATE state)
    {
        switch (state)
        {
            case E_GUI_STATE.TITLE:
                //단, 타이머관련된 기능과 애니메이션관련 기능이 모두 정지된다.
                //deltaTime * timeScale
                Time.timeScale = 0;
                break;
            case E_GUI_STATE.THEEND:
                Time.timeScale = 0;
                break;
            case E_GUI_STATE.GAMEOVER:
                Time.timeScale = 0;
                break;
            case E_GUI_STATE.PLAY:
                Time.timeScale = 1;
                break;
        }
        ShowScenec(state);
        curGUIState = state;
    }
    public void UpdateState()
    {
        switch (curGUIState)
        {
            case E_GUI_STATE.TITLE:
                break;
            case E_GUI_STATE.THEEND:
                break;
            case E_GUI_STATE.GAMEOVER:
                break;
            case E_GUI_STATE.PLAY:
                {
                    GameObject objPlayer = gameManager.responnerPlayer.objPlayer;

                    if (objPlayer)
                        guiPlayerInfo.UpdateInfo(objPlayer.GetComponent<Player>());

                    if (Input.GetKeyDown(KeyCode.I))
                    {
                        EventItemIventory();
                    }

                    if (objPopupLayer.activeSelf)
                    {
                        if (gameManager.itemIventory.listItems.Count != guiItemIventory.listItemButtens.Count)
                        {
                            guiItemIventory.ClearIventory();
                            guiItemIventory.SetIventory(gameManager.itemIventory);
                        }
                    }
                }
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetGUIScene(curGUIState);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    public void EventChageScene(int stateNumber)
    {
        SetGUIScene((E_GUI_STATE)stateNumber);
    }
}
