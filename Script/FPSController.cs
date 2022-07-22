using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    float speed = 5.0f;

    public GameObject cam;
    Quaternion cameraRot, characterRot;

    //タブレットを回転する基準点
    public GameObject tabletPivot;
    Quaternion tabletPivotRot;

    bool tabletPowerFlag;

    //XY方向の視点感度
    public float Xsensityvity, Ysensityvity;

    bool deadFlag;

    //変数の宣言(角度の制限用)
    float minX;
    float maxX;

    bool squatFlag;
    //音
    public AudioClip footStep;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
        tabletPivotRot = tabletPivot.transform.localRotation;
        tabletPowerFlag = false;
        deadFlag = false;
        squatFlag = false;
        const float normalMaxX = 90f;
        maxX = normalMaxX;
        const float normalMinX = -90f;
        minX = normalMinX;
        Cursor.visible = false;
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

        //プレイヤーのしゃがむ入力処理
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {//しゃがんでいるかのフラグ切り替え
            const float squatPositionY = 0.5f;
            Vector3 squatPosition = cam.transform.position;
            if (squatFlag == false)
            {//しゃがみ時の速度を代入
                speed = 1.0f;
                squatFlag = true;
                squatPosition.y -= squatPositionY;
            }
            else
            {//立ち状態での速度を代入
                speed = 5.0f;
                squatFlag = false;
                squatPosition.y += squatPositionY;
            }

            cam.transform.position = squatPosition;
        }
        //タブレットの操作処理
        TabletProcessing();
    }

    //タブレットの操作処理
    private void TabletProcessing()
    {
        if (tabletPowerFlag)
        {
            if (TabletBootProcessing())
            {//完全に起動している場合のみシャットダウンを受け付ける
                if (Input.GetKey(KeyCode.Tab))
                {//タブレット未起動時の視点操作範囲に変更
                    const float normalMaxAngleX = 90f;
                    const float normalMinAngleX = -90f;
                    minX = normalMinAngleX;
                    maxX = normalMaxAngleX;
                    tabletPowerFlag = false;
                    Cursor.visible = false;
                }
            }
        }
        else
        {
            if (TabletShutDownProcessing())
            {//完全にシャットダウンしている場合のみ起動を受け付ける
                if (Input.GetKey(KeyCode.Tab))
                {//タブレット起動時の視点操作範囲に変更
                    const float tabletMaxAngleX = 0.0f;
                    const float tabletMinAngleX = 0.0f;
                    minX = tabletMinAngleX;
                    maxX = tabletMaxAngleX;
                    tabletPowerFlag = true;
                    Cursor.visible = true;
                }
            }
        }
    }
    //タブレットの起動処理
    private bool TabletBootProcessing()
    {
        tabletPivot.transform.localRotation.ToAngleAxis(out float angle, out Vector3 axis);

        bool endAngle = angle <= 0.0f;
        if (endAngle)
        {
            return true;
        }
        else
        {
            const float angleIncremantsize = -5.0f;
            Quaternion rot = Quaternion.AngleAxis(angleIncremantsize, Vector3.right);
            tabletPivotRot *= rot;
            tabletPivot.transform.localRotation = tabletPivotRot;
        }

        return false;
    }
    //タブレットのシャットダウン処理
    private bool TabletShutDownProcessing()
    {
        tabletPivot.transform.localRotation.ToAngleAxis(out float angle, out Vector3 axis);

        bool endAngle = angle >= 180.0f;
        if (endAngle)
        {
            return true;
        }
        else
        {
            const float angleIncrementSize = 5.0f;
            Quaternion rot = Quaternion.AngleAxis(angleIncrementSize, Vector3.right);
            tabletPivotRot *= rot;
            tabletPivot.transform.localRotation = tabletPivotRot;
        }
        return false;
    }

    private void FixedUpdate()
    {
        if (tabletPowerFlag) { return; }
        //プレイヤー移動処理
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 velocity = gameObject.transform.rotation * new Vector3(0, 0, speed);
            gameObject.transform.position += velocity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 velocity = gameObject.transform.rotation * new Vector3(-speed, 0, 0);
            gameObject.transform.position += velocity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 velocity = gameObject.transform.rotation * new Vector3(0, 0, -speed);
            gameObject.transform.position += velocity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 velocity = gameObject.transform.rotation * new Vector3(speed, 0, 0);
            gameObject.transform.position += velocity * Time.deltaTime;
        }
      
    }

    //角度制限関数の作成
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2.0f;

        angleX = Mathf.Clamp(angleX, minX, maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
            deadFlag = true;
        }
    }

    public bool GetSquatFlag()
    {
        return squatFlag;
    }

    public void SetSquatFlag(bool arg_squatFlag)
    {
        squatFlag = arg_squatFlag;
    }
}
