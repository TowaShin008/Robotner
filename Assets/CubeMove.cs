using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    void Update()
    {
        float dz = Input.GetAxis("Horizontal") * Time.deltaTime * 3;
        float dx = Input.GetAxis("Vertical") * Time.deltaTime * 3;
        transform.position = new Vector3(
        transform.position.x - dx, 0.5f, transform.position.z + dz
        );
    }
}
