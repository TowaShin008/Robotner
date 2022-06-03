using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

	//���񂷂�4�ӏ��̃|�C���g
    public Vector3[] wayPoints = new Vector3[4];
	//���݂̖ړI�̍��W
    private int currentRoot;
    private int mode;
	//�v���C���[�̃|�W�V����
    public Transform playerPos;
	//�G�l�~�[�̃|�W�V����
    public Transform enemyPos;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
		Vector3 pos = wayPoints[currentRoot];//Vector3�^��pos�Ɍ��݂̖ړI�n�̍��W����
		float distance = Vector3.Distance(enemyPos.position, playerPos.transform.position);//�G�ƃv���C���[�̋��������߂�

		if (distance > 5)
		{//�����v���C���[�ƓG�̋�����5�ȏ�Ȃ�
			mode = 0;//Mode��0�ɂ���
		}

		if (distance < 5)
		{//�����v���C���[�ƓG�̋�����5�ȉ��Ȃ�
			mode = 1;//Mode��1�ɂ���
		}

		switch (mode)
		{//Mode�̐؂�ւ���

			case 0://case0�̏ꍇ

				if (Vector3.Distance(transform.position, pos) < 1f)
				{//�����G�̈ʒu�ƌ��݂̖ړI�n�Ƃ̋�����1�ȉ��Ȃ�
					currentRoot += 1;//currentRoot��+1����
					if (currentRoot > wayPoints.Length - 1)
					{//����currentRoot��wayPoints�̗v�f��-1���傫���Ȃ�
						currentRoot = 0;//currentRoot��0�ɂ���
					}
				}
				GetComponent<NavMeshAgent>().SetDestination(pos);//NavMeshAgent�̏����擾���ړI�n��pos�ɂ���
				break;//switch���̊e�p�^�[���̍Ō�ɂ���

			case 1://case1�̏ꍇ
				navMeshAgent.destination = playerPos.transform.position;//�v���C���[�Ɍ������Đi��		
				break;//switch���̊e�p�^�[���̍Ō�ɂ���
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
