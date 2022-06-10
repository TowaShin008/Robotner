using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] GameObject selectStage;
    [SerializeField] List<GameObject> gameObjects = new List<GameObject>();
    private List<GameObject> spawnPoints = new List<GameObject>();
    private SelectStageScript selectStageScript;
    private GameObject spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        selectStageScript = selectStage.GetComponent<SelectStageScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!selectStageScript.GetStartFlag()) return;
        
        spawnPoint = GameObject.Find("playStage/SpawnPoint").gameObject;

        for (int i = 0; i < spawnPoint.transform.childCount; i++)
        {
            spawnPoints.Add(spawnPoint.transform.GetChild(i).gameObject);

            gameObjects[i].transform.position = spawnPoints[i].transform.position;
            gameObjects[i].transform.rotation = spawnPoints[i].transform.rotation;
        }

        selectStageScript.ChangeStartFlag();
    }
}
