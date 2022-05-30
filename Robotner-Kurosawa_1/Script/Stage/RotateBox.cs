using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBox : MonoBehaviour
{

    private bool stay = false;

    public int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > 5) return;
        if (!stay)
        {
            gameObject.transform.Rotate(0, 90, 0);
            //Debug.Log("rot");
            counter++;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("hit");
        stay = true;
    }
}
