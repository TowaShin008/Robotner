using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    //巡回する4箇所のポイント
    public Vector3[] wayPoints = new Vector3[4];
    //現在の目的の座標
    private int currentRoot;
    private int mode;
    //プレイヤーのポジション
    public Transform playerPos;
    //エネミーのポジション
    public Transform enemyPos;

    private bool stopFlag;
    private bool sphereCollisionFlag;
    private bool rayCollisionFlag;
    private int stopTimer = 60;
    private bool jumpFlag = false;

    public AudioClip clip;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stopFlag = false;
        sphereCollisionFlag = false;
        rayCollisionFlag = false;
        stopTimer = 60;
    }

    void Update()
    {
        Vector3 pos = wayPoints[currentRoot];//Vector3型のposに現在の目的地の座標を代入
        float distance = Vector3.Distance(enemyPos.position, playerPos.transform.position);//敵とプレイヤーの距離を求める



        if (sphereCollisionFlag == false && rayCollisionFlag == false)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.updatePosition = true;
            stopFlag = false;
            //stopTimer = 60;
            mode = 0;//Modeを0にする
        }
        else
        {
            mode = 1;
            if (stopFlag == false)
            {
                StopProcessing();
            }
            //else
            //{
            //    navMeshAgent.isStopped = true;
            //    navMeshAgent.updatePosition = false;
            //}
        }

        //if (sphereCollisionFlag || rayCollisionFlag)
        //{

        //}

        sphereCollisionFlag = false;
        rayCollisionFlag = false;

        switch (mode)
        {//Modeの切り替えは

            case 0://case0の場合

                if (Vector3.Distance(transform.position, pos) < 1f)
                {//もし敵の位置と現在の目的地との距離が1以下なら
                    currentRoot += 1;//currentRootを+1する
                    stopTimer = 60;
                    if (currentRoot > wayPoints.Length - 1)
                    {//もしcurrentRootがwayPointsの要素数-1より大きいなら
                        currentRoot = 0;//currentRootを0にする
                    }
                }
                GetComponent<NavMeshAgent>().SetDestination(pos);//NavMeshAgentの情報を取得し目的地をposにする

                break;//switch文の各パターンの最後につける

            case 1://case1の場合

                if (stopFlag)
                {
                    navMeshAgent.destination = playerPos.transform.position;
                }
                break;//switch文の各パターンの最後につける
        }

        //     if (sphereCollisionFlag==false && rayCollisionFlag == false)
        //     {
        //stopFlag = false;
        //     }
        //     else


        //レイの当たり判定
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                rayCollisionFlag = true;
                //navMeshAgent.updatePosition = true;
                //if (stopFlag)
                //{
                //    navMeshAgent.destination = playerPos.transform.position;
                //}
            }
        }
    }

    public void OnDetectObject(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            sphereCollisionFlag = true;
            //navMeshAgent.updatePosition = true;
            //if (stopFlag)
            //{
            //	navMeshAgent.destination = playerPos.transform.position;
            //}
        }
    }
    //発見の演出処理
    public void StopProcessing()
    {
        navMeshAgent.isStopped = true;
        //navMeshAgent.updatePosition = false;

        if (stopTimer < 0)
        {

            //Rigidbody rigidbody = GetComponent<Rigidbody>();
            //rigidbody.isKinematic = false;
            navMeshAgent.isStopped = false;
            navMeshAgent.updatePosition = true;
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            stopFlag = true;
        }
        else
        {
            if (stopTimer == 60)
            {
                //JumpProcessing();
                GetComponent<AudioSource>().PlayOneShot(clip);
            }
            stopTimer--;
        }

    }

    public void JumpProcessing()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.AddForce(transform.up * 1000);
    }
}
