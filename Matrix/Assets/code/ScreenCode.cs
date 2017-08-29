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
    public float lightSuber = 170;
    private int mX = 0;
    private int mY = 0;
    public int offsetWidth=10;
    private Color[,] lightMatrix;
    private Text[,] symbols;
    float time;
    float animationTime;
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
                lightMatrix[Random.Range(0, mX), Random.Range(0, mY)] = new Color32(0, 255, 0,255);
                Text newSym= Instantiate(symbol, new Vector3((i*symbolWidth),(-(j*symbolHeight))+ height, 0),symbol.transform.rotation,this.transform);
                newSym.color =  lightMatrix[i, j];
                 newSym.name="x="+i+" y="+j;
                symbols[i, j] = newSym;
            }
        }
    }

    // Update is called once per frame
    int x=0, y=0;
	void Update () {
        time+= Time.deltaTime;
        animationTime += Time.deltaTime;
        if(time>5)
        {
            for (int i = 0; i < mX; i++)
            {
                lightMatrix[i, 0].a = 1;// Random.Range(0.9f, 1f);
            }
            time = 0;
        }
        
            y++;
            if ((x + 1) == mX)
            {
                x = 0;
               
            }
            if ((y + 1) >= mY)
            {
                y = 0;
                x++;
            }
            //if (lightMatrix[x, y].a >= 0.9)
            //{if (animationTime > 0.00001)
            //{
                
            //    for (int l = y; l >=0; l--)
            //    {
            //        if ((l - 1) != mY-1)
            //        {
            //            lightMatrix[x, l + 1].a = lightMatrix[x, l].a;
            //            lightMatrix[x, l].a -= lightSuber;
            //            //lightMatrix[x, l+1].a -= lightSuber;
            //            Debug.Log("Color.a=" + lightMatrix[x, l].a);
            //        }
            //    }
            //    if ((y + 1) != mY)
            //    {
            //        y += 1;
            //    }
            //    animationTime = 0;
            //}
            //}
            
        

            for (int i = 0; i < mX; i++)
            {
                for (int j = 0; j < mY; j++)
                {
                if (lightMatrix[x, y].a >= 0.9)
                {
                    if (animationTime > 0.00001)
                    {

                        for (int l = j; l >= 0; l--)
                        {
                            if ((l - 1) != mY - 1)
                            {
                                lightMatrix[x, l + 1].a = lightMatrix[x, l].a;
                                lightMatrix[x, l].a -= lightSuber;
                                //lightMatrix[x, l+1].a -= lightSuber;
                                Debug.Log("Color.a=" + lightMatrix[x, l].a);
                            }
                        }
                        //if ((j + 1) != mY)
                        //{
                        //    j += 1;
                        //}
                        animationTime = 0;
                    }
                }
                symbols[i, j].color = lightMatrix[i, j];
                }
            }
       	}
}
