using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AstarManager m_cAStarManager;
    public TileManager m_cTileManager;

    // Start is called before the first frame update
    void Start()
    {
        m_cAStarManager.Init();
        m_cTileManager.Init(m_cAStarManager.SizeX, m_cAStarManager.SizeY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,100,20), "PathFinding()"))
            m_cAStarManager.PathFinding();
    }
}
