using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    float x, z;
    float speed = 0.1f;

    Vector3 characterPos;
    float playerBasePosY;

    public GameObject cam;
    Quaternion cameraRot, characterRot;

    public GameObject tabletPivot;
    Quaternion tabletPivotRot;

    bool tabletPowerFlag;

    float Xsensityvity = 3f, Ysensityvity = 3f;

    bool deadFlag;

    //�ϐ��̐錾(�p�x�̐����p)
    float minX = -20f, maxX = 20f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        cameraRot = cam.transform.localRotation;
        characterPos = transform.position;
        playerBasePosY = characterPos.y;
        characterRot = transform.localRotation;
        tabletPivotRot = tabletPivot.transform.localRotation;
        tabletPowerFlag = false;
        deadFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);

        //Update�̒��ō쐬�����֐����Ă�
        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;

        //�^�u���b�g�N�����̂ݎ��_�̉��ړ��𐧌�
        if (tabletPowerFlag == false)
        {
            float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
            characterRot *= Quaternion.Euler(0, xRot, 0);
            transform.localRotation = characterRot;
        }


        //�^�u���b�g�̑��쏈��
        TabletProcessing();

        if(tabletPowerFlag == false)
        {
            //�ړ�����
            MoveProcessing();
        }
    }

    //�^�u���b�g�̑��쏈��
    private void TabletProcessing()
    {
        if(tabletPowerFlag)
        {
            if(TabletBootProcessing())
            {//���S�ɋN�����Ă���ꍇ�̂݃V���b�g�_�E�����󂯕t����
                if (Input.GetKey(KeyCode.E))
                {
                    minX = -90f;
                    maxX = 90f;
                    tabletPowerFlag = false;
                }
            }
        }
        else
        {
            if (TabletShutDownProcessing())
            {//���S�ɃV���b�g�_�E�����Ă���ꍇ�̂݋N�����󂯕t����
                if (Input.GetKey(KeyCode.E))
                {
                    minX = -20f;
                    maxX = 20f;
                    tabletPowerFlag = true;
                }
            }
        }
    }
    //�^�u���b�g�̋N������
    private bool TabletBootProcessing()
    {
        tabletPivot.transform.localRotation.ToAngleAxis(out float angle, out Vector3 axis);
        if (angle <= 0.0f)
        {
            return true;
        }
        else
        {
            Quaternion rot = Quaternion.AngleAxis(-5, Vector3.right);
            tabletPivotRot *= rot;
            tabletPivot.transform.localRotation = tabletPivotRot;
        }

        return false;
    }
    //�^�u���b�g�̃V���b�g�_�E������
    private bool TabletShutDownProcessing()
    {
        tabletPivot.transform.localRotation.ToAngleAxis(out float angle, out Vector3 axis);
        if (angle >= 180.0f)
        {
            return true;
        }
        else
        {
            Quaternion rot = Quaternion.AngleAxis(5, Vector3.right);
            tabletPivotRot *= rot;
            tabletPivot.transform.localRotation = tabletPivotRot;
        }
        return false;
    }
    //�v���C���[�̈ړ�����
    private void MoveProcessing()
    {
        //���Ɉړ�
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(-0.01f, 0.0f, 0.0f);
        }
        //�E�Ɉړ�
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(0.01f, 0.0f, 0.0f);
        }
        //��Ɉړ�
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(0.0f, 0.0f, 0.01f);
        }
        //���Ɉړ�
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0.0f, 0.0f, -0.01f);
        }
        characterPos = this.transform.position;

        //�v���C���[�������Ȃ��悤�Ɉ��̍����ŌŒ�
        characterPos.y = playerBasePosY;
        this.transform.position = characterPos;
    }

    private void FixedUpdate()
    {
        if (tabletPowerFlag) { return; }
        
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;
        z = Input.GetAxisRaw("Vertical") * speed;

        //transform.position += new Vector3(x,0,z);

        transform.position += cam.transform.forward * z + cam.transform.right * x;
    }

    //�p�x�����֐��̍쐬
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,z�̓x�N�g���i�ʂƌ����j�Fw�̓X�J���[�i���W�Ƃ͖��֌W�̗ʁj)

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX, minX, maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="Enemy")
        {
            Debug.Log("Hit");
            deadFlag = true;
        }
    }
}
