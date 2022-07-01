using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmitterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject transmitterPrefab;

    public void Spawn ()
    {
        Instantiate(transmitterPrefab, transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
    }
}
