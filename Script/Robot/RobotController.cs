using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RobotController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private float speed = 0.08f;
    private bool stop = true;
    private Vector3 vec = Vector3.zero;
    TransmitterCounter transmitterCounter;
    [SerializeField] private GameObject textObject;
    private Text textComponent;

    // Start is called before the first frame update
    void Start()
    { 
        rigidbody = GetComponent<Rigidbody>();
        transmitterCounter = GameObject.Find("TransmitterCounter").GetComponent<TransmitterCounter>();
        textComponent = textObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stop == true)
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

        transform.position += vec;
         
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
            StopOnOff();
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

        for (int i = 0; i <= 5; i++)
        {
            transform.position -= vec;
        }

        stop = true;
        textComponent.text = "Ž©“®‘€c:OFF";
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

    public void StopOnOff()
    {
        if(stop == true) { 
            stop = false;
            textComponent.text = "Ž©“®‘€c:ON";
        }
        else { 
            stop = true;
            textComponent.text = "Ž©“®‘€c:OFF";
        }
    }

}
