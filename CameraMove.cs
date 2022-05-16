using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    //最大振り向き角度
   // private float MaxRotate = 90;
    //振り向きスピード
    //[SerializeField]
    private float RotSpeed = 30;
    //
    private float RotY = 0f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //現在の角度から＋ー90°を指定
        //それ以上に行かないようにする
        //→
        if (Input.GetMouseButton(0)) 
        {
            RotY = -RotSpeed;
        }
        //←
        if (Input.GetMouseButton(1)) 
        {
            RotY = RotSpeed;
        }
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + RotY * Time.deltaTime,
           transform.eulerAngles.z);

        RotY = 0;
    }
}
