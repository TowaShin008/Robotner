using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag != "Enemy") { return; }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "Enemy") { return; }

        //ここにエネミーに当たった時の処理を書く
    }
}
