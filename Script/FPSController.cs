using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    float x, z;
    float speed = 5.0f;

    public GameObject cam;
    Quaternion cameraRot, characterRot;

    //タブレットを回転する基準点
    public GameObject tabletPivot;
    Quaternion tabletPivotRot;

    bool tabletPowerFlag;

    //XY方向の視点感度
    public float Xsensityvity, Ysensityvity;
    //死亡フラグ
    bool deadFlag;
    //ゲーム開始フラグ
    bool gameStartFrag;
    float wakeUpRotation=90;
    public float wakeUpSpeed;
    int wakeUpCase = 0;
    public float trunSpeed;
    Vector3 savePos;
    Vector3 axis;
    float angle;
    int count;
    public GameObject panelScript;

    //変数の宣言(角度の制限用)
    float minX;
    float maxX;

    bool squatFlag;
    //音
    public AudioClip clip;
    public AudioClip walkClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource walkAudioSource;
    bool walk = false;
    int warktime = 0;

    Vector3 savecam;
    float deathrotation = 0;
    [SerializeField] int rotspeed;
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
        const float normalMinX = -90f;
        minX = normalMinX;
        maxX = normalMaxX;
        gameStartFrag = false;

        audioSource.clip = clip;

        walkAudioSource.clip = walkClip;
        walkAudioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameStartFrag)
        {
            if (!deadFlag)
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
                    {
                        squatFlag = true;
                        squatPosition.y -= squatPositionY;
                    }
                    else
                    {
                        squatFlag = false;
                        squatPosition.y += squatPositionY;
                    }

                    cam.transform.position = squatPosition;
                }
                //タブレットの操作処理
                TabletProcessing();
                savecam = cam.transform.position;

            }
            else
            {
                Quaternion rot = transform.localRotation;
                rot = Quaternion.Euler(deathrotation, 0, 0);
                transform.localRotation = rot;
                if (deathrotation < 90)
                {
                    deathrotation += rotspeed;
                }
                else
                {
                    Vector3 campos = cam.transform.position;
                    campos.y = savecam.y + 5;
                    cam.transform.position = campos;
                    deathrotation = 90;
                }
            }
        }
        else
        {
            //ここでゲーム開始の導入追加
            //横たわらせる 
            Quaternion rot = transform.localRotation;
            Vector3 pos = gameObject.transform.position;

            switch (wakeUpCase)
            {
                case 0:
                    float sin = Mathf.Sin(Time.time);

                    //視界ぱちぱち
                    //左右振り向き
                   

                   
                    axis = new Vector3(0, 0, 1);

                    angle = sin * trunSpeed;
                    Quaternion q1 = Quaternion.AngleAxis(180 - angle, axis);
                    axis = new Vector3(1, 0, 0);
                    Quaternion q2 = Quaternion.AngleAxis(-90, axis);
                    rot = q1 * q2;
                    savePos = gameObject.transform.position;

                    if (0<sin&&sin<0.05)
                    {
                        count++;
                    }
                    if (count > 3)
                    {
                        wakeUpCase = 1;
                    }

                    break;
                case 1:
                    //垂直起き上がり膝まで
                    if (savePos.y+3.0>pos.y)
                    {
                        pos.y += 0.1f;
                    }
                    else
                    {
                        wakeUpCase = 2;
                    }
                    gameObject.transform.position = pos;

                    break;
                case 2:
                    //起き上がり頭まで、頭(カメラ)回転
                    if (wakeUpRotation >= 0)
                    {
                        wakeUpRotation -= wakeUpSpeed;
                    }
                    else
                    {
                        wakeUpRotation = 0;
                        gameStartFrag = true;
                        tabletPowerFlag = true;
                        panelScript.GetComponent<ShowPanel>().ShowPad();
                    }
                    rot = Quaternion.Euler(wakeUpRotation, 180, 0);
                    break;
            }
            //rot = Quaternion.Euler(wakeUpRotation, trunangle, 0);
            transform.localRotation = rot;

        }
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
                    audioSource.PlayOneShot(clip);
                    walk = false;
                    walkAudioSource.mute = true;
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

        bool endAngle = angle >= 180.0f;
        if (endAngle)
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
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            walk = true;
        }
        else
        {
            walk = false;
        }

        if (walk == true)
        {
            walkAudioSource.mute = false;
        }
        else
        {
            walkAudioSource.mute = true;
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
        for (int i = 1; i < 5; i++)
        {
            if (collision.gameObject.name == "enemy"+i)
            {
                Debug.Log("Hit");
                deadFlag = true;
            }
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
    public bool GetDeadFlag()
    {
        return deadFlag;
    }
}
