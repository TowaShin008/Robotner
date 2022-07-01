using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFlag : MonoBehaviour
{
    public bool is_Open; //ドアが開いたかどうかの変数
    public KeyCode keyCode; //どのキーを入力するかの変数
    public Material mat;

    GameObject Shutter2; //Shutterそのものが入る変数

    ShutterMove script; //ShutterMoveScriptが入る変数

    public enum Situation
    {
        Open,
        Opening,
        Shutouting,
        Shutout
    }

    Situation situation = Situation.Shutout;

    void Start()
    {
        //Shutter2をオブジェクトの名前から取得して変数に格納する
        Shutter2 = GameObject.Find("Shutter2");
        //Shutter2の中にあるShutterMoveを取得して変数に格納する
        script = Shutter2.GetComponent<ShutterMove>();
        //デフォルトの色
        GetComponent<Renderer>().material.color = Color.blue;

        situation = Situation.Shutout;

        switch (situation)
        {
            case Situation.Open:
                Debug.Log("開いている");
                break;
            case Situation.Opening:
                Debug.Log("開け途中");
                break;
            case Situation.Shutouting:
                Debug.Log("閉め途中");
                break;
            case Situation.Shutout:
                Debug.Log("閉まってる");
                break;
        }
    }

    //どちらかのIs TriggerがオンになっているCollider同士が接触している間常に呼び出し
    //private void OnTriggerStay(Collider obj)
    void Update()
    {
       
        //特定のボタンを押してかつis_Openがfalseの時is_Openをtrueにする
        if (Input.GetMouseButtonDown(0) && situation == Situation.Shutout)
        {            
            script.Up();
            situation = Situation.Opening;
            GetComponent<Renderer>().material.color = Color.red;
            if ( situation == Situation.Opening)
            {
                situation = Situation.Open;
                Debug.Log("開!");
            }
        } 

        // 特定のボタンを押してかつis_Openがtrueの時is_Openをfalseにする
      else if (Input.GetMouseButtonDown(0) && situation == Situation.Open) 
        {     
            situation = Situation.Shutouting;
            GetComponent<Renderer>().material.color = Color.blue;
            script.Down();
            if (situation == Situation.Shutouting)
            {
                situation = Situation.Shutout;
                Debug.Log("閉!");
            }
        }
    }
}
