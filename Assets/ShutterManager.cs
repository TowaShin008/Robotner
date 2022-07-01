using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterManager : MonoBehaviour
{

    [SerializeField]
   // private SwitchFlag gimicControl; // ギミック管理用スクリプト
    

    //private BoxCollider collider; // ゲートのコライダー
    public GameObject Shutternum; //Shutterそのものが入る変数
   // public ShutterMove script; //ShutterMoveScriptが入る変数
    public bool Open = true;
    public bool actionFlug;

    Vector3 finpos;  //�@仮の変数宣言
    private static readonly Vector3 vector3 = new(0.5f, 5f, 2f);

    //どこに向かって上昇・下降するか
    Vector3 directionUp = vector3;
    Vector3 directionDown = new(0.5f, 0.5f, 2f);
    //動きの高さ
    float heightUp = 3.0f;
    float heightDown = -3.0f;

    void Start()
    {
        //デフォルトの色
        if(Open == false)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") // 操作キャラのオブジェクトタグ
        {
            GetComponent<Renderer>().material.color = Color.red;

        }
    }

  
        
         

        void Update()
        {

            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


        if (Open == false && Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 10.0f)) //アクションボタンを押したとき
            {
                if (hit.collider.gameObject == gameObject)
                {
                    finpos = Shutternum.transform.position;
                    finpos.y = finpos.y + heightUp;

                    Shutternum.transform.position = finpos;

                GetComponent<Renderer>().material.color = Color.red;

                Open = true;
                }
            }
            else if (Open == true && Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 10.0f))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    finpos = Shutternum.transform.position;
                    finpos.y = finpos.y + heightDown;

                    Shutternum.transform.position = finpos;

                    Open = false;
                    GetComponent<Renderer>().material.color = Color.blue;
                }
            }

        }
    }


