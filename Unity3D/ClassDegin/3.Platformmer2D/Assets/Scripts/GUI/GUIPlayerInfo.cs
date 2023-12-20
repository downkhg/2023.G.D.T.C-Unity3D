using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIPlayerInfo : MonoBehaviour
{
    public Text textPlayerName;
    public Text textPlayerLv;
    public GUIStatusBar guiPlayerHp;
    public GUIStatusBar guiPlayerMp;
    public GUIStatusBar guiPlayerExp;

    public void UpdateInfo(Player player)
    {
        textPlayerLv.text = string.Format("Lv.{0}", player.nLv);
        textPlayerName.text = player.gameObject.name;
        guiPlayerHp.SetValue(player.playerStatus.nHP, player.nMaxHP);
        guiPlayerMp.SetValue(player.playerStatus.nMP, player.nMaxMP);
        guiPlayerExp.SetValue(player.nExp, player.nMaxExp);
    }
}
