using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class RobotController : MonoBehaviour
{
    private Rigidbody rigidbody;
    float x, z;
    [SerializeField] private float speed = 0.2f;
    private bool modeAuto = false;
    private bool modeTurn = false;
    private bool modeTracking = false;

    private bool right = true;
    private bool isBack = false;
    private bool isTurn = false;
    [SerializeField] private bool isBreak = false;
    private int turnCount = 0;
    private int backCount = 0;
    private Vector3 vec = Vector3.zero;
    TransmitterCounter transmitterCounter;
    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject textObject2;
    private Text textComponent;
    private Text textComponent2;
    [SerializeField] private GameObject RobotScene;
    [SerializeField] private GameObject player;

    public float knockback;

    private bool move;
    public AudioClip clip;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transmitterCounter = GameObject.Find("TransmitterCounter").GetComponent<TransmitterCounter>();
        textComponent = textObject.GetComponent<Text>();
        textComponent2 = textObject2.GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (modeTracking == true && Vector3.Distance(transform.position, player.transform.position)! > 5.0f && RobotScene.activeInHierarchy == false)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
            MoveForward();
            transform.position += vec;
            move = true;
        }

        if (move == true)
        {
            audioSource.mute = false;
        }
        else
        {
            audioSource.mute = true;
        }

        if (RobotScene.activeInHierarchy == false)
        {
            //audioSource.mute = true;
            return;
        }

        //���̉�ꂽ���Ƀ��{�b�g�𒼂��{�^������
        if (Input.GetKeyDown(KeyCode.B))
        {
            ModeBreakOnOff();
        }
        if (isBreak != false)
        {
            audioSource.mute = true;
            return;
        }
        move = false;

        //�������c���I���̎��A�����Ă�������ɐi�ݑ�����
        if (modeAuto == true)
        {
            MoveForward();
            move = true;
        }

        //�������c���I���̎��A�ǂɂԂ������班���o�b�N����
        if (/*modeAuto == false &&*/ isBack == true)
        {
            backCount++;

            if (backCount <= 30)
            {
                transform.position -= vec * knockback;
            }
            else
            {
                isBack = false;
            }
        }
        else if (modeAuto == false && isTurn == true)
        {
            turnCount++;

            if (turnCount < 360)
            {
                RotateRight();
            }
            else
            {
                isTurn = false;
            }
        }
        else if (modeAuto == false)
        {
            vec = Vector3.zero;
        }

        //���{�b�g�̈ړ�����
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        if (z > 0)
        {
            MoveForward();
            move = true;
        }
        else if (z < 0)
        {
            MoveBack();
            move = true;
        }

        if (isBack == false && isTurn == false)
        {
            rigidbody.MovePosition(transform.position + vec);
            //transform.position += vec;
        }

        //���{�b�g�̌����̑���
        if (x < 0)
        {
            RotateLeft();
            move = true;
        }
        else if (x > 0)
        {
            RotateRight();
            move = true;
        }


        if (x == 0 && z == 0 && modeAuto == false && modeTracking == false)
        {
            move = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (modeAuto == true && modeTracking == false)
            {
                ModeAutoOnOff();
                ModeTrackingOnOff();
            }
            else if (modeAuto == false && modeTracking == false)
            {
                ModeAutoOnOff();
            }
            else if (modeAuto == false && modeTracking == true)
            {
                ModeTrackingOnOff();
            }
            else
            {
                //���������̕s��łǂ����true�Ȃ�
                ModeAutoOnOff();
                ModeTrackingOnOff();
            }

        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ModeTurnOnOff();
        }

        //���M�@�ݒu
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //rigidbody.AddForce(new Vector3(0, 0, 5));
            if (transmitterCounter.remaining >= 1)
            {
                GameObject.Find("TransmitterCounter").GetComponent<TransmitterCounter>().SubRemaining();

                GameObject.Find("TransmitterSpawner").GetComponent<TransmitterSpawner>().Spawn();
            }

        }


    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Enemy") { return; }
        if (isBreak == false)
        {
            ModeBreakOnOff();
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Wall") { return; }

        if (modeAuto == true)
        {
            isBack = true;
            backCount = 0;
            //modeAuto = false;
            textComponent.text = "�������c:OFF";

            if (modeTurn == true)
            {
                RotateRight();
                modeTurn = false;
                isTurn = true;
                turnCount = 0;
                textComponent2.text = "��]:OFF";
            }
        }

        vec = Vector3.zero;
    }


    public void MoveForward()
    {
        vec = transform.forward * speed;
    }

    public void MoveBack()
    {
        vec = -(transform.forward * speed);
    }

    public void RotateRight()
    {
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime, Space.World);
    }
    public void RotateLeft()
    {
        transform.Rotate(new Vector3(0, -90, 0) * Time.deltaTime, Space.World);
    }

    public void ModeAutoOnOff()
    {
        if (modeAuto == false)
        {
            modeAuto = true;
            textComponent.text = "�������c:ON";
        }
        else
        {
            modeAuto = false;
            textComponent.text = "�������c:OFF";
        }
    }
    public void ModeTurnOnOff()
    {
        if (modeTurn == true)
        {
            modeTurn = false;
            textComponent2.text = "��]:OFF";
        }
        else
        {
            modeTurn = true;
            textComponent2.text = "��]:ON";
        }
    }

    public void ModeTrackingOnOff()
    {
        if (modeTracking == false)
        {
            modeTracking = true;
            textComponent.text = "�Ǐ]:ON";
        }
        else
        {
            modeTracking = false;
            textComponent.text = "�Ǐ]:OFF";
        }
    }

    public void ModeBreakOnOff()
    {
        if (isBreak == true)
        {
            isBreak = false;
        }
        else
        {
            isBreak = true;
        }
    }
}
