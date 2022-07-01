using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanel : MonoBehaviour
{
    [SerializeField] GameObject pad;
    public GameObject cam;
    //private bool cameraLockFlag;
    private Quaternion camrot;
    private int count;
    private bool padFlag;

    public int activeTimer;


    // Start is called before the first frame update
    void Start()
    {
        pad.SetActive(!pad.activeSelf);
        count = 120;
        padFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeFlag();
        LockCamera();
    }

    public void ChangeFlag()
    {
        if (!padFlag)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                pad.SetActive(!pad.activeSelf);
                padFlag = !padFlag;
                count = activeTimer + 100;
            }
        }
        else
        {
            if (count < activeTimer)
            {
                count++;
                return;
            }
            else if (count == activeTimer)
            {
                count++;
                pad.SetActive(!pad.activeSelf);
                padFlag = !padFlag;

            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                count = 0;
            }
        }
    }

    public void LockCamera()
    {
        camrot = default;
        cam.transform.rotation = camrot;
    }
}
