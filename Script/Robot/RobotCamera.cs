using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCamera : MonoBehaviour
{
    [SerializeField] GameObject robotCamera;
    [SerializeField] GameObject robot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = robot.transform.position;
        robotCamera.transform.position = pos;
        Quaternion rot = robot.transform.rotation;
        robotCamera.transform.rotation = rot;
    }
}
