using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timmer : MonoBehaviour
{
    int CurTime = 0;
    bool isUse = false;

    IEnumerator ProcessCountTime()
    {
        CurTime = 0;
        isUse = true;
        while(isUse)
        {
            yield return new WaitForSeconds(1);
            CurTime++;
        }
        isUse = false;
    }

    public void Play()
    {
        StartCoroutine(ProcessCountTime());
    }

    public void Stop()
    {
        isUse = false;
    }

    //private void OnGUI()
    //{
    //    if(GUI.Button(new Rect(Screen.width/2 - 50,Screen.height/2 - 50, 100, 100), "Time:" + CurTime))
    //    {
    //        if (isUse)
    //            Stop();
    //        else
    //            Play();
    //    }
    //}
}
