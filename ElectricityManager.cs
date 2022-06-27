using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityManager : MonoBehaviour
{
    //停電までの時間（固定）
    [SerializeField]  float maxoffTime = 10;
    //停電までの経過時間
    float offElectricityTiem;
    //停電状態
    bool blackOut;
    private List<GameObject> lightObject=new List<GameObject>();
    private List<GameObject> electricity = new List<GameObject>();

    [SerializeField] GameObject shutter;
    [SerializeField] int lightNum;
    [SerializeField] int electNum;
    //現在起動中のブレーカー
    int nowActiveElectricity = 0;
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
                //ブレーカー切り替え
                NowBreakerChange();
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
    void NowBreakerChange() 
    {
        //次のアクティブへ切り替え
        nowActiveElectricity++;
        if (nowActiveElectricity == electNum)
        {
            nowActiveElectricity = 0;
        }
        //ブレーカーのアクティブ切り替え
        for (int i = 0; i < electNum; i++)
        {
            if (i == nowActiveElectricity)
            {
                electricity[i].SetActive(true);
            }
            else
            {
                electricity[i].SetActive(false);
            }
        }
    }
}
