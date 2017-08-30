using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Symbol : MonoBehaviour {
    Text text;
    float time;
    float wait;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        time = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= wait)
        {
            wait = Random.Range(0.1f, 5.0f);
            text.text = ((char) Random.Range(50,110)).ToString();
            time = 0.0f;
        }
	}
}
