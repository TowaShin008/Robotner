using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathplayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<FPSController>().GetDeadFlag())
        {
            
            gameObject.transform.position = player.transform.position;

            gameObject.transform.rotation = player.transform.rotation;
        }
    }
}
