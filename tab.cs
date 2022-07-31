using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tab : MonoBehaviour
{
    // Start is called before the first frame update
    int counter = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (counter++ > 1)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
