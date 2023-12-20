using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIStatusBar : MonoBehaviour
{
    public Text textName;
    public RectTransform rectBar;
    public RectTransform rectBackGround;
    public Color colorBar = Color.red;
    public void SetValue(float cur, float max)
    {
        float fRat = cur / max;
        Vector2 vBGSize = rectBackGround.sizeDelta;
        Vector2 vBarSize = rectBar.sizeDelta;
        vBarSize.x = vBGSize.x * fRat;
        rectBar.sizeDelta = vBarSize; 
    }
    public void Init()
    {
        textName.text = gameObject.name;
        Image image = rectBar.GetComponent<Image>();
        image.color = colorBar;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
        //SetValue(50, 100);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
