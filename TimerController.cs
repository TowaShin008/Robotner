using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text timeText;

    public float totalTime;
    int seconds;
    //クリアフラグ
    bool crearFlag=false;

    void Start()
    {
        totalTime = 0;
        crearFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (crearFlag == false)
        {
            totalTime += Time.deltaTime;
        }    
        seconds = (int)totalTime;    
        timeText.text = seconds.ToString();
    }
}
