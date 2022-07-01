using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFlag : MonoBehaviour
{
    public bool is_Open; //�h�A���J�������ǂ����̕ϐ�
    public KeyCode keyCode; //�ǂ̃L�[����͂��邩�̕ϐ�
    public Material mat;

    GameObject Shutter2; //Shutter���̂��̂�����ϐ�

    ShutterMove script; //ShutterMoveScript������ϐ�

    public enum Situation
    {
        Open,
        Opening,
        Shutouting,
        Shutout
    }

    Situation situation = Situation.Shutout;

    void Start()
    {
        //Shutter2���I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        Shutter2 = GameObject.Find("Shutter2");
        //Shutter2�̒��ɂ���ShutterMove���擾���ĕϐ��Ɋi�[����
        script = Shutter2.GetComponent<ShutterMove>();
        //�f�t�H���g�̐F
        GetComponent<Renderer>().material.color = Color.blue;

        situation = Situation.Shutout;

        switch (situation)
        {
            case Situation.Open:
                Debug.Log("�J���Ă���");
                break;
            case Situation.Opening:
                Debug.Log("�J���r��");
                break;
            case Situation.Shutouting:
                Debug.Log("�ߓr��");
                break;
            case Situation.Shutout:
                Debug.Log("�܂��Ă�");
                break;
        }
    }

    //�ǂ��炩��Is Trigger���I���ɂȂ��Ă���Collider���m���ڐG���Ă���ԏ�ɌĂяo��
    //private void OnTriggerStay(Collider obj)
    void Update()
    {
       
        //����̃{�^���������Ă���is_Open��false�̎�is_Open��true�ɂ���
        if (Input.GetMouseButtonDown(0) && situation == Situation.Shutout)
        {            
            script.Up();
            situation = Situation.Opening;
            GetComponent<Renderer>().material.color = Color.red;
            if ( situation == Situation.Opening)
            {
                situation = Situation.Open;
                Debug.Log("�J!");
            }
        } 

        // ����̃{�^���������Ă���is_Open��true�̎�is_Open��false�ɂ���
      else if (Input.GetMouseButtonDown(0) && situation == Situation.Open) 
        {     
            situation = Situation.Shutouting;
            GetComponent<Renderer>().material.color = Color.blue;
            script.Down();
            if (situation == Situation.Shutouting)
            {
                situation = Situation.Shutout;
                Debug.Log("��!");
            }
        }
    }
}
