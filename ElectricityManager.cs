using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityManager : MonoBehaviour
{
    //停電までの時間（固定）
    const float maxoffTime = 10;
    //停電までの経過時間
    float offElectricityTiem;
    //停電状態
    bool blackOut;
    private List<GameObject> lightObject=new List<GameObject>();
    private List<GameObject> electricity = new List<GameObject>();

    [SerializeField] GameObject shutter;
    [SerializeField] int lightNum;
    [SerializeField] int electNum;
    // Start is called before the first frame update
    void Start()
    {
        //ライトの取得
        for(int i = 0; i < lightNum; i++) 
        {
            lightObject.Add(GameObject.Find("Point Light (" + i + ")"));
            //lightObject[i].SetActive(true);
        }
        //電気switch取得
        for(int i = 0; i < electNum; i++) 
        {
            electricity.Add(GameObject.Find("Electricity" + i));
            //最初だけアクティブに
            if (i != 0)
            {
                electricity[i].SetActive(false);
            }
        }
        offElectricityTiem = maxoffTime;
        blackOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        //時間の減算
        TimeSubtract();
        //一定範囲内にいてかつボタンが押されたら。
        if (Input.GetKey(KeyCode.Space))
        {
            //停電してたら
            if (blackOut)
            {
                //停電復旧
                offElectricityTiem = maxoffTime;
                //全ライトをアクティブに
                for (int i = 0; i < lightNum; i++)
                {
                    lightObject[i].SetActive(true);
                }
            }
        }
    }
    //時間の減算
    void TimeSubtract()
    {
        if (offElectricityTiem >= 0)
        {
            blackOut = false;
            offElectricityTiem -=Time.deltaTime;
        }
        else
        {
            //停電状態
            blackOut = true;
            //全ライトを非アクティブに
            for (int i = 0; i < lightNum; i++)
            {
                lightObject[i].SetActive(false);
            }
        }
    }
}
