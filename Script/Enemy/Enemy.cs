using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    //���񂷂�4�ӏ��̃|�C���g
    public Transform[] wayPoints = new Transform[4];
    //���݂̖ړI�̍��W
    private int currentRoot;
    private int mode;
    //�v���C���[�̃|�W�V����
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
        Transform currentPoint = wayPoints[currentRoot];//Vector3�^��pos�Ɍ��݂̖ړI�n�̍��W����
        float distance = Vector3.Distance(this.transform.position, playerObject.transform.position);//�G�ƃv���C���[�̋��������߂�

        if (sphereCollisionFlag == false && rayCollisionFlag == false)
        {
            navMeshAgent.isStopped = false;
            stopFlag = false;

            //�v���C���[�����Ⴊ��ł����珄�񃂁[�h�Ɉڍs
            if (playerObject.GetComponent<FPSController>().GetSquatFlag())
            {
                mode = 0;//Mode��0�ɂ���
            }
        }
        else
        {
            mode = 1;
            if (stopFlag == false)
            {//�������̒�~���o
                StopProcessing();
            }
        }

        sphereCollisionFlag = false;
        rayCollisionFlag = false;

        switch (mode)
        {//Mode�̐؂�ւ���

            case 0://case0�̏ꍇ

                if (Vector3.Distance(transform.position, currentPoint.position) < 1f)
                {//�����G�̈ʒu�ƌ��݂̖ړI�n�Ƃ̋�����1�ȉ��Ȃ�
                    currentRoot += 1;//currentRoot��+1����
                    stopTimer = 60;
                    if (currentRoot > wayPoints.Length - 1)
                    {//����currentRoot��wayPoints�̗v�f��-1���傫���Ȃ�
                        currentRoot = 0;//currentRoot��0�ɂ���
                    }
                }
                GetComponent<NavMeshAgent>().SetDestination(currentPoint.position);//NavMeshAgent�̏����擾���ړI�n��pos�ɂ���

                break;//switch���̊e�p�^�[���̍Ō�ɂ���

            case 1://case1�̏ꍇ
                //��~���o���I�������ǐՂ̊J�n
                if (stopFlag)
                {
                    navMeshAgent.destination = playerObject.transform.position;
                }
                break;
        }

        ////���C�̓����蔻��
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
    //�����̉��o����
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

    //��`�̓����蔻��
    public bool CollisionFan_to_Point(Vector3 otherPosition)
    {

        return false;
    }
}
