using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //rigidbody.AddForce(new Vector3(0, 0, 5));
            transform.position += transform.forward * speed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //rigidbody.AddForce(new Vector3(0, 0, -5));
            transform.position -= transform.forward * speed;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //rigidbody.AddForce(new Vector3(-5, 0, 0));
            transform.Rotate(new Vector3(0, -45, 0) * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //rigidbody.AddForce(new Vector3(5, 0, 0));
            transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime, Space.World);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
