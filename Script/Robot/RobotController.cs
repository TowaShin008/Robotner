using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RobotController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private float speed = 0.02f;
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
        if (modeAuto == false && isBack == true)
        {
            backCount++;

            if (backCount <= 30)
            {
                transform.position -= vec * 2;
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
       


        if (Input.GetKey(KeyCode.W))
        {
            MoveForward();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MoveBack();
        }
        if (isBack == false && isTurn == false)
        {
            transform.position += vec;
        }


        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
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

        //”­M‹@Ý’u
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //rigidbody.AddForce(new Vector3(0, 0, 5));
            if(transmitterCounter.remaining >= 1) {
                GameObject.Find("TransmitterCounter").GetComponent<TransmitterCounter>().SubRemaining();

                GameObject.Find("TransmitterSpawner").GetComponent<TransmitterSpawner>().Spawn();
            }
            
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Wall") { return; }

        //for (int i = 0; i <= 10; i++)
        //{
        //    transform.position -= vec;
        //}

        if(modeAuto == true)
        {
            isBack = true;
            backCount = 0;
            modeAuto = false;
            textComponent.text = "Ž©“®‘€c:OFF";

            if (modeTurn == true)
            {
                RotateRight();
                modeTurn = false;
                isTurn = true;
                turnCount = 0;                
                textComponent2.text = "‰ñ“]:OFF";
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
        if(modeAuto == false) { 
            modeAuto = true;
            textComponent.text = "Ž©“®‘€c:ON";
        }
        else { 
            modeAuto = false;
            textComponent.text = "Ž©“®‘€c:OFF";
        }
    }
    public void ModeTurnOnOff()
    {
        if (modeTurn == true)
        {
            modeTurn = false;
            textComponent2.text = "‰ñ“]:OFF";
        }
        else
        {
            modeTurn = true;
            textComponent2.text = "‰ñ“]:ON";
        }
    }
}
