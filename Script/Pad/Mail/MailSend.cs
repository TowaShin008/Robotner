using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailSend : MonoBehaviour
{
    enum STATE
    {
        ANNOUNCE,
        NEEDACCESSCODE,
        COMPLAINTS,
        
    }

    private STATE nowstate;
    private int counter = 0;
    private int timer = 0;
    private GameObject sendComp = null;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "Mail_SendCompleated")
            {
                sendComp = transform.GetChild(i).gameObject;
                break;
            }
        }
        sendComp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer++ > 700)
        {
            timer = 0;
            counter = 0;
        }

        if(counter > 10)
        {
            STATE save = nowstate;
            nowstate = STATE.COMPLAINTS;
            Receive();
            nowstate = save;

            timer = 0;
            counter = 0;
        }
    }

    public void Send()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "Mail_SendCompleated")
            {
                transform.GetChild(i).gameObject.SetActive(true);
                break;
            }
        }

        Receive();
    }

    //���[�����󂯎��
    void Receive()
    {
        //���ʋC��Content������(�啪�߂��Ⴍ����ӂ����܂����B)


        CreateMail createMail = null;

        for (int q = 0; q < transform.parent.childCount; q++)
        {
            if (transform.parent.GetChild(q).name == "Menu")
            {
                for(int w = 0; w < transform.parent.GetChild(q).childCount; w++)
                {
                    if (transform.parent.GetChild(q).GetChild(w).name == "Scroll View")
                    {
                        for(int e = 0; e < transform.parent.GetChild(q).GetChild(w).childCount; e++)
                        {
                            if (transform.parent.GetChild(q).GetChild(w).GetChild(e).name == "Viewport")
                            {
                                createMail = transform.parent.GetChild(q).GetChild(w).GetChild(e).GetChild(0).GetComponent<CreateMail>();
                            }
                        }
                    }
                }
            }
        }


        switch (nowstate)
        {
            case STATE.ANNOUNCE:
                createMail.CreateNewMail("��������", 0);//�ύX�\��
                break;
            case STATE.NEEDACCESSCODE:
                createMail.CreateNewMail("����������", 1);//�ύX�\��
                break;
            case STATE.COMPLAINTS:
                createMail.CreateNewMail("���邳���I", 3);//�ύX�\��
                break;
            default:
                break;
        }
        counter++;
        timer = 0;
    }

    public void SwitchCompleatMonitor()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "Mail_SendCompleated")
            {
                transform.GetChild(i).gameObject.SetActive(false);
                break;
            }
        }
    }

    void ChangeState(int stateNum)
    {
        nowstate = (STATE)stateNum;
    }
}
