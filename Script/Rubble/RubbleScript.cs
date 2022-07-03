using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleScript : MonoBehaviour
{

    public GameObject baseObject;
    public Transform spawnPoint;
    public int size;
    public int sizeRange;
    private int num = 0;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        return;

        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(baseObject);
            obj.transform.position = spawnPoint.position;
            float randomX = (float)Random.Range(10, sizeRange) /100;
            float randomY = (float)Random.Range(10, sizeRange) /100;
            float randomZ = (float)Random.Range(10, sizeRange) /100;
            Vector3 scale = new Vector3(randomX, randomY, randomZ);
            obj.transform.localScale = scale;
            obj.name = "CobbleStone";
            obj.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
