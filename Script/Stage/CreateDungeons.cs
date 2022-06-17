using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDungeons : MonoBehaviour
{
    public GameObject cube;
    public GameObject plane;
    [TooltipAttribute("奇数のみ"), Range(5, 50)] public int x, z;
    [TooltipAttribute("部屋の最大の大きさ(今は偶数のみかも)"), Range(2, 10)] public int roomMaxScale;
    [TooltipAttribute("ランダムな部屋の数(下の設定が無効となります)")] public bool randomRoomN;
    [TooltipAttribute("部屋の数(注:全体の大きさ割る部屋の大きさ)"), Range(0, 16)] public int roomN;

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

        //並べる用の二次元配列を作成
        int[,] mapChip = new int[x, z];
        //中心の位置を保存しておく
        List<Vector2Int> center = new List<Vector2Int>();
        //枠を埋める
        //1個置きにブロックを埋めていく(デフォルト)
        for (int i = 0; i < z; i++)
        {
            for (int n = 0; n < x; n++)
            {
                if (i == 0 || i == z - 1 ||
                    n == 0 || n == x - 1 ||
                    i % roomMaxScale == 0 && n % roomMaxScale == 0)
                {
                    mapChip[n, i] = 1;
                    if (i == 0 || i == z - 1 ||
                    n == 0 || n == x - 1) continue;
                    if(i % roomMaxScale == 0 && n % roomMaxScale == 0)
                    {
                        center.Add(new Vector2Int(n, i));
                    }
                }
            }
        }

        if (randomRoomN) roomN = Random.Range(1, 5);
        //部屋を作るところ
        List<int> checkRoom = new List<int>();

        //部屋ポイント決定
        for (int i = 0; i< roomN; i++)
        {
            int loopCheck = 0;//無限ループ回避用
            while (true)
            {
                loopCheck++;
                if (loopCheck > 9999) break;//無限ループしてそうなら抜ける

                bool checkDouble = false;//重複チェック
                int num = Random.Range(0, center.Count);// 部屋を作る場所
                for (int c = 0; c < checkRoom.Count; c++)//もうすでに登録されていたらもう一度
                {
                    if (checkRoom[c] == num)
                    {
                        checkDouble = true;
                        continue;
                    }
                }
                if (checkDouble) continue;
                checkRoom.Add(num);//作成済みの部屋のリストに追加
                break;
            }
        }



        

        //部屋作成
        for(int i = 0; i < checkRoom.Count; i++)
        {
            //部屋の大きさの半径
            int rad = Random.Range(roomMaxScale / 2, roomMaxScale) / 2;//部屋の大きさは基本ランダムだが、最小でも設定した大きさの半分
            for (int posY = center[checkRoom[i]].y - rad; posY < center[checkRoom[i]].y + rad; posY++)
            {
                for (int posX = center[checkRoom[i]].x - rad; posX < center[checkRoom[i]].x + rad; posX++)
                {
                    mapChip[posX, posY] = 1;//部屋の中心から部屋の大きさの半径分埋める
                }
            }
        }

        //おとくい棒倒し
        for (int i = 0; i < center.Count; i++)//centerの数だけ繰り返す
        {
            int safecounter = 0;
            while (true)
            {
                if (safecounter++ > 9999)//無限ループ回避用
                {
                    Debug.Log("break");
                    break;
                }
                switch ((Direction)Random.Range(0, 4))
                {
                    case Direction.Up:
                        if (center[i].y != 2) continue;//上に倒せるのは一番上の列のみ
                        if (mapChip[center[i].x, center[i].y - roomMaxScale / 2 - 1] == 1) continue;//既に埋められていたらやり直し
                        for (int b = 0; b < roomMaxScale; b++)
                        {
                            mapChip[center[i].x, center[i].y - b] = 1;
                        }
                        break;
                    case Direction.Down:
                        if (mapChip[center[i].x, center[i].y + roomMaxScale / 2 - 1] == 1) continue;
                        for (int b = 0; b < roomMaxScale; b++)
                        {
                            mapChip[center[i].x, center[i].y + b] = 1;
                        }
                        break;
                    case Direction.Left:
                        if (mapChip[center[i].x - roomMaxScale / 2 - 1, center[i].y] == 1) continue;
                        for (int b = 0; b < roomMaxScale; b++)
                        {
                            mapChip[center[i].x - b, center[i].y] = 1;
                        }
                        break;
                    case Direction.Right:
                        if (mapChip[center[i].x + roomMaxScale / 2 - 1, center[i].y] == 1) continue;
                        for (int b = 0; b < roomMaxScale; b++)
                        {
                            mapChip[center[i].x + b, center[i].y] = 1;
                        }
                        break;
                }
                break;
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
        fpos.x = (float)x / 2 - 0.5f;
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
