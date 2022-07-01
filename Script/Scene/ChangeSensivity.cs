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
    }
}
