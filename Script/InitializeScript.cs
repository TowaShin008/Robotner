using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeScript : MonoBehaviour
{
    private int processTime = 0;
    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        FadeManager.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        if(counter>=60)
        {
            processTime++;
        }
    }

    public int GetProcessTime()
    {
        return processTime;
    }
}
