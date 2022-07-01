using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    //巡回する4箇所のポイント
    public Transform[] wayPoints = new Transform[4];
    //現在の目的の座標
    private int currentRoot;
    private int mode;
    //プレイヤーのポジション
    public GameObject playerObject;

    private bool stopFlag;
    private bool sphereCollisionFlag;
    private bool rayCollisionFlag;
    private int stopTimer = 60;

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
        Transform currentPoint = wayPoints[currentRoot];//Vector3型のposに現在の目的地の座標を代入
        float distance = Vector3.Distance(this.transform.position, playerObject.transform.position);//敵とプレイヤーの距離を求める

        if (sphereCollisionFlag == false && rayCollisionFlag == false)
        {
            navMeshAgent.isStopped = false;
            stopFlag = false;

            //プレイヤーがしゃがんでいたら巡回モードに移行
            if (playerObject.GetComponent<FPSController>().GetSquatFlag())
            {
                mode = 0;//Modeを0にする
            }
        }
        else
        {
            mode = 1;
            if (stopFlag == false)
            {//発見時の停止演出
                StopProcessing();
            }
        }

        sphereCollisionFlag = false;
        rayCollisionFlag = false;

        switch (mode)
        {//Modeの切り替えは

            case 0://case0の場合

                if (Vector3.Distance(transform.position, currentPoint.position) < 1f)
                {//もし敵の位置と現在の目的地との距離が1以下なら
                    currentRoot += 1;//currentRootを+1する
                    stopTimer = 60;
                    if (currentRoot > wayPoints.Length - 1)
                    {//もしcurrentRootがwayPointsの要素数-1より大きいなら
                        currentRoot = 0;//currentRootを0にする
                    }
                }
                GetComponent<NavMeshAgent>().SetDestination(currentPoint.position);//NavMeshAgentの情報を取得し目的地をposにする

                break;//switch文の各パターンの最後につける

            case 1://case1の場合
                //停止演出が終わったら追跡の開始
                if (stopFlag)
                {
                    navMeshAgent.destination = playerObject.transform.position;
                }
                break;
        }

        ////レイの当たり判定
        //Ray ray = new Ray(transform.position, transform.forward);

        //RaycastHit hit;

        //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //{
        //    if (hit.collider.gameObject.tag == "Player")
        //    {
        //        rayCollisionFlag = true;
        //    }
        //}
    }

    public void OnDetectObject(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            sphereCollisionFlag = true;
        }
    }

    public void OnFanDetectObject(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("HIT");
            rayCollisionFlag = true;
        }
    }
    //発見の演出処理
    public void StopProcessing()
    {
        navMeshAgent.isStopped = true;

        bool timeOut = stopTimer < 0;

        if (timeOut)
        {
            navMeshAgent.isStopped = false;
            stopFlag = true;
        }
        else
        {
            if (stopTimer == 60)
            {
                GetComponent<AudioSource>().PlayOneShot(clip);
            }
            stopTimer--;
        }
    }

    //扇形の当たり判定
    public bool CollisionFan_to_Point(Vector3 otherPosition)
    {

        return false;
    }
}
