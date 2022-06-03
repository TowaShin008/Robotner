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

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
		Vector3 pos = wayPoints[currentRoot];//Vector3型のposに現在の目的地の座標を代入
		float distance = Vector3.Distance(enemyPos.position, playerPos.transform.position);//敵とプレイヤーの距離を求める

		if (distance > 5)
		{//もしプレイヤーと敵の距離が5以上なら
			mode = 0;//Modeを0にする
		}

		if (distance < 5)
		{//もしプレイヤーと敵の距離が5以下なら
			mode = 1;//Modeを1にする
		}

		switch (mode)
		{//Modeの切り替えは

			case 0://case0の場合

				if (Vector3.Distance(transform.position, pos) < 1f)
				{//もし敵の位置と現在の目的地との距離が1以下なら
					currentRoot += 1;//currentRootを+1する
					if (currentRoot > wayPoints.Length - 1)
					{//もしcurrentRootがwayPointsの要素数-1より大きいなら
						currentRoot = 0;//currentRootを0にする
					}
				}
				GetComponent<NavMeshAgent>().SetDestination(pos);//NavMeshAgentの情報を取得し目的地をposにする
				break;//switch文の各パターンの最後につける

			case 1://case1の場合
				navMeshAgent.destination = playerPos.transform.position;//プレイヤーに向かって進む		
				break;//switch文の各パターンの最後につける
		}
	}

    public void OnDetectObject(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            navMeshAgent.destination = collider.transform.position;
        }
    }
}
