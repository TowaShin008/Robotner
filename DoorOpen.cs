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
            if (transform.position.y < maxHight)
            {
                Vector3 pos = transform.position;
                pos.y += speed;
                transform.position = pos;
            }
        }
        else 
        {
            if (transform.position.y > savePosotion.y)
            {
                Vector3 pos = transform.position;
                pos.y -= speed;
                transform.position = pos;

            }
        }
    }
}
