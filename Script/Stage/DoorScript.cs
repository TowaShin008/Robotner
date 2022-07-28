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

    public AudioClip clip;
    private bool sound = false;

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
            sound = true;
        }

        Vector3 scale = gameObject.transform.localScale;
        Vector3 pos = gameObject.transform.position;

        if (openFlag)
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

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Robot")
        {
            if (sound == true)
            {
                GetComponent<AudioSource>().PlayOneShot(clip);
                sound = false;
            }
        }
    }
}
