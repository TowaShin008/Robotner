using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageScript : MonoBehaviour
{
    public Vector3 scale;
    [SerializeField] List<GameObject> stages = new List<GameObject>();
    private bool starting = false;
    public float stageY;
    private GameObject nowStage;

    // Start is called before the first frame update
    void Start()
    {
        int num = Random.Range(0, stages.Count);
        nowStage = Object.Instantiate(stages[num]) as GameObject;

        nowStage.name = "mapStage";
        stages[num].name = "playStage";
        nowStage.transform.localScale = scale;
        Vector3 pos = nowStage.transform.position;
        pos.y = stageY;
        nowStage.transform.position = pos;

        starting = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetStartFlag()
    {
        return starting;
    }
    public void ChangeStartFlag()
    {
        starting = !starting;
    }
}
