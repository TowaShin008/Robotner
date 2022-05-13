using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    // Start is called before the first frame update
    //���e����J�����̏��
    private bool active=false;
    //�P�t���[���O�̃J�����̃A�N�e�B�u���
    private bool saveActive = false;
    //�J�����i�[�p
    private List<GameObject> cameraList = new List<GameObject>();
    //�J�����̑S�̂̍��v��
    //�C���X�y�N�^�[��ɕ\��
    [SerializeField]
    public int cameraNum=3;
    //���݂̕\���J����
    private int nowWatchCamera = 0;
    void Start()
    {
        //���C���ƃT�u���擾
        for (int i = 0; i < cameraNum; i++)
        {
            //�S�ẴJ�������擾
            cameraList.Add(GameObject.Find("SubCamera" + i));
            //�ŏ��̈�ڈȊO��
            if (i != nowWatchCamera)
            {  
                //��A�N�e�B�u��
                cameraList[i].SetActive(active);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�P�t���[���O�̃A�N�e�B�u���
        saveActive = active;
        if (Input.GetKeyDown("space"))
        {
            //�A�N�e�B�u��ԕύX
            active =! active;
            //���̃J�����ɕύX
            nowWatchCamera++; 
        } 
        //�A�N�e�B�u��Ԃ��؂�ւ������
        if(active!=saveActive)
        {
            //�����Ă���J�������ő吔�ɂȂ�����ŏ��̃J�����ɖ߂�
            if (nowWatchCamera == cameraNum)
            {
                nowWatchCamera = 0;
            }
            //�����Ă���J�����������A�N�e�B�u��Ԃ�
            cameraList[nowWatchCamera].SetActive(active);
            //�J�����̍ő吔���A�N�e�B�u��Ԃ�false�ɂ���
            for (int i = 0; i < cameraNum; i++)
            {
                if (nowWatchCamera != i)
                {
                    cameraList[i].SetActive(!active);
                }
            }            
        }
        //�ύX�I��
        active = false;
    }
}
