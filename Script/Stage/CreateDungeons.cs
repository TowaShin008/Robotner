using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDungeons : MonoBehaviour
{
    public GameObject cube;
    public GameObject plane;
    [TooltipAttribute("��̂�"), Range(5, 50)] public int x, z;
    [TooltipAttribute("�����̍ő�̑傫��(���͋����݂̂���)"), Range(2, 10)] public int roomMaxScale;
    [TooltipAttribute("�����_���ȕ����̐�(���̐ݒ肪�����ƂȂ�܂�)")] public bool randomRoomN;
    [TooltipAttribute("�����̐�(��:�S�̂̑傫�����镔���̑傫��)"), Range(0, 16)] public int roomN;

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
        //�������������ɂ���B
        if (x % 2 == 0) x++;
        if (z % 2 == 0) z++;

        //���ׂ�p�̓񎟌��z����쐬
        int[,] mapChip = new int[x, z];
        //���S�̈ʒu��ۑ����Ă���
        List<Vector2Int> center = new List<Vector2Int>();
        //�g�𖄂߂�
        //1�u���Ƀu���b�N�𖄂߂Ă���(�f�t�H���g)
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
        //���������Ƃ���
        List<int> checkRoom = new List<int>();

        //�����|�C���g����
        for (int i = 0; i< roomN; i++)
        {
            int loopCheck = 0;//�������[�v���p
            while (true)
            {
                loopCheck++;
                if (loopCheck > 9999) break;//�������[�v���Ă����Ȃ甲����

                bool checkDouble = false;//�d���`�F�b�N
                int num = Random.Range(0, center.Count);// ���������ꏊ
                for (int c = 0; c < checkRoom.Count; c++)//�������łɓo�^����Ă����������x
                {
                    if (checkRoom[c] == num)
                    {
                        checkDouble = true;
                        continue;
                    }
                }
                if (checkDouble) continue;
                checkRoom.Add(num);//�쐬�ς݂̕����̃��X�g�ɒǉ�
                break;
            }
        }



        

        //�����쐬
        for(int i = 0; i < checkRoom.Count; i++)
        {
            //�����̑傫���̔��a
            int rad = Random.Range(roomMaxScale / 2, roomMaxScale) / 2;//�����̑傫���͊�{�����_�������A�ŏ��ł��ݒ肵���傫���̔���
            for (int posY = center[checkRoom[i]].y - rad; posY < center[checkRoom[i]].y + rad; posY++)
            {
                for (int posX = center[checkRoom[i]].x - rad; posX < center[checkRoom[i]].x + rad; posX++)
                {
                    mapChip[posX, posY] = 1;//�����̒��S���畔���̑傫���̔��a�����߂�
                }
            }
        }

        //���Ƃ����_�|��
        for (int i = 0; i < center.Count; i++)//center�̐������J��Ԃ�
        {
            int safecounter = 0;
            while (true)
            {
                if (safecounter++ > 9999)//�������[�v���p
                {
                    Debug.Log("break");
                    break;
                }
                switch ((Direction)Random.Range(0, 4))
                {
                    case Direction.Up:
                        if (center[i].y != 2) continue;//��ɓ|����͈̂�ԏ�̗�̂�
                        if (mapChip[center[i].x, center[i].y - roomMaxScale / 2 - 1] == 1) continue;//���ɖ��߂��Ă������蒼��
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

        //�I�u�W�F�N�g�z�u
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


        //���z�u
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
