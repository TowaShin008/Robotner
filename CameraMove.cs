using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    //カメラのトランスフォーム取得
    private Transform cameraTransform;
    //最大振り向き角度
    private float MaxRotate = 90;
    //振り向きスピード
    //[SerializeField]
    private float RotSpeed = 30;
    //
    private float RotY = 0f;
    void Start()
    {
        cameraTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //現在の角度から＋ー90°を指定
        //それ以上に行かないようにする
        //→
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            RotY = RotSpeed;
        }
        //←
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            RotY = -RotSpeed;
        }
        transform.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y + RotY * Time.deltaTime,
            0.0f);

        RotY = 0;
    }
}
