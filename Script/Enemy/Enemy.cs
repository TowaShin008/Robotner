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
    public Transform robotPosition;

    private Transform targetPosition;

    private bool stopFlag;
    private bool sphereCollisionFlag;
    private bool fanCollisionFlag;
    private int stopTimer;

    public AudioClip bark;
    public AudioClip howling;

    int patrolTime;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stopFlag = false;
        sphereCollisionFlag = false;
        fanCollisionFlag = false;
        stopTimer = 60;
        patrolTime = 120;
    }

    void Update()
    {
        //Vector3型のposに現在の目的地の座標を代入
        Transform currentPoint = wayPoints[currentRoot];

        if (sphereCollisionFlag == false && fanCollisionFlag == false)
        {
            navMeshAgent.isStopped = false;
            stopFlag = false;

            //プレイヤーに完全に気づいていないか(パトロール状態が長く続いているか)どうかのフラグ
            bool noticeFlag = patrolTime < 120;
            if(noticeFlag)
            {
                patrolTime++;
            }
            mode = 0;
        }
        else
        {
            mode = 1;
            if (stopFlag == false)
            {
                //プレイヤーがしゃがんでいたら巡回モードに移行
                if (playerObject.GetComponent<FPSController>().GetSquatFlag() && fanCollisionFlag == false && patrolTime == 120)
                {
                    mode = 0;
                }
                else
                {//パトロール時間の初期化
                    patrolTime = 0;
                    //発見時の停止演出処理
                    StopProcessing();
                }
            }
        }

        //それぞれの当たり判定をリセット
        sphereCollisionFlag = false;
        fanCollisionFlag = false;

        switch (mode)
        {
            case 0:

                if (Vector3.Distance(transform.position, currentPoint.position) < 1.0f)
                {//もし敵の位置と現在の目的地との距離が1以下ならインデックスを次にする
                    currentRoot++;
                    stopTimer = 60;
                    if (currentRoot > wayPoints.Count - 1)
                    {//もしcurrentRootがwayPointsの要素数-1より大きいならcurrentRootを0にする
                        currentRoot = 0;
                    }
                }

                GetComponent<NavMeshAgent>().SetDestination(currentPoint.position);//NavMeshAgentの情報を取得し目的地をposにする

                break;

            case 1:
                //停止演出が終わったら追跡の開始
                if (stopFlag)
                {
                    navMeshAgent.destination = targetPosition.position;
                }
                break;
        }
    }

    public void OnDetectObject(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            sphereCollisionFlag = true;
            targetPosition = playerObject.transform;
        }
        else if (collider.gameObject.CompareTag("Robot"))
        {
            sphereCollisionFlag = true;
            targetPosition = robotPosition.transform;
        }
    }

    public void OnFanDetectObject(Collider collider)
    {//扇形の当たり判定のスクリプトでタグの判定をしているためオブジェクトが何であれフラグはtrueになる
        fanCollisionFlag = true;

        if (collider.gameObject.CompareTag("Player"))
        {
            targetPosition = playerObject.transform;
        }
        else if (collider.gameObject.CompareTag("Robot"))
        {
            targetPosition = robotPosition.transform;
        }
    }
    //発見時の停止演出処理
    public void StopProcessing()
    {
        navMeshAgent.isStopped = true;

        bool timeOut = stopTimer < 0;

        if (stopTimer == 1)
        {
            const float howlingVolume = 5.0f;
            GetComponent<AudioSource>().PlayOneShot(howling, howlingVolume);
        }

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
                GetComponent<AudioSource>().PlayOneShot(bark);
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
