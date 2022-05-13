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

    bool cursorLock = true;

    //変数の宣言(角度の制限用)
    float minX = -90f, maxX = 90f;

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
    }

    // Update is called once per frame
    void Update()
    {
        float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Updateの中で作成した関数を呼ぶ
        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;

        //カーソルの固定処理
        UpdateCursorLock();

        //タブレットの操作処理
        TabletProcessing();

        //左に移動
        if(Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(-0.01f, 0.0f, 0.0f);
        }
        //右に移動
        if(Input.GetKey(KeyCode.D))
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
        characterPos.y = playerBasePosY;
        this.transform.position = characterPos;
    }

    //タブレットの操作処理
    private void TabletProcessing()
    {
        if(tabletPowerFlag)
        {
            if(TabletBootProcessing())
            {
                if (Input.GetKey(KeyCode.E))
                {
                    tabletPowerFlag = false;
                }
            }
        }
        else
        {
            if (TabletShutDownProcessing())
            {
                if (Input.GetKey(KeyCode.E))
                {
                    tabletPowerFlag = true;
                }
            }
        }
    }

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

    private void FixedUpdate()
    {
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;
        z = Input.GetAxisRaw("Vertical") * speed;

        //transform.position += new Vector3(x,0,z);

        transform.position += cam.transform.forward * z + cam.transform.right * x;
    }


    public void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        else if (Input.GetMouseButton(0))
        {
            cursorLock = true;
        }


        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
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
        }
    }
}
