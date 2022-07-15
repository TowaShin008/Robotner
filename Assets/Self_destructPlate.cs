using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_destructPlate : MonoBehaviour
{
    public GameObject Robot;
    public GameObject Player;
    public GameObject Rubble;//瓦礫
    public GameObject Rubble2;//瓦礫2つめ
    public bool Open = false;
    Vector3 finpos;  //変数宣言
    float heightDown = -0.03f;
   
    void Start()
    {
        Open = false;
        GetComponent<Renderer>().material.color = Color.yellow;
        
    }
    void Update()
    {   
        //感圧版との距離計算
        Vector3 robot = Robot.transform.position;
        float Robotdis = Vector3.Distance(robot, this.transform.position);
        //ロボットが押した時
        if (Robotdis < 0.9f && Open == false)
        {
            // コルーチンの起動
            StartCoroutine(DelayCoroutine());
            PlateOn();
        }

        Vector3 player = Player.transform.position;
        float Playerdis = Vector3.Distance(player, this.transform.position);
        Vector3 rubble = Rubble.transform.position;
        float Rubbledis = Vector3.Distance(rubble, this.transform.position);
        Vector3 rubble2 = Rubble2.transform.position;
        float Rubbledis2 = Vector3.Distance(rubble2, this.transform.position);
        //瓦礫かプレイヤーが押した時
        if ((Rubbledis < 0.9f || Playerdis < 0.9f || Rubbledis2 < 0.9f) && Open == false)
        {
            PlateOn();
        }
    }
    IEnumerator DelayCoroutine()
    {
        // 5秒間待つ
        yield return new WaitForSeconds(5);
        RubbleGravity();
    }

    void RubbleGravity()//瓦礫が落ちる
    {
        Rubble.GetComponent<Rigidbody>().useGravity = true;
        Rubble2.GetComponent<Rigidbody>().useGravity = true;
    }  

    void PlateOn()//感圧版がオンの時(共通)
    {
        GetComponent<Renderer>().material.color = Color.green;
        Open = true;
        finpos = gameObject.transform.position;
        finpos.y = finpos.y + heightDown;
        transform.position = finpos;
    }
}

