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
    //���S�t���O
    bool deadFlag;
    //�Q�[���J�n�t���O
    bool gameStartFrag;
    float wakeUpRotation=90;
    public float wakeUpSpeed;
    int wakeUpCase = 0;
    public float trunSpeed;
    Vector3 savePos;
    Vector3 axis;
    float angle;
    int count;
    public GameObject panelScript;

    //�ϐ��̐錾(�p�x�̐����p)
    float minX;
    float maxX;

    bool squatFlag;
    //��
    public AudioClip clip;
    public AudioClip walkClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource walkAudioSource;
    bool walk = false;
    int warktime = 0;

    Vector3 savecam;
    float deathrotation = 0;
    [SerializeField] int rotspeed;
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
        gameStartFrag = false;

        audioSource.clip = clip;

        walkAudioSource.clip = walkClip;
        walkAudioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameStartFrag)
        {
            if (!deadFlag)
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
                savecam = cam.transform.position;

            }
            else
            {
                Quaternion rot = transform.localRotation;
                rot = Quaternion.Euler(deathrotation, 0, 0);
                transform.localRotation = rot;
                if (deathrotation < 90)
                {
                    deathrotation += rotspeed;
                }
                else
                {
                    Vector3 campos = cam.transform.position;
                    campos.y = savecam.y + 5;
                    cam.transform.position = campos;
                    deathrotation = 90;
                }
            }
        }
        else
        {
            //�����ŃQ�[���J�n�̓����ǉ�
            //������点�� 
            Quaternion rot = transform.localRotation;
            Vector3 pos = gameObject.transform.position;

            switch (wakeUpCase)
            {
                case 0:
                    float sin = Mathf.Sin(Time.time);

                    //���E�ς��ς�
                    //���E�U�����
                   

                   
                    axis = new Vector3(0, 0, 1);

                    angle = sin * trunSpeed;
                    Quaternion q1 = Quaternion.AngleAxis(180 - angle, axis);
                    axis = new Vector3(1, 0, 0);
                    Quaternion q2 = Quaternion.AngleAxis(-90, axis);
                    rot = q1 * q2;
                    savePos = gameObject.transform.position;

                    if (0<sin&&sin<0.05)
                    {
                        count++;
                    }
                    if (count > 3)
                    {
                        wakeUpCase = 1;
                    }

                    break;
                case 1:
                    //�����N���オ��G�܂�
                    if (savePos.y+3.0>pos.y)
                    {
                        pos.y += 0.1f;
                    }
                    else
                    {
                        wakeUpCase = 2;
                    }
                    gameObject.transform.position = pos;

                    break;
                case 2:
                    //�N���オ�蓪�܂ŁA��(�J����)��]
                    if (wakeUpRotation >= 0)
                    {
                        wakeUpRotation -= wakeUpSpeed;
                    }
                    else
                    {
                        wakeUpRotation = 0;
                        gameStartFrag = true;
                        tabletPowerFlag = true;
                        panelScript.GetComponent<ShowPanel>().ShowPad();
                    }
                    rot = Quaternion.Euler(wakeUpRotation, 180, 0);
                    break;
            }
            //rot = Quaternion.Euler(wakeUpRotation, trunangle, 0);
            transform.localRotation = rot;

        }
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
                    audioSource.PlayOneShot(clip);
                    walk = false;
                    walkAudioSource.mute = true;
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
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            walk = true;
        }
        else
        {
            walk = false;
        }

        if (walk == true)
        {
            walkAudioSource.mute = false;
        }
        else
        {
            walkAudioSource.mute = true;
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
        for (int i = 1; i < 5; i++)
        {
            if (collision.gameObject.name == "enemy"+i)
            {
                Debug.Log("Hit");
                deadFlag = true;
            }
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
    public bool GetDeadFlag()
    {
        return deadFlag;
    }
}
