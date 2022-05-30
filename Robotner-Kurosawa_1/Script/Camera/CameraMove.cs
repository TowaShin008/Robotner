using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    //�ő�U������p�x
   // private float MaxRotate = 90;
    //�U������X�s�[�h
    //[SerializeField]
    private float RotSpeed = 30;
    //
    private float RotY = 0f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //���݂̊p�x����{�[90�����w��
        //����ȏ�ɍs���Ȃ��悤�ɂ���
        //��
        if (Input.GetMouseButton(0)) 
        {
            RotY = -RotSpeed;
        }
        //��
        if (Input.GetMouseButton(1)) 
        {
            RotY = RotSpeed;
        }
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + RotY * Time.deltaTime,
           transform.eulerAngles.z);

        RotY = 0;
    }
}
