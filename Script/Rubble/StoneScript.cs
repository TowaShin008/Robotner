using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{

    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > 180) return;
        counter++;
        if (counter == 180)
        {
            Rigidbody rig = GetComponent<Rigidbody>();
            rig.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player"||
           collision.gameObject.name == "enemy" ||
           collision.gameObject.name == "enemy2")
        {
            Rigidbody rig = GetComponent<Rigidbody>();
            rig.constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("Hit");
        }
    }
}
