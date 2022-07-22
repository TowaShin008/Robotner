using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerMove : MonoBehaviour
{
    enum XYZ
    {
        x,
        y,
        z
    }

    [SerializeField] int time = 1; 
    [SerializeField] float speed = 0.1f;
    [SerializeField] XYZ direction = XYZ.x;
    [SerializeField] float jumpVel = 0.1f;
    [SerializeField] int jumpTiming = 10;
    [SerializeField] int stayTime = 4;

    private int counter = 0;
    private enum MODE
    {
        DASH,
        JUMP,
        FALL,
        WAKE,
        STAY
    }
    private MODE mode = MODE.DASH;
    // Start is called before the first frame update
    void Start()
    {
        time *= 60;
        stayTime *= 60;
    }

    // Update is called once per frame
    void Update()
    {
        counter++;

        //ダッシュで開始
        //ジャンプしたあとにダッシュ
        if (counter == 0 || 
            counter == time + (jumpTiming * 3) + stayTime + stayTime)
        {
            mode = MODE.DASH;
        }

        if(counter == (time + (jumpTiming * 3) + stayTime + stayTime) + 5 * 60)
        {
            FadeManager.FadeOut("Scene_Title");
        }

        //こける。
        if (counter == time + (jumpTiming))
        {
            mode = MODE.FALL;
        }

        //2秒後に起きる
        if (counter == time + (jumpTiming) + stayTime)
        {
            mode = MODE.WAKE;
        }

        //2秒後にはねる
        if (counter == time + (jumpTiming) + stayTime + stayTime ||
            counter == time + (jumpTiming * 2) + stayTime + stayTime)//2回跳ねる
        {
            mode = MODE.JUMP;            
        }

        switch (mode)
        {
            case MODE.DASH:
                Dash();
                break;
            case MODE.JUMP:
                Jump();
                break;
            case MODE.FALL:
                Fall();
                break;
            case MODE.WAKE:
                Wake();
                break;
            default:
                break;
        }
    }

    private void Dash()
    {
        Vector3 pos = transform.position;
        switch (direction)
        {
            case XYZ.x:
                pos.x += speed;
                break;
            case XYZ.y:
                pos.y += speed;
                break;
            case XYZ.z:
                pos.z += speed;
                break;
            default:
                break;
        }
        transform.position = pos;
    }

    private void Jump()
    {
        Vector3 pos = transform.position;
        pos.y += jumpVel;
        transform.position = pos;
        mode = MODE.STAY;
    }

    private void Fall()
    {
        Quaternion rot = transform.rotation;
        switch (direction)
        {
            case XYZ.x:
                rot.x += Quaternion.Euler(90, 0, 0).x;
                break;
            case XYZ.y:
                rot.y += Quaternion.Euler(0, 90, 0).y;
                break;
            case XYZ.z:
                rot.z += Quaternion.Euler(0, 0, 90).z;
                break;
            default:
                break;
        }
        transform.rotation = rot;

        mode = MODE.STAY;
    }
    private void Wake()
    {
        Quaternion rot = transform.rotation;
        rot = Quaternion.Euler(0, 0, 0);
        transform.rotation = rot;

        mode = MODE.STAY;
    }
}
