using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] GameObject stage_T;
    [SerializeField] GameObject stage_I;
    [SerializeField] GameObject stage_L;
    [SerializeField] GameObject stage_O;
    public int sizeX;
    public int sizeY;
    public Vector3 scale;

    private int counter = 0;

    enum BLOCK
    {
        T,
        I,
        L,
        O,
    }

    private List<GameObject> stageBlocks = new List<GameObject>();

    RotateBox rotatescript;

    // Start is called before the first frame update
    void Start()
    {
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                switch ((BLOCK)Random.Range(1, 4))
                {
                    case BLOCK.T:
                        stageBlocks.Add(Object.Instantiate(stage_T) as GameObject);
                        break;
                    case BLOCK.I:
                        stageBlocks.Add(Object.Instantiate(stage_I) as GameObject);
                        break;
                    case BLOCK.L:
                        stageBlocks.Add(Object.Instantiate(stage_L) as GameObject);
                        break;
                    default:
                        stageBlocks.Add(Object.Instantiate(stage_T) as GameObject);
                        break;
                }

                int rotNum = Random.Range(1, 5) * 90;
                Vector3 pos = Vector3.zero;
                pos.x = x * (scale.x - 1f);
                pos.y = scale.y / 2;
                pos.z = y * (scale.z - 1f);
                stageBlocks[stageBlocks.Count - 1].transform.position = pos;

                Quaternion rot = default;
                rot.y = rotNum;
                stageBlocks[stageBlocks.Count - 1].transform.Rotate(new Vector3(0, rotNum, 0));

                stageBlocks[stageBlocks.Count - 1].transform.localScale = scale;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (counter < 5)
        {
            for(int i = 0; i < stageBlocks.Count; i++)
            {
                rotatescript = stageBlocks[i].GetComponent<RotateBox>();
                if(rotatescript.counter > 5)
                {
                    Vector3 pos = stageBlocks[i].transform.position;
                    stageBlocks[i] = Object.Instantiate(stage_O) as GameObject;

                    stageBlocks[i].transform.position = pos;
                    stageBlocks[i].transform.localScale = scale;
                }
            }
        }
        counter++;
    }
}
