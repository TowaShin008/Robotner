using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLight : MonoBehaviour
{

    [SerializeField] GameObject robotLight;
    public float range = 5.0f;
    public float spotAngle = 80.0f;
    public float intensity = 15.0f;
    [SerializeField] GameObject robot;
    [SerializeField] private GameObject RobotScene;

    // Start is called before the first frame update
    void Start()
    {
        robotLight.GetComponent<Light>().type = LightType.Spot;
        robotLight.GetComponent<Light>().range = range;
        robotLight.GetComponent<Light>().spotAngle = spotAngle;
        robotLight.GetComponent<Light>().intensity = intensity;
    }

    // Update is called once per frame
    void Update()
    {
        robotLight.GetComponent<Light>().type = LightType.Spot;
        robotLight.GetComponent<Light>().range = range;
        robotLight.GetComponent<Light>().spotAngle = spotAngle;
        robotLight.GetComponent<Light>().intensity = intensity;

        Vector3 pos = robot.transform.position;
        robotLight.transform.position = pos;
        Quaternion rot = robot.transform.rotation;
        robotLight.transform.rotation = rot;

        if (RobotScene.activeInHierarchy == false) { return; }

        if (Input.GetKeyDown(KeyCode.C))
        {
            robotLight.SetActive(!robotLight.activeSelf);
        }
    }
}
