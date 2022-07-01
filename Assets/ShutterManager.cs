using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterManager : MonoBehaviour
{

    [SerializeField]
   // private SwitchFlag gimicControl; // �M�~�b�N�Ǘ��p�X�N���v�g
    

    //private BoxCollider collider; // �Q�[�g�̃R���C�_�[
    public GameObject Shutternum; //Shutter���̂��̂�����ϐ�
   // public ShutterMove script; //ShutterMoveScript������ϐ�
    public bool Open = true;
    public bool actionFlug;

    Vector3 finpos;  //�@���̕ϐ��錾
    private static readonly Vector3 vector3 = new(0.5f, 5f, 2f);

    //�ǂ��Ɍ������ď㏸�E���~���邩
    Vector3 directionUp = vector3;
    Vector3 directionDown = new(0.5f, 0.5f, 2f);
    //�����̍���
    float heightUp = 3.0f;
    float heightDown = -3.0f;

    void Start()
    {
        //�f�t�H���g�̐F
        if(Open == false)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") // ����L�����̃I�u�W�F�N�g�^�O
        {
            GetComponent<Renderer>().material.color = Color.red;

        }
    }

  
        
         

        void Update()
        {

            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


        if (Open == false && Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 10.0f)) //�A�N�V�����{�^�����������Ƃ�
            {
                if (hit.collider.gameObject == gameObject)
                {
                    finpos = Shutternum.transform.position;
                    finpos.y = finpos.y + heightUp;

                    Shutternum.transform.position = finpos;

                GetComponent<Renderer>().material.color = Color.red;

                Open = true;
                }
            }
            else if (Open == true && Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 10.0f))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    finpos = Shutternum.transform.position;
                    finpos.y = finpos.y + heightDown;

                    Shutternum.transform.position = finpos;

                    Open = false;
                    GetComponent<Renderer>().material.color = Color.blue;
                }
            }

        }
    }


