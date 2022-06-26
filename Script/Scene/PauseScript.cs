using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseUI;

    // Start is called before the first frame update
    void Start()
    {
        //　スタート時にポーズUIを非表示にする
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //　ポーズボタンを押した時
        if (Input.GetButtonDown("Pause"))
        {
            if (Mathf.Approximately(Time.timeScale, 1f))
            {
                Time.timeScale = 0f;
                pauseUI.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                pauseUI.SetActive(false);
            }
        }
    }
}
