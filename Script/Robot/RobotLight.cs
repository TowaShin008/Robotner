using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLight : MonoBehaviour
{

    [SerializeField] GameObject robotLight;
    private Light robLight;
    public float range = 5.0f;
    public float spotAngle = 80.0f;
    public float intensity = 15.0f;
    [SerializeField] GameObject robot;
    // Start is called before the first frame update
    void Start()
    {
        robLight = robotLight.AddComponent<Light>();
        robLight.type = LightType.Spot;
        robLight.range = range;
        robLight.spotAngle = spotAngle;       
        robLight.intensity = intensity;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = robot.transform.position;
        robotLight.transform.position = pos;
        Quaternion rot = robot.transform.rotation;
        robotLight.transform.rotation = rot;

        if (Input.GetKeyDown(KeyCode.C))
        {
            robotLight.SetActive(!robotLight.activeSelf);
        }
    }
}
