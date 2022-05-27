using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAccessCode : MonoBehaviour
{
    public int accessCodeNumber = -1;

    // Start is called before the first frame update
    void Start()
    {
        accessCodeNumber = Random.Range(1, 999999 + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
