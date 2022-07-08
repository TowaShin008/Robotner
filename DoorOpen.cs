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
            if (transform.position.y < maxHight)
            {
                Vector3 pos = transform.position;
                pos.y += speed;
                transform.position = pos;
            }
        }
        else 
        {
            if (transform.position.y > savePosotion.y)
            {
                Vector3 pos = transform.position;
                pos.y -= speed;
                transform.position = pos;

            }
        }
    }
}
