using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendAccessCode : MonoBehaviour
{
    public GameObject robPad;
    public GameObject robot;
    public GameObject accessCode;

    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ロボットとロボパッドの距離を求める
        float distance = Vector3.Distance(robot.transform.position, robPad.transform.position);

        Ray robotRay = new Ray(robot.transform.position, robot.transform.forward);

        Debug.DrawRay(robotRay.origin, robotRay.direction * 10, Color.red, 5);
       
        //if (Physics.Raycast(robotRay, out hit, 5.0f))
        //{
        //    Debug.Log(hit.transform.gameObject.name);
        //    
        //    if (Input.GetKeyDown(KeyCode.V) && hit.collider.CompareTag("Player"))
        //    {
        //        SendCode();
        //    }
        //}

        //　Cubeのレイを飛ばしターゲットと接触しているか判定
        if (Physics.BoxCast(robot.transform.position, Vector3.one * 1.0f, robot.transform.forward, out hit, Quaternion.identity, 5.0f))
        {
            Debug.Log(hit.transform.gameObject.name);

            if (Input.GetKeyDown(KeyCode.V) && hit.collider.CompareTag("Player"))
            {
                SendCode();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(robot.transform.position + robot.transform.forward * hit.distance, Vector3.one * 1.5f);
    }

    public void SendCode()
    {
        accessCode.transform.parent = robPad.transform;
    }
}
