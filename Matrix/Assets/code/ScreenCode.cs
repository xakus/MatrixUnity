using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public int step=10;
    private Color[,] lightMatrix;
    private Text[,] symbols;
    private bool threadFlag = true;
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

        mX = (int)(width / symbolWidth * step);
        mY = (int)(height / symbolHeight);
        Debug.Log("mX=" + mX);
        lightMatrix = new Color[mX, mY];
        symbols = new Text[mX, mY];
        for (int i=0;i<mX; i += step)
        {
            for (int j=0;j<mY;j++)
            {
                lightMatrix[Random.Range(0, mX), Random.Range(0, mY)] = new Color32(0, 255, 0,0);
                Text newSym= Instantiate(symbol, new Vector3((i*symbolWidth),(-(j*symbolHeight))+ height, 0),symbol.transform.rotation,this.transform);
                newSym.color =  lightMatrix[i, j];
                 newSym.name="x="+i+" y="+j;
                symbols[i, j] = newSym;
            }
        }
        Thread t = new Thread(thr);
        t.Start();
    }
  
    // Update is called once per frame
    int x=0, y=0;
	void Update () {
        if (Input.GetButtonDown("q"))
        {
            Application.Quit();
            Debug.Log("q");
        }
        lightSuber = Random.Range(0.01f, 0.08f);
        time+= Time.deltaTime;
        
        if(time>0.01)
        {
            for (int i = 0; i < mX; i += step) {
                if (Random.RandomRange(1f, 100f) > 98)
                {
                    lightMatrix[i, 0].a = 1f; 
                }
            }
            time = 0;
        }
         
        for (int i = 0; i < mX; i += step)
        {
            for (int j = 0; j < mY; j++)
            {
                if (lightMatrix[i, j].a >=0.98f)
                {
                    lightMatrix[i, j] = Color.white;

                }
                else
                {
                    lightMatrix[i, j] = new Color(0f, 1f, 0f, lightMatrix[i, j].a);
                }
                symbols[i, j].color = lightMatrix[i, j];

            }
        }
            
       
    }

    private void thr()
    {
        while (threadFlag)
        {
            
            for (int i = 0; i < mX; i += step)
            {
                
                  for (int l = mY - 1; l >= 0; l--)
                     {
                    
                        if ((l + 1) != mY)
                        {
                        Thread.SpinWait(5000);
                            lightMatrix[i, l + 1].a = lightMatrix[i, l].a;
                            lightMatrix[i, l].a -= lightSuber;

                        }
                      
                     }
                   
                
            }
               
          
        }
    }
    private void OnApplicationQuit()
    {
        threadFlag = false;
    }
}


