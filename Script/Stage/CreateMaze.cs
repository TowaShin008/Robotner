using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMaze : MonoBehaviour
{
    public GameObject cube;
    public GameObject plane;
    [TooltipAttribute("奇数のみ"), Range(5, 50)] public int x, z;

    enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }

    // Start is called before the first frame update
    void Start()
    {
        //偶数だったら奇数にする。
        if (x % 2 == 0) x++;
        if (z % 2 == 0) z++;

        //二次元配列で並べる
        int[,] mapChip = new int[x, z];
        //枠を埋める
        for (int i = 0; i < z; i++)
        {
            for (int n = 0; n < x; n++)
            {
                if (i == 0 || i == z - 1 ||
                    n == 0 || n == x - 1 ||
                    i % 2 == 0 && n % 2 == 0)//
                {
                    mapChip[n, i] = 1;

                    
                }
            }
        }

        //棒倒し
        for (int i = 0; i < z; i++)
        {
            for (int n = 0; n < x; n++)
            {
                if (i == 0 || i == z - 1 ||
                    n == 0 || n == x - 1)
                {
                    continue;
                }

                if (i % 2 == 0 && n % 2 == 0)//
                {
                    int safecounter = 0;
                    while (true)
                    {
                        if(safecounter++ > 9999)
                        {
                            Debug.Log("break");
                            break;
                        }
                        switch ((Direction)Random.Range(0, 4))
                        {
                            case Direction.Up:
                                if (n != 2) continue;
                                if (mapChip[n, i - 1] == 1) continue;
                                mapChip[n, i - 1] = 1;
                                break;
                            case Direction.Down:
                                if (mapChip[n, i + 1] == 1) continue;
                                mapChip[n, i + 1] = 1;
                                break;
                            case Direction.Left:
                                if (mapChip[n - 1, i] == 1) continue;
                                mapChip[n - 1, i] = 1;
                                break;
                            case Direction.Right:
                                if (mapChip[n + 1, i] == 1) continue;
                                mapChip[n + 1, i] = 1;
                                break;
                        }
                        break;
                    }
                }
            }
        }

        //オブジェクト配置
        int counter = 0;
        for (int i = 0; i < z; i++)
        {
            for (int n = 0; n < x; n++)
            {
                if (mapChip[n, i] == 1)//
                {
                    GameObject obj = Instantiate(cube);
                    Vector3 pos = obj.transform.position;
                    pos.x = n;
                    pos.z = i;
                    obj.transform.position = pos;
                    obj.name = "cube" + counter;
                    counter++;
                }
            }
        }


        //床配置
        GameObject floor = Instantiate(plane);
        Vector3 fpos = floor.transform.position;
        fpos.x = (float)x / 2 -0.5f;
        fpos.y -= 0.5f;
        fpos.z = (float)z / 2 - 0.5f;
        floor.transform.position = fpos;
        Vector3 scale = floor.transform.localScale;
        scale.x *= x;
        scale.z *= z;
        floor.transform.localScale = scale;
        floor.name = "floor";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
