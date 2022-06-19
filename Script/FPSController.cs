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

    //�^�u���b�g����]�����_
    public GameObject tabletPivot;
    Quaternion tabletPivotRot;

    bool tabletPowerFlag;

    //XY�����̎��_���x
     public float Xsensityvity, Ysensityvity;

    bool deadFlag;

    //�ϐ��̐錾(�p�x�̐����p)
    float minX;
    float maxX;
    bool checkController;
    float yRot;
    float xRot;
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
        const float normalMaxX = 90f;
        const float normalMinX = -90f;
        minX = normalMinX;
        maxX = normalMaxX;
        //�R���g���[���[�ڑ��m�F
        var controllerNames = Input.GetJoystickNames();

        //�����ڑ�����Ă��Ȃ��Ȃ�}�E�X�̏���
        if (controllerNames[0] == "")
        {
            //�ڑ�����ĂȂ�����}�E�X
            checkController = false;
        }
        else
        {
            //�ڑ�����Ă���R���g���[���[
            checkController = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkController = true;   
        float x = Input.GetAxis("lightJoyStickX");
        float y = Input.GetAxis("lightJoyStickY");
        if (checkController)
        {
            if (y != 0)
            {
                yRot = -y* Xsensityvity;
            }
            else 
            {
                yRot = y;
            }
        }
        else
        {
            yRot = Input.GetAxis("Mouse Y") * Xsensityvity;
        }
        cameraRot *= Quaternion.Euler(-yRot, 0, 0);

        //Update�̒��ō쐬�����֐����Ă�
        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;

        //�^�u���b�g�N�����̂ݎ��_�̉��ړ��𐧌�
        if (tabletPowerFlag == false)
        {
            if (checkController)
            {
                if (x != 0)
                {
                    xRot = x * Ysensityvity;
                }
                else
                {
                    xRot = x;
                }
            }
            else
            {

                xRot = Input.GetAxis("Mouse X") * Ysensityvity;
            }
            characterRot *= Quaternion.Euler(0, xRot, 0);
            transform.localRotation = characterRot;
        }


        //�^�u���b�g�̑��쏈��
        TabletProcessing();
    }

    //�^�u���b�g�̑��쏈��
    private void TabletProcessing()
    {
        if (tabletPowerFlag)
        {
            if (TabletBootProcessing())
            {//���S�ɋN�����Ă���ꍇ�̂݃V���b�g�_�E�����󂯕t����
                if (Input.GetKey(KeyCode.Tab))
                {
                    const float normalMaxX = 90f;
                    const float normalMinX = -90f;
                    minX = normalMinX;
                    maxX = normalMaxX;
                    tabletPowerFlag = false;
                }
            }
        }
        else
        {
            if (TabletShutDownProcessing())
            {//���S�ɃV���b�g�_�E�����Ă���ꍇ�̂݋N�����󂯕t����
                if (Input.GetKey(KeyCode.Tab))
                {
                    const float tabletMaxX = 5f;
                    const float tabletMinX = -5f;
                    minX = tabletMinX;
                    maxX = tabletMaxX;
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

    private void FixedUpdate()
    {
        if (tabletPowerFlag) { return; }

        x = 0;
        z = 0;

        //�v���C���[�ړ�����
        x = Input.GetAxisRaw("Horizontal") * speed;
        z = Input.GetAxisRaw("Vertical") * speed;

        characterPos = this.transform.position;

        //�v���C���[�������Ȃ��悤�Ɉ��̍����ŌŒ�(Y���̃|�W�V�������Œ�)
        characterPos.y = playerBasePosY;
        this.transform.position = characterPos;

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
        if (collision.gameObject.name == "Enemy")
        {
            Debug.Log("Hit");
            deadFlag = true;
        }
    }
}
