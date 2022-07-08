using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendAccessCode : MonoBehaviour
{
    //�p�b�h�A���{�b�g�A�A�N�Z�X�R�[�h�̏��ŃI�u�W�F�N�g������
    public GameObject robPad;
    public GameObject robot;
    public GameObject accessCode;
    GameObject clone;

    [SerializeField] private float needTime = 20.0f;

    //send�̉ߋ��`�A�N�Z�X�R�[�h�𑗂�ς݂�
    bool sent = false;
    
    //�v���̊J�n���Ԃ��i�[����ϐ�
    private float start;
    //�o�ߎ��Ԃ��i�[����ϐ�
    private float elapsedTime; 

    RaycastHit hit;

    public Slider gauge;

    // Start is called before the first frame update
    void Start()
    {
        sent = false;
        gauge.maxValue = needTime;
    }

    // Update is called once per frame
    void Update()
    {
        //���{�b�g�ƃ��{�p�b�h�̋��������߂�
        float distance = Vector3.Distance(robot.transform.position, robPad.transform.position);

        Ray robotRay = new Ray(robot.transform.position, robot.transform.forward);

        Debug.DrawRay(robotRay.origin, robotRay.direction * 10, Color.red, 5);
               
        //�@Cube�̃��C���΂��^�[�Q�b�g�ƐڐG���Ă��邩����
        if (Physics.BoxCast(robot.transform.position, Vector3.one * 1.0f, robot.transform.forward, out hit, Quaternion.identity, 5.0f))
        {
            Debug.Log(hit.transform.gameObject.name);
            if (hit.collider.CompareTag("Player"))
            {
                elapsedTime = Time.time - start;
            }
            else
            {
                start = Time.time;
            }

            //if (Input.GetKeyDown(KeyCode.V) && hit.collider.CompareTag("Player"))
            //{
            //    SendCode();
            //}

            if (elapsedTime >= needTime && sent == false)
            {
                sent = true;
                SendCode();
            }
        }
        else
        {
            start = Time.time;
            elapsedTime = Time.time - start;
        }

        Debug.Log(elapsedTime);

        if (sent == false)
        {
            gauge.value = elapsedTime;
        }
        else
        {
            gauge.value = gauge.maxValue;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(robot.transform.position + robot.transform.forward * hit.distance, Vector3.one * 1.5f);
    }

    public void SendCode()
    {
        clone = Instantiate(accessCode, accessCode.transform);
        clone.transform.parent = robot.transform;
        accessCode.transform.parent = robPad.transform;
    }

}
