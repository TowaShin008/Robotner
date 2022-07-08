using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    //�����̃X�N���v�g
    [SerializeField]GameObject plate;
    //�J�������̍ő�l
    [SerializeField]float maxHight;
    //�J���X�s�[�h
    [SerializeField] float speed;
    //�J���t���O
    bool openFlag = false;
    //����
    int timer = 0;
    //�܂�܂ł̗P�\����
    int shutTime = 30;
    //���ݒn�̕ۑ�
    Vector3 savePosotion;
    PressureSensitivePlate plateScript;
    // Start is called before the first frame update
    void Start()
    {
        plateScript = plate.GetComponent<PressureSensitivePlate>();

        savePosotion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //���������܂ꂽ��
        if (plateScript.GetHitFlag())
        {
            openFlag = true;
            timer = 0;
        }
        if (timer == 0 && !openFlag)
        {
            if (transform.position.y >= savePosotion.y)
            {
                Vector3 pos = transform.position;
                pos.y -= speed;
                transform.position = pos;

            }
        }
        if (openFlag)
        {
            if (transform.position.y <= maxHight)
            {
                Vector3 pos = transform.position;
                pos.y += speed;
                transform.position = pos;
            }
            timer++;
            if (timer > shutTime) 
            {
                timer = 0;
                openFlag = false;
            }
        }
       
    }
}
