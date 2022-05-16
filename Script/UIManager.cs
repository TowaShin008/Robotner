using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //それぞれのアプリ用のオブジェクトを用意する。
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject cameraPanel;
    [SerializeField] GameObject switchPanel;
    [SerializeField] GameObject mapPanel;
    [SerializeField] GameObject AccessCodePanel;

    

    // Start is called before the first frame update
    void Start()
    {
        //メニューに戻る
        BackToMenu();
    }


    //カメラのアプリのみアクティブにする。
    public void SelectCameraDescription()
    {
        menuPanel.SetActive(false);
        switchPanel.SetActive(false);
        cameraPanel.SetActive(true);
        mapPanel.SetActive(false);
        AccessCodePanel.SetActive(false);
    }


    //スイッチのアプリのアクティブにする
    public void SelectSwitchDescription()
    {
        menuPanel.SetActive(false);
        cameraPanel.SetActive(false);
        switchPanel.SetActive(true);
        mapPanel.SetActive(false);
        AccessCodePanel.SetActive(false);
    }

    //マップのアプリのみアクティブにする
    public void SelectMapDescription()
    {
        menuPanel.SetActive(false);
        cameraPanel.SetActive(false);
        switchPanel.SetActive(false);
        mapPanel.SetActive(true);
        AccessCodePanel.SetActive(false);
    }

    //Aアクセスコードのみアクティブにする
    public void SelectAccessCodeDescription()
    {
        menuPanel.SetActive(false);
        cameraPanel.SetActive(false);
        switchPanel.SetActive(false);
        mapPanel.SetActive(false);
        AccessCodePanel.SetActive(true);
    }


    //メニューのみアクティブにする
    public void BackToMenu()
    {
        menuPanel.SetActive(true);
        cameraPanel.SetActive(false);
        switchPanel.SetActive(false);
        mapPanel.SetActive(false);
        AccessCodePanel.SetActive(false);
    }
}