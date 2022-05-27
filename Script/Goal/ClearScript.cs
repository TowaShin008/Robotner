using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class ClearScript : MonoBehaviour
{
    public TextMeshProUGUI accessCodeText;
    public TextMeshProUGUI timeText;
    private float processTime;
    private int processTimeINT;
    private bool clearFlag = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (clearFlag)
        {
            accessCodeText.text = string.Format("CLEAR!!!");
            timeText.text = string.Format("time : " + processTimeINT);

            FadeManager.FadeOut(1);

            return;
        }
        processTime += Time.deltaTime;
        processTimeINT = (int)processTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        clearFlag = true;
    }

    public bool GetClearFlag()
    {
        return clearFlag;
    }
}
