using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    //感圧板のスクリプト
    [SerializeField]GameObject plate;
    //開く高さの最大値
    [SerializeField]float maxHight;
    //開くスピード
    [SerializeField] float speed;
    //開きフラグ
    bool openFlag = false;
    //時間
    int timer = 0;
    //閉まるまでの猶予時間
    int shutTime = 30;
    //現在地の保存
    Vector3 savePosotion;
    PressureSensitivePlate plateScript;
    // Start is called before the first frame update
    void Start()
    {
        plateScript = plate.GetComponent<PressureSensitivePlate>();

        savePosotion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //感圧板が踏まれたら
        if (plateScript.GetHitFlag())
        {
            openFlag = true;
            timer = 0;
        }
        if (timer == 0 && !openFlag)
        {
            if (transform.position.y >= savePosotion.y)
            {
                Vector3 pos = transform.position;
                pos.y -= speed;
                transform.position = pos;

            }
        }
        if (openFlag)
        {
            if (transform.position.y <= maxHight)
            {
                Vector3 pos = transform.position;
                pos.y += speed;
                transform.position = pos;
            }
            timer++;
            if (timer > shutTime) 
            {
                timer = 0;
                openFlag = false;
            }
        }
       
    }
}
