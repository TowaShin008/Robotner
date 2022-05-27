using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{

    public Vector3 cameraPos;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = cameraPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
