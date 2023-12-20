using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Responner : MonoBehaviour
{
    public GameObject objPlayer;
    public bool isRespon = false;
    public string strPrefabName;
    public float Time = 1;
    // Start is called before the first frame update
    void Start()
    {
        strPrefabName = objPlayer.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (objPlayer == null)// && isRespon == false)
        {
            if (isRespon == false)
                StartCoroutine(ProcessTimmer());
        }
    }

    IEnumerator ProcessTimmer()
    {
        //Debug.Log("ProcessTimmer 1");
        isRespon = true;
        yield return new WaitForSeconds(Time);
        GameObject prefabPlayer = Resources.Load("Prefabs/"+strPrefabName) as GameObject;
        objPlayer = Instantiate(prefabPlayer, this.transform.position, Quaternion.identity);
        objPlayer.name = prefabPlayer.name;
        ////독수리만을 위한 기능을 리스포너에 추가하는것은 비효률적이다.
        //Eagle eagle = objPlayer.GetComponent<Eagle>();
        //if(eagle)
        //    eagle.objResponPoint = this.gameObject;
        isRespon = false;
        //Debug.Log("ProcessTimmer 2");
    }
}
