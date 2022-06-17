using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendAccessCode : MonoBehaviour
{
    public GameObject robPad;
    public GameObject robot;
    public GameObject accessCode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���{�b�g�ƃ��{�p�b�h�̋��������߂�
        float distance = Vector3.Distance(robot.transform.position, robPad.transform.position);

        Ray robotRay = new Ray(robot.transform.position, robot.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(robotRay.origin, robotRay.direction * 10, Color.red, 5);
       
        if (Physics.Raycast(robotRay, out hit, 5.0f))
        {
            Debug.Log(hit.transform.gameObject.name);
            //������5�ȉ��Ȃ�
            if (Input.GetKeyDown(KeyCode.V) && hit.collider.CompareTag("Player"))
            {
                SendCode();
            }
        }
        
    }

    public void SendCode()
    {
        accessCode.transform.parent = robPad.transform;
    }
}
