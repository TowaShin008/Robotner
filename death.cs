using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour
{
    Vector3 playerpos;
    public GameObject playerDeath;
    public Camera maincamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDeath.GetComponent<FPSController>().GetDeadFlag()) 
        {
            Vector3 pos = transform.position;
            pos = playerpos;
            pos.y = playerpos.y+2;
            transform.position = pos;
        }
        else 
        {
            playerpos = maincamera.transform.position;
        }
    }
}
