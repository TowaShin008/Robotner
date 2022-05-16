using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private float speed = 0.02f;
    TransmitterCounter transmitterCounter;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transmitterCounter = GameObject.Find("TransmitterCounter").GetComponent<TransmitterCounter>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveForward();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveBack();
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
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

    private void OnCollisionEnter(Collision collision)
    {
    }

    public void MoveForward()
    {
        transform.position += transform.forward * speed;
    }

    public void MoveBack()
    {
        transform.position -= transform.forward * speed;
    }

    public void RotateRight()
    {
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime, Space.World);
    }
    public void RotateLeft()
    {
        transform.Rotate(new Vector3(0, -90, 0) * Time.deltaTime, Space.World);
    }
}
