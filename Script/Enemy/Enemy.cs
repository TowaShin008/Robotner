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
        Vector3 pos = wayPoints[currentRoot];//Vector3�^��pos�Ɍ��݂̖ړI�n�̍��W����
        float distance = Vector3.Distance(enemyPos.position, playerPos.transform.position);//�G�ƃv���C���[�̋��������߂�



        if (sphereCollisionFlag == false && rayCollisionFlag == false)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.updatePosition = true;
            stopFlag = false;
            //stopTimer = 60;
            mode = 0;//Mode��0�ɂ���
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
        {//Mode�̐؂�ւ���

            case 0://case0�̏ꍇ

                if (Vector3.Distance(transform.position, pos) < 1f)
                {//�����G�̈ʒu�ƌ��݂̖ړI�n�Ƃ̋�����1�ȉ��Ȃ�
                    currentRoot += 1;//currentRoot��+1����
                    stopTimer = 60;
                    if (currentRoot > wayPoints.Length - 1)
                    {//����currentRoot��wayPoints�̗v�f��-1���傫���Ȃ�
                        currentRoot = 0;//currentRoot��0�ɂ���
                    }
                }
                GetComponent<NavMeshAgent>().SetDestination(pos);//NavMeshAgent�̏����擾���ړI�n��pos�ɂ���

                break;//switch���̊e�p�^�[���̍Ō�ɂ���

            case 1://case1�̏ꍇ

                if (stopFlag)
                {
                    navMeshAgent.destination = playerPos.transform.position;
                }
                break;//switch���̊e�p�^�[���̍Ō�ɂ���
        }

        //     if (sphereCollisionFlag==false && rayCollisionFlag == false)
        //     {
        //stopFlag = false;
        //     }
        //     else


        //���C�̓����蔻��
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
    //�����̉��o����
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
