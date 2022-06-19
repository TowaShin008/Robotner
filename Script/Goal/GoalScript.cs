using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    private GameObject acccessCode;
    private bool openFlag;
    private Vector3 pos;
    public float velocity;
    // Start is called before the first frame update
    void Start()
    {
        acccessCode = GameObject.Find("AccessCode");
    }

    // Update is called once per frame
    void Update()
    {
        if (!openFlag) return;

        Vector3 scale = gameObject.transform.localScale;

        if (scale.z <= 0) return;

        scale.z -= 0.1f;
        gameObject.transform.localScale = scale;

        pos = gameObject.transform.position;
        pos.x += velocity;
        gameObject.transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        openFlag = true;
    }
}
