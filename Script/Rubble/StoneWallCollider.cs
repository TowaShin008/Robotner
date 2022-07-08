using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneWallCollider : MonoBehaviour
{
    public GameObject wallCollider;
    public Transform wallTransform;
    public float xSize = 2.5f;
    public float zSize = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = wallTransform.position;
        Vector3 scale = wallTransform.localScale;
        Quaternion rot = wallTransform.localRotation;

        scale.y = 20;
        scale.z = 0.5f;
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    pos.x += xSize;
                    rot = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                    scale.x = zSize * 2;
                    break;
                case 1:
                    pos.x -= xSize * 2;
                    scale.x = zSize * 2;
                    break;
                case 2:
                    pos.x += xSize;
                    pos.z += zSize;
                    rot = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                    scale.x = xSize * 2;
                    break;
                case 3:
                    pos.z -= zSize * 2;
                    scale.x = xSize * 2;
                    break;
                default:
                    break;
            }

            GameObject collisionWall = Instantiate(wallCollider);
            collisionWall.transform.position = pos;
            collisionWall.transform.localScale = scale;
            collisionWall.transform.localRotation = rot;
            collisionWall.name = "collisionWall";
            collisionWall.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
