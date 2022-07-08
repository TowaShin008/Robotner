using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    //巡回する4箇所のポイント
    public List<Transform> wayPoints = new List<Transform>();
    //現在の目的の座標
    private int currentRoot;
    private int mode;
    //プレイヤーのポジション
    public GameObject playerObject;

    private bool stopFlag;
    private bool sphereCollisionFlag;
    private bool fanCollisionFlag;
    private int stopTimer;

    public AudioClip clip;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stopFlag = false;
        sphereCollisionFlag = false;
        fanCollisionFlag = false;
        stopTimer = 60;
    }

    void Update()
    {
        Transform currentPoint = wayPoints[currentRoot];//Vector3型のposに現在の目的地の座標を代入

        if (sphereCollisionFlag == false && fanCollisionFlag == false)
        {
            navMeshAgent.isStopped = false;
            stopFlag = false;
            mode = 0;
        }
        else
        {
            mode = 1;
            if (stopFlag == false)
            {
             //プレイヤーがしゃがんでいたら巡回モードに移行
                if (playerObject.GetComponent<FPSController>().GetSquatFlag() && fanCollisionFlag == false)
                {
                    mode = 0;
                }
                else
                {//発見時の停止演出
                    StopProcessing();
                }
            }
        }

        //それぞれの当たり判定をリセット
        sphereCollisionFlag = false;
        fanCollisionFlag = false;

        switch (mode)
        {//Modeの切り替えは

            case 0://case0の場合

                if (Vector3.Distance(transform.position, currentPoint.position) < 1f)
                {//もし敵の位置と現在の目的地との距離が1以下なら
                    currentRoot++;//インデックスを次にする
                    stopTimer = 60;
                    if (currentRoot > wayPoints.Count - 1)
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
            fanCollisionFlag = true;
        }
    }
    //発見時の停止演出処理
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
            bool beginStop = stopTimer == 60;
            if (beginStop)
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
