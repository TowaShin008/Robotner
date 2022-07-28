using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalScript : MonoBehaviour
{
    private int accessCode;
    private bool openFlag;
    private Vector3 pos;
    public float velocity;
    public bool isX = false;

    public Text accessCodeText;

    public AudioClip clip;
    private bool sound = true;
    // Start is called before the first frame update
    void Start()
    {
        accessCode = GameObject.Find("CreateAccessCode").GetComponent<CreateAccessCode>().accessCodeNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (!openFlag) return;

        Vector3 scale = gameObject.transform.localScale;
        Vector3 pos = gameObject.transform.position;

        if (scale.x < 0|| scale.z < 0) return;

        scale.x -= 0.1f;
        if (!isX) pos.z -= 0.05f;
        else pos.x -= 0.05f;

        gameObject.transform.localScale = scale;
        gameObject.transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {

        accessCode = GameObject.Find("CreateAccessCode").GetComponent<CreateAccessCode>().accessCodeNumber;
        if (accessCode ==
            GameObject.Find("AccessCode").GetComponent<AccessCode>().GetAccessCode())
        {
            if(sound == true)
            {
                GetComponent<AudioSource>().PlayOneShot(clip);
                sound = false;
            }
            openFlag = true;
            return;
        }

        accessCodeText.text = "No AccessCode";
    }
}
