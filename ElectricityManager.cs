using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityManager : MonoBehaviour
{
    //��d�܂ł̎��ԁi�Œ�j
    [SerializeField]  float maxoffTime = 10;
    //��d�܂ł̌o�ߎ���
    float offElectricityTiem;
    //��d���
    bool blackOut;
    private List<GameObject> lightObject=new List<GameObject>();
    private List<GameObject> electricity = new List<GameObject>();

    [SerializeField] GameObject shutter;
    [SerializeField] int lightNum;
    [SerializeField] int electNum;
    //���݋N�����̃u���[�J�[
    int nowActiveElectricity = 0;
    // Start is called before the first frame update
    void Start()
    {
        //���C�g�̎擾
        for(int i = 0; i < lightNum; i++) 
        {
            lightObject.Add(GameObject.Find("Point Light (" + i + ")"));
            //lightObject[i].SetActive(true);
        }
        //�d�Cswitch�擾
        for(int i = 0; i < electNum; i++) 
        {
            electricity.Add(GameObject.Find("Electricity" + i));
            //�ŏ������A�N�e�B�u��
            if (i != 0)
            {
                electricity[i].SetActive(false);
            }
        }
        offElectricityTiem = maxoffTime;
        blackOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        //���Ԃ̌��Z
        TimeSubtract();
        //���͈͓��ɂ��Ă��{�^���������ꂽ��B
        if (Input.GetKey(KeyCode.Space))
        {
            //��d���Ă���
            if (blackOut)
            {
                //��d����
                offElectricityTiem = maxoffTime;
                //�S���C�g���A�N�e�B�u��
                for (int i = 0; i < lightNum; i++)
                {
                    lightObject[i].SetActive(true);
                }
                //�u���[�J�[�؂�ւ�
                NowBreakerChange();
            }
        }
    }
    //���Ԃ̌��Z
    void TimeSubtract()
    {
        if (offElectricityTiem >= 0)
        {
            blackOut = false;
            offElectricityTiem -=Time.deltaTime;
        }
        else
        {
            //��d���
            blackOut = true;
            //�S���C�g���A�N�e�B�u��
            for (int i = 0; i < lightNum; i++)
            {
                lightObject[i].SetActive(false);
            }
        }
    }
    void NowBreakerChange() 
    {
        //���̃A�N�e�B�u�֐؂�ւ�
        nowActiveElectricity++;
        if (nowActiveElectricity == electNum)
        {
            nowActiveElectricity = 0;
        }
        //�u���[�J�[�̃A�N�e�B�u�؂�ւ�
        for (int i = 0; i < electNum; i++)
        {
            if (i == nowActiveElectricity)
            {
                electricity[i].SetActive(true);
            }
            else
            {
                electricity[i].SetActive(false);
            }
        }
    }
}
