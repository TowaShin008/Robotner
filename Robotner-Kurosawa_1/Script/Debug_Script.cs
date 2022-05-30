using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Script : MonoBehaviour
{
    [SerializeField]List<GameObject> gameObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
