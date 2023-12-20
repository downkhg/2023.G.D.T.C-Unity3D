using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct PlayerStatus
{
    public int nMP;
    public int nHP;
    public int nDemage;

    public static PlayerStatus operator+(PlayerStatus a, PlayerStatus b)
    {
        PlayerStatus temp;
        temp.nDemage = a.nDemage + b.nDemage;
        temp.nMP = a.nMP + b.nMP;
        temp.nHP = a.nHP + b.nHP;
        return temp;
    }

    public static PlayerStatus operator -(PlayerStatus a, PlayerStatus b)
    {
        PlayerStatus temp;
        temp.nDemage = a.nDemage - b.nDemage;
        temp.nMP = a.nMP - b.nMP;
        temp.nHP = a.nHP - b.nHP;
        return temp;
    }
}

public class Player : MonoBehaviour
{
    public int nMaxMP;
    public int nMaxHP;
    public PlayerStatus playerStatus;
    public PlayerStatus incPlayerStatus;

    public int nExp;
    public int nMaxExp = 100;
    public int nLv = 1;

    public GUIStatusBar guiHPBar;
    public GUIStatusBar guiMPBar;
    public GUIStatusBar guiExpBar;

    public void UpdateStatusBar()
    {
        if(guiHPBar) guiHPBar.SetValue(playerStatus.nHP, nMaxHP);
        if(guiMPBar) guiMPBar.SetValue(playerStatus.nMP, nMaxMP);
        if(guiExpBar) guiExpBar.SetValue(nExp, nMaxExp);
    }
    public void LvUp(PlayerStatus inc)
    {
        if(nExp >= nMaxExp)
        {
            playerStatus += inc;
            nMaxHP += inc.nHP;
            nMaxMP += inc.nMP;
            nExp -= nMaxExp;
            nLv++;
        }
    }
    public void StillExp(Player target)
    {
        Debug.Log("StillExp:"+target.gameObject.name);
        this.nExp += (target.nExp + target.nLv * 100);
    }
    public void Attack(Player target)
    {
        target.playerStatus.nHP = target.playerStatus.nHP - this.playerStatus.nDemage;
    }
    public bool Death()
    {
        if (playerStatus.nHP > 0)
            return false;
        else
            return true;
    }

    public bool Recovery()
    {
        if (playerStatus.nHP == nMaxHP)
            return false;
        else
            playerStatus.nHP = nMaxHP;
        return true;
    }

    public int nDebugIdx;
    private void OnGUI()
    {
        Vector2 vPos = Vector2.zero;
        Vector2 vSize = new Vector2(100, 20);
        int nLine = 0;
        GUI.Box(new Rect(vPos.x + (vSize.x * nDebugIdx), vPos.y + (vSize.y * nLine), vSize.x, vSize.y), "Name:" + gameObject.name); nLine++;
        GUI.Box(new Rect(vPos.x + (vSize.x * nDebugIdx), vPos.y + (vSize.y*nLine) , vSize.x, vSize.y), "HP:" + playerStatus.nHP); nLine++;
        GUI.Box(new Rect(vPos.x + (vSize.x * nDebugIdx), vPos.y + (vSize.y * nLine), vSize.x, vSize.y), "Demage:" + playerStatus.nDemage); nLine++;
        GUI.Box(new Rect(vPos.x + (vSize.x * nDebugIdx), vPos.y + (vSize.y * nLine), vSize.x, vSize.y), "Lv/Exp:" + nLv + "/" + nExp); nLine++;
    }

    // Start is called before the first frame update
    void Start()
    {
        nMaxHP = playerStatus.nHP;
        nMaxMP = playerStatus.nMP;
    }
    // Update is called once per frame
    void Update()
    {
        if (Death())
            Destroy(this.gameObject);

        LvUp(incPlayerStatus);
        UpdateStatusBar();
    }
}
