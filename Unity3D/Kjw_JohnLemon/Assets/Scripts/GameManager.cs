using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextRPG;
using static TextRPG.PlayerManager;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] ItemManager m_cItemManager = new ItemManager();
    [SerializeField] PlayerManager m_cPlayerManager = new PlayerManager();

    public ItemManager ItemManager { get { return m_cItemManager; } }
    public PlayerManager PlayerManager { get { return m_cPlayerManager; } }

    public List<GameObject> m_listGUIScenes;
    public enum E_GUI_STATE { TITLE, THEEND, GAMEOVER, PLAY }
    public E_GUI_STATE m_curGUIState;

    public StatusBar m_guiHPBar;
    public GUIIventory m_guiInventory;


    public bool m_isPopup = false;
    public GameObject m_objPopup;

    public void PopupIventroy(int playerIdx = 0)
    {
        if (m_objPopup.activeSelf == false)
        {
            Player player = m_listPlayer[playerIdx].m_cPlayer;
            m_guiInventory.SetIventory(player);
            m_objPopup.SetActive(true);
        }
        else
        {
            m_objPopup.SetActive(false);
            m_guiInventory.ResetIventoryButton();
        }
    }

    public void EventStart()
    {
        SetGUIScene(E_GUI_STATE.PLAY);
    }

    public void EventGameOver()
    {
        SetGUIScene(E_GUI_STATE.GAMEOVER);
    }

    public void EventEnd()
    {
        SetGUIScene(E_GUI_STATE.THEEND);
    }

    public void EventRetry()
    {
        SetGUIScene(E_GUI_STATE.PLAY);
    }

    public void EventExit()
    {
        Application.Quit();
    }

    public void EventUpdateStatus(int playerIdx = 0)
    {
        Player player = m_listPlayer[playerIdx].m_cPlayer;
        if(player != null)
            m_guiHPBar.SetBar(player.m_nHp, player.m_sStatus.nHP);
    }

    public void EventChageScene(int stateNumber)
    {
        SetGUIScene((E_GUI_STATE)stateNumber);
    }

    public void ShowScenec(E_GUI_STATE state)
    {
        for (int i = 0; i < m_listGUIScenes.Count; i++)
        {
            if ((E_GUI_STATE)i == state)
                m_listGUIScenes[i].SetActive(true);
            else
                m_listGUIScenes[i].SetActive(false);
        }
    }
    public void SetGUIScene(E_GUI_STATE state)
    {
        switch (state)
        {
            case E_GUI_STATE.TITLE:
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
        m_curGUIState = state;
    }
    public void UpdateState()
    {
        switch (m_curGUIState)
        {
            case E_GUI_STATE.TITLE:
                break;
            case E_GUI_STATE.THEEND:
                break;
            case E_GUI_STATE.GAMEOVER:
                break;
            case E_GUI_STATE.PLAY:
                EventUpdateStatus();
                if(Input.GetKeyDown(KeyCode.I))
                {
                    PopupIventroy();
                }

                break;
        }
    }


    public List<PlayerMovement> m_listPlayer;
    public List<EnemyMovement> m_listEnemies;

    public GameEnding m_cGameEnding;

    //public void EventGameOver()
    //{
    //    m_cGameEnding.CaughtPlayer();
    //}

    //public void EventEnd()
    //{
    //    m_cGameEnding.GoalInPlayer();
    //}

    static GameManager m_cInstance;

    public static GameManager GetInstance()
    {
        return m_cInstance;
    }


    private void Awake()
    {
        m_cInstance = this;
        m_cItemManager.Init();
        m_cPlayerManager.Init();

        PlayerMovement playerMovement = m_listPlayer[0].GetComponent<PlayerMovement>();
        playerMovement.m_cPlayer = m_cPlayerManager.GetPlayer(PlayerManager.E_PLAYER.JHON_LEAMON);
        m_cItemManager.SetPlayerAllData(playerMovement.m_cPlayer);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetGUIScene(m_curGUIState);

        foreach(var enemy in m_listEnemies)
        {
            if (enemy.m_isAuto == true)
            {
                enemy.m_cPlayer = m_cPlayerManager.GetPlayer(PlayerManager.E_PLAYER.GHOST);
            }
            else
            {
                enemy.m_cPlayer = m_cPlayerManager.GetPlayer(PlayerManager.E_PLAYER.GARGOYLE);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        PlayerUpdate();
    }
    void PlayerUpdate()
    {
        foreach (var player in m_listPlayer)
        {
            if (player.m_cPlayer.Death())
            {
                GameManager.GetInstance().EventGameOver();
            }
        }
    }
}
