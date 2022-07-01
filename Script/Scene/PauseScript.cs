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
        //�@�X�^�[�g���Ƀ|�[�YUI���\���ɂ���
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�@�|�[�Y�{�^������������
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
