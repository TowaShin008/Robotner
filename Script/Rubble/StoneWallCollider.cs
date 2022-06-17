using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneWallCollider : MonoBehaviour
{
    public GameObject wallCollider;
    public Transform wallTransform;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = wallTransform.position;
        Vector3 scale = wallTransform.localScale;
        Quaternion rot = wallTransform.localRotation;
        scale.x = 20;
        scale.y = 20;
        scale.z = 0.5f;
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    pos.x += 2.5f;
                    rot = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                    break;
                case 1:
                    pos.x -= 5.0f;
                    break;
                case 2:
                    pos.x += 2.5f;
                    pos.z += 2.5f;
                    rot = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                    break;
                case 3:
                    pos.z -= 5.0f;
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
