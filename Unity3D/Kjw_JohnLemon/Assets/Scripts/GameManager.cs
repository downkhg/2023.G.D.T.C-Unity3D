using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextRPG;
using static TextRPG.PlayerManager;

public class GameManager : MonoBehaviour
{
    ItemManager m_cItemManager = new ItemManager();
    PlayerManager m_cPlayerManager = new PlayerManager();

    public List<PlayerMovement> m_listPlayer;
    public List<Observer> m_listEnemies;

    public GameEnding m_cGameEnding;

    public void EventGameOver()
    {
        m_cGameEnding.CaughtPlayer();
    }

    public void EventEnd()
    {
        m_cGameEnding.GoalInPlayer();
    }

    static GameManager m_cInstance;

    public static GameManager GetInstance()
    {
        return m_cInstance;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_cInstance = this;

        m_cItemManager.Init();
        m_cPlayerManager.Init();

        PlayerMovement playerMovement = m_listPlayer[0].GetComponent<PlayerMovement>();
        playerMovement.m_cPlayer = m_cPlayerManager.GetPlayer(PlayerManager.E_PLAYER.JHON_LEAMON);

        foreach(var observer in m_listEnemies)
        {
            observer.m_cPlayer = m_cPlayerManager.GetPlayer(PlayerManager.E_PLAYER.GHOST);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
