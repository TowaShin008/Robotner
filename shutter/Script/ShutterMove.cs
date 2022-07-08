using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterMove : MonoBehaviour
{
    public GameObject Shutternum; //Shutter���̂��̂�����ϐ�
    public bool Open = true;
    //�����̍���
    float height = 3.0f;
    Vector3 finpos;

    void Start()
    {
        if (Open == false)
        {
            //�f�t�H���g�̐F
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            //�f�t�H���g�̐F
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Open == false && Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 10.0f)) //�A�N�V�����{�^�����������Ƃ�
        {
            if (hit.collider == this.GetComponent<Collider>())
            {
                finpos = Shutternum.transform.position;
                finpos.y = finpos.y + height;

                Shutternum.transform.position = finpos;
                GetComponent<Renderer>().material.color = Color.red;

                Open = true;
            }
        }
        else if (Open == true && Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 10.0f))
        {
            if (hit.collider == this.GetComponent<Collider>())
            {
                finpos = Shutternum.transform.position;
                finpos.y = finpos.y - height;

                Shutternum.transform.position = finpos;
                Open = false;
                GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }
}