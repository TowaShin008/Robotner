using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameEnd : MonoBehaviour
{
    float timer;
    public GameObject text0;
    public GameObject text1;
    public GameObject text2;

    int rand=0;
    bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        rand = 0;
        text0.SetActive(false);
        text1.SetActive(false);
        text2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (rand == 0)
        {
            rand = Random.Range(1, 4);
        }
        timer += Time.deltaTime;
        if (timer > 5)
        {
            FadeManager.FadeOut("Scene_Title");
        }
        switch (rand)
        {
            case 1:
                text0.SetActive(true);
                Text intext0 = text0.GetComponent<Text>();
                break;
            case 2:
                text1.SetActive(true);
                Text intext1 = text1.GetComponent<Text>();

                break;
            case 3:
                text2.SetActive(true);
                Text intext2 = text2.GetComponent<Text>();
                break;
        }
    }
}
