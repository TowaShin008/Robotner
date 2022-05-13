using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    // Start is called before the first frame update
    //投影するカメラの状態
    private bool active=false;
    //１フレーム前のカメラのアクティブ状態
    private bool saveActive = false;
    //カメラ格納用
    private List<GameObject> cameraList = new List<GameObject>();
    //カメラの全体の合計数
    //インスペクター上に表示
    [SerializeField]
    public int cameraNum=3;
    //現在の表示カメラ
    private int nowWatchCamera = 0;
    void Start()
    {
        //メインとサブを取得
        for (int i = 0; i < cameraNum; i++)
        {
            //全てのカメラを取得
            cameraList.Add(GameObject.Find("SubCamera" + i));
            //最初の一つ目以外を
            if (i != nowWatchCamera)
            {  
                //非アクティブに
                cameraList[i].SetActive(active);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //１フレーム前のアクティブ状態
        saveActive = active;
        if (Input.GetKeyDown("space"))
        {
            //アクティブ状態変更
            active =! active;
            //次のカメラに変更
            nowWatchCamera++; 
        } 
        //アクティブ状態が切り替わったら
        if(active!=saveActive)
        {
            //今見ているカメラが最大数になったら最初のカメラに戻す
            if (nowWatchCamera == cameraNum)
            {
                nowWatchCamera = 0;
            }
            //今見ているカメラだけをアクティブ状態に
            cameraList[nowWatchCamera].SetActive(active);
            //カメラの最大数分アクティブ状態をfalseにする
            for (int i = 0; i < cameraNum; i++)
            {
                if (nowWatchCamera != i)
                {
                    cameraList[i].SetActive(!active);
                }
            }            
        }
        //変更終了
        active = false;
    }
}
