using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSensivity : MonoBehaviour
{
    public Slider sliderX;
    public Slider sliderY;
    public GameObject playerObject;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FPSController playerScript = playerObject.GetComponent<FPSController>();
        playerScript.Xsensityvity = sliderX.value;
        playerScript.Ysensityvity = sliderY.value;
        if (Cursor.visible == false)
        {
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
        }
    }
}
