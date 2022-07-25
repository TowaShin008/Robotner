using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextView : MonoBehaviour
{

    [SerializeField] string text = "New Text";
    [SerializeField] int textSpeed = 60;

    [SerializeField] bool flag = false;

    private int nowTextNum = 0;
    private int counter = 0;
    private string nowText = "";

    // Start is called before the first frame update
    void Start()
    {
        //nowTextNum = text.Length;
        gameObject.GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!flag) return;
        if (nowText.Length == text.Length) return;

        if (counter++ > textSpeed)
        {
            nowText = "";
            for(int i = 0; i < nowTextNum; i++)
            {
                nowText = nowText + text[i];
            }

            gameObject.GetComponent<Text>().text = nowText;
            nowTextNum++;
            counter = 0;
        }
    }
}
