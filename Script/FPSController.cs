using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    float x, z;
    float speed = 5.0f;

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

    bool squatFlag;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
        tabletPivotRot = tabletPivot.transform.localRotation;
        tabletPowerFlag = false;
        deadFlag = false;
        squatFlag = false;
        const float normalMaxX = 90f;
        const float normalMinX = -90f;
        minX = normalMinX;
        maxX = normalMaxX;
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

        //�v���C���[�̂��Ⴊ�ޓ��͏���
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {//���Ⴊ��ł��邩�̃t���O�؂�ւ�
            const float squatPositionY = 0.5f;
            Vector3 squatPosition = cam.transform.position;
            if (squatFlag == false)
            {
                squatFlag = true;
                squatPosition.y -= squatPositionY;
            }
            else
            {
                squatFlag = false;
                squatPosition.y += squatPositionY;
            }

            cam.transform.position = squatPosition;
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
                {//�^�u���b�g���N�����̎��_����͈͂ɕύX
                    const float normalMaxAngleX = 90f;
                    const float normalMinAngleX = -90f;
                    minX = normalMinAngleX;
                    maxX = normalMaxAngleX;
                    tabletPowerFlag = false;
                }
            }
        }
        else
        {
            if (TabletShutDownProcessing())
            {//���S�ɃV���b�g�_�E�����Ă���ꍇ�̂݋N�����󂯕t����
                if (Input.GetKey(KeyCode.Tab))
                {//�^�u���b�g�N�����̎��_����͈͂ɕύX
                    const float tabletMaxAngleX = 0.0f;
                    const float tabletMinAngleX = 0.0f;
                    minX = tabletMinAngleX;
                    maxX = tabletMaxAngleX;
                    tabletPowerFlag = true;
                }
            }
        }
    }
    //�^�u���b�g�̋N������
    private bool TabletBootProcessing()
    {
        tabletPivot.transform.localRotation.ToAngleAxis(out float angle, out Vector3 axis);

        bool endAngle = angle <= 0.0f;
        if (endAngle)
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

        bool endAngle = angle >= 180.0f;
        if (endAngle)
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

        //�v���C���[�ړ�����
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 velocity = gameObject.transform.rotation * new Vector3(0, 0, speed);
            gameObject.transform.position += velocity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 velocity = gameObject.transform.rotation * new Vector3(-speed, 0, 0);
            gameObject.transform.position += velocity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 velocity = gameObject.transform.rotation * new Vector3(0, 0, -speed);
            gameObject.transform.position += velocity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 velocity = gameObject.transform.rotation * new Vector3(speed, 0, 0);
            gameObject.transform.position += velocity * Time.deltaTime;
        }
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

    public bool GetSquatFlag()
    {
        return squatFlag;
    }

    public void SetSquatFlag(bool arg_squatFlag)
    {
        squatFlag = arg_squatFlag;
    }
}
