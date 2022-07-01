using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RobotController : MonoBehaviour
{
    private Rigidbody rigidbody;
    float x, z;
    private float speed = 0.05f;
    private bool modeAuto = false;
    private bool modeTurn = false;
    private bool right = true;
    private bool isBack = false;
    private bool isTurn = false;
    private int turnCount = 0;
    private int backCount = 0;
    private Vector3 vec = Vector3.zero;
    TransmitterCounter transmitterCounter;
    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject textObject2;
    private Text textComponent;
    private Text textComponent2;
    [SerializeField] private GameObject RobotScene;

    public float knockback;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transmitterCounter = GameObject.Find("TransmitterCounter").GetComponent<TransmitterCounter>();
        textComponent = textObject.GetComponent<Text>();
        textComponent2 = textObject2.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RobotScene.activeInHierarchy == false) { return; }

        //自動操縦がオンの時、向いている方向に進み続ける
        if (modeAuto == true)
        {
            MoveForward();
        }

        //自動操縦がオンの時、壁にぶつかったら少しバックする
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

        //ロボットの移動操作
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        if (z > 0)
        {
            MoveForward();
        }
        else if (z < 0)
        {
            MoveBack();
        }

        if (isBack == false && isTurn == false)
        {
            transform.position += vec;
        }

        //ロボットの向きの操作
        if (x < 0)
        {
            RotateLeft();
        }
        else if (x > 0)
        {
            RotateRight();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ModeAutoOnOff();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ModeTurnOnOff();
        }

        //発信機設置
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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Wall") { return; }

        if (modeAuto == true)
        {
            isBack = true;
            backCount = 0;
            //modeAuto = false;
            textComponent.text = "自動操縦:OFF";

            if (modeTurn == true)
            {
                RotateRight();
                modeTurn = false;
                isTurn = true;
                turnCount = 0;
                textComponent2.text = "回転:OFF";
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
            textComponent.text = "自動操縦:ON";
        }
        else
        {
            modeAuto = false;
            textComponent.text = "自動操縦:OFF";
        }
    }
    public void ModeTurnOnOff()
    {
        if (modeTurn == true)
        {
            modeTurn = false;
            textComponent2.text = "回転:OFF";
        }
        else
        {
            modeTurn = true;
            textComponent2.text = "回転:ON";
        }
    }
}
