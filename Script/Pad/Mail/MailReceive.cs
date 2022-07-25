using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailReceive : MonoBehaviour
{
    enum MAILTYPE
    {
        Annouce,
        NeedAccessCode,
        MeetRobot,
        four,
        five,
    }

    [SerializeField] GameObject mailContent;
    [SerializeField] MAILTYPE mailtype;
    [SerializeField] bool flag;
    

    GameObject templateMail;
    CreateMail createMail;
    // Start is called before the first frame update
    void Start()
    {
        templateMail = GameObject.Find("Template_Mails");
    }

    // Update is called once per frame
    void Update()
    {
        if (flag) return;

        if(gameObject.name == "MeetRobot")
        {

            GameObject robot = GameObject.Find("Robot/robot");
            transform.position = robot.transform.position;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (flag) return;

        if (mailContent == null)
        {
            Debug.Log("Can't find mailContent");
            return;
        }

        createMail = mailContent.GetComponent<CreateMail>();

        switch ((int)mailtype)
        {
            case 0:
                Announce();
                break;
            case 1:
                NeedAccessCode();
                break;
            case 2:
                MeetRobot();
                break;
        }
    }

    void Announce()
    {
        createMail.CreateNewMail("職員各位", 0);

        flag = true;
    }

    void NeedAccessCode()
    {
        Transform player = GameObject.Find("Player").transform;
        Transform pad = null;
        for (int i = 0; i < player.childCount; i++)
        {
            if(player.transform.GetChild(i).name == "RobPad")
            {
               pad = player.GetChild(i);
                break;
            }
        }

        if (pad == null)
        {
            Debug.Log("pad not found");
            return;
        }

        for (int i = 0; i < pad.childCount; i++)
        {
            if (pad.transform.GetChild(i).name == "AccessCode")
            {
                if(pad.transform.GetChild(i).GetComponent<AccessCode>().GetAccessCode() != -1)
                {
                    flag = true;
                    return;
                }
                break;
            }
        }

        createMail.CreateNewMail("出口の開き方", 1);

        flag = true;
    }

    void MeetRobot()
    {
        createMail.CreateNewMail("ロボット", 4);

        flag = true;
    }
}
