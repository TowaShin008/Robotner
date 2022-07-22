using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    //���񂷂�4�ӏ��̃|�C���g
    public List<Transform> wayPoints = new List<Transform>();
    //���݂̖ړI�̍��W
    private int currentRoot;
    private int mode;
    //�v���C���[�̃|�W�V����
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
        //Vector3�^��pos�Ɍ��݂̖ړI�n�̍��W����
        Transform currentPoint = wayPoints[currentRoot];

        if (sphereCollisionFlag == false && fanCollisionFlag == false)
        {
            navMeshAgent.isStopped = false;
            stopFlag = false;

            //�v���C���[�Ɋ��S�ɋC�Â��Ă��Ȃ���(�p�g���[����Ԃ����������Ă��邩)�ǂ����̃t���O
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
                //�v���C���[�����Ⴊ��ł����珄�񃂁[�h�Ɉڍs
                if (playerObject.GetComponent<FPSController>().GetSquatFlag() && fanCollisionFlag == false && patrolTime == 120)
                {
                    mode = 0;
                }
                else
                {//�p�g���[�����Ԃ̏�����
                    patrolTime = 0;
                    //�������̒�~���o����
                    StopProcessing();
                }
            }
        }

        //���ꂼ��̓����蔻������Z�b�g
        sphereCollisionFlag = false;
        fanCollisionFlag = false;

        switch (mode)
        {
            case 0:

                if (Vector3.Distance(transform.position, currentPoint.position) < 1.0f)
                {//�����G�̈ʒu�ƌ��݂̖ړI�n�Ƃ̋�����1�ȉ��Ȃ�C���f�b�N�X�����ɂ���
                    currentRoot++;
                    stopTimer = 60;
                    if (currentRoot > wayPoints.Count - 1)
                    {//����currentRoot��wayPoints�̗v�f��-1���傫���Ȃ�currentRoot��0�ɂ���
                        currentRoot = 0;
                    }
                }

                GetComponent<NavMeshAgent>().SetDestination(currentPoint.position);//NavMeshAgent�̏����擾���ړI�n��pos�ɂ���

                break;

            case 1:
                //��~���o���I�������ǐՂ̊J�n
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
    {//��`�̓����蔻��̃X�N���v�g�Ń^�O�̔�������Ă��邽�߃I�u�W�F�N�g�����ł���t���O��true�ɂȂ�
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
    //�������̒�~���o����
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

    //��`�̓����蔻��
    public bool CollisionFan_to_Point(Vector3 otherPosition)
    {
        return false;
    }
}
