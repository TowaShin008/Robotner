using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_destructPlate : MonoBehaviour
{
    public GameObject Robot;
    public GameObject Player;
    public GameObject Rubble;//���I
    public GameObject Rubble2;//���I2��
    public bool Open = false;
    Vector3 finpos;  //�ϐ��錾
    float heightDown = -0.03f;
   
    void Start()
    {
        Open = false;
        GetComponent<Renderer>().material.color = Color.yellow;
        
    }
    void Update()
    {   
        //�����łƂ̋����v�Z
        Vector3 robot = Robot.transform.position;
        float Robotdis = Vector3.Distance(robot, this.transform.position);
        //���{�b�g����������
        if (Robotdis < 0.9f && Open == false)
        {
            // �R���[�`���̋N��
            StartCoroutine(DelayCoroutine());
            PlateOn();
        }

        Vector3 player = Player.transform.position;
        float Playerdis = Vector3.Distance(player, this.transform.position);
        Vector3 rubble = Rubble.transform.position;
        float Rubbledis = Vector3.Distance(rubble, this.transform.position);
        Vector3 rubble2 = Rubble2.transform.position;
        float Rubbledis2 = Vector3.Distance(rubble2, this.transform.position);
        //���I���v���C���[����������
        if ((Rubbledis < 0.9f || Playerdis < 0.9f || Rubbledis2 < 0.9f) && Open == false)
        {
            PlateOn();
        }
    }
    IEnumerator DelayCoroutine()
    {
        // 5�b�ԑ҂�
        yield return new WaitForSeconds(5);
        RubbleGravity();
    }

    void RubbleGravity()//���I��������
    {
        Rubble.GetComponent<Rigidbody>().useGravity = true;
        Rubble2.GetComponent<Rigidbody>().useGravity = true;
    }  

    void PlateOn()//�����ł��I���̎�(����)
    {
        GetComponent<Renderer>().material.color = Color.green;
        Open = true;
        finpos = gameObject.transform.position;
        finpos.y = finpos.y + heightDown;
        transform.position = finpos;
    }
}

