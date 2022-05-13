using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    //�J�����̃g�����X�t�H�[���擾
    private Transform cameraTransform;
    //�ő�U������p�x
    private float MaxRotate = 90;
    //�U������X�s�[�h
    //[SerializeField]
    private float RotSpeed = 30;
    //
    private float RotY = 0f;
    void Start()
    {
        cameraTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //���݂̊p�x����{�[90�����w��
        //����ȏ�ɍs���Ȃ��悤�ɂ���
        //��
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            RotY = RotSpeed;
        }
        //��
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            RotY = -RotSpeed;
        }
        transform.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y + RotY * Time.deltaTime,
            0.0f);

        RotY = 0;
    }
}
