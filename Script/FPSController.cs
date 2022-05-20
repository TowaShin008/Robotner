using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    float x, z;
    float speed = 0.1f;

    Vector3 characterPos;
    float playerBasePosY;

    public GameObject cam;
    Quaternion cameraRot, characterRot;

    public GameObject tabletPivot;
    Quaternion tabletPivotRot;

    bool tabletPowerFlag;

    float Xsensityvity = 3f, Ysensityvity = 3f;

    bool deadFlag;

    //変数の宣言(角度の制限用)
    float minX = -20f, maxX = 20f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        cameraRot = cam.transform.localRotation;
        characterPos = transform.position;
        playerBasePosY = characterPos.y;
        characterRot = transform.localRotation;
        tabletPivotRot = tabletPivot.transform.localRotation;
        tabletPowerFlag = false;
        deadFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);

        //Updateの中で作成した関数を呼ぶ
        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;

        //タブレット起動時のみ視点の横移動を制限
        if (tabletPowerFlag == false)
        {
            float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
            characterRot *= Quaternion.Euler(0, xRot, 0);
            transform.localRotation = characterRot;
        }


        //タブレットの操作処理
        TabletProcessing();

        if(tabletPowerFlag == false)
        {
            //移動処理
            MoveProcessing();
        }
    }

    //タブレットの操作処理
    private void TabletProcessing()
    {
        if(tabletPowerFlag)
        {
            if(TabletBootProcessing())
            {//完全に起動している場合のみシャットダウンを受け付ける
                if (Input.GetKey(KeyCode.E))
                {
                    minX = -90f;
                    maxX = 90f;
                    tabletPowerFlag = false;
                }
            }
        }
        else
        {
            if (TabletShutDownProcessing())
            {//完全にシャットダウンしている場合のみ起動を受け付ける
                if (Input.GetKey(KeyCode.E))
                {
                    minX = -20f;
                    maxX = 20f;
                    tabletPowerFlag = true;
                }
            }
        }
    }
    //タブレットの起動処理
    private bool TabletBootProcessing()
    {
        tabletPivot.transform.localRotation.ToAngleAxis(out float angle, out Vector3 axis);
        if (angle <= 0.0f)
        {
            return true;
        }
        else
        {
            Quaternion rot = Quaternion.AngleAxis(-5, Vector3.right);
            tabletPivotRot *= rot;
            tabletPivot.transform.localRotation = tabletPivotRot;
        }

        return false;
    }
    //タブレットのシャットダウン処理
    private bool TabletShutDownProcessing()
    {
        tabletPivot.transform.localRotation.ToAngleAxis(out float angle, out Vector3 axis);
        if (angle >= 180.0f)
        {
            return true;
        }
        else
        {
            Quaternion rot = Quaternion.AngleAxis(5, Vector3.right);
            tabletPivotRot *= rot;
            tabletPivot.transform.localRotation = tabletPivotRot;
        }
        return false;
    }
    //プレイヤーの移動処理
    private void MoveProcessing()
    {
        //左に移動
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(-0.01f, 0.0f, 0.0f);
        }
        //右に移動
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(0.01f, 0.0f, 0.0f);
        }
        //上に移動
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(0.0f, 0.0f, 0.01f);
        }
        //下に移動
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0.0f, 0.0f, -0.01f);
        }
        characterPos = this.transform.position;

        //プレイヤーが浮かないように一定の高さで固定
        characterPos.y = playerBasePosY;
        this.transform.position = characterPos;
    }

    private void FixedUpdate()
    {
        if (tabletPowerFlag) { return; }
        
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;
        z = Input.GetAxisRaw("Vertical") * speed;

        //transform.position += new Vector3(x,0,z);

        transform.position += cam.transform.forward * z + cam.transform.right * x;
    }

    //角度制限関数の作成
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX, minX, maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="Enemy")
        {
            Debug.Log("Hit");
            deadFlag = true;
        }
    }
}
