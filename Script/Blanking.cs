using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ライト点滅スクリプト
public class Blanking : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]AnimationCurve curve;
    [SerializeField] float speed;
    [SerializeField] float intensity;

    Light light;
    float t = 0f;


    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        light.intensity = intensity * curve.Evaluate(t * speed);
    }
}
