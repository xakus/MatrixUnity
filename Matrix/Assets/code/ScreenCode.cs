using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenCode : MonoBehaviour {

    public int width = 0;
    public int height = 0;
    public bool autoSize = false;
    public Text symbol;
    private float symbolWidth = 0;
    private float symbolHeight = 0;
    public byte lightSuber = 17;
    private int mX = 0;
    private int mY = 0;
    private Color[,] lightMatrix;
    private Text[,] symbols;
    float time;
    // Use this for initialization
    void Start () {
        if (autoSize)
        {
            width= Screen.width;
            height = Screen.height;
        }
        symbolWidth = symbol.rectTransform.rect.width;
        symbolHeight = symbol.rectTransform.rect.height;

        mX =(int)( width / symbolWidth);
        mY = (int)(height / symbolHeight);
        Debug.Log("mX=" + mX);
        lightMatrix = new Color[mX, mY];
        symbols = new Text[mX, mY];
        for (int i=0;i<mX;i++)
        {
            for (int j=0;j<mY;j++)
            {
                lightMatrix[i, j] = new Color32(0, 255, 0, (byte)Random.Range(50,255));
                Text newSym= Instantiate(symbol, new Vector3((i*symbolWidth), (j*symbolHeight)+symbolHeight*2, 0),symbol.transform.rotation,this.transform);
                newSym.color =  lightMatrix[i, j];
                symbols[i,j] = newSym;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        time+= Time.deltaTime;
        if(time>2)
        {
            for (int i = 0; i < mX; i++)
            {
                lightMatrix[i, mY-1].a = Random.Range(254, 256);
            }
        }
        for (int i = 0; i < mX; i++)
        {
            for (int j = 0; j < mY; j++)
            {
                if (lightMatrix[i, j].a >= 220)
                {
                    for (int l=j;l>=0;l--)
                    {
                        if ((l - 1) != -1)
                        {
                            lightMatrix[i, l- 1] = lightMatrix[i, j];
                            lightMatrix[i, l].a -= lightSuber;
                        }
                    }
                }
                symbols[i, j].color = lightMatrix[i, j];
            }
        }
	}
}
