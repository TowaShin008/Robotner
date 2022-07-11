using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Vector3 saveScale;
    private bool openFlag;    
    private int timer = 0;
    [SerializeField, TooltipAttribute("’÷‚Ü‚é‚Ü‚Å‚ÌŽžŠÔ")] int maxtimer;
    [SerializeField, TooltipAttribute("c‚©‰¡‚©")] bool isX;

    // Start is called before the first frame update
    void Start()
    {
        saveScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer--;
        }
        else
        {
            openFlag = false;
        }

        Vector3 scale = gameObject.transform.localScale;
        Vector3 pos = gameObject.transform.position;

        if(openFlag)
        {
            if (scale.x < 0) return;

            scale.x -= 0.1f;
            if (!isX) pos.z -= 0.05f;
            else pos.x -= 0.05f;
        }
        else
        {
            if (scale.x > saveScale.x) return;

            scale.x += 0.1f;
            if (!isX) pos.z += 0.05f;
            else pos.x += 0.05f;
        }

        gameObject.transform.localScale = scale;
        gameObject.transform.position = pos;
    }

    void OnCollisionEnter(Collision collision)
    {
        openFlag = true;
        timer = maxtimer;
    }
}
