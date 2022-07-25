using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailScript : MonoBehaviour
{
    public List<GameObject> mailScene = new List<GameObject>();
    public bool[] readCheck;
    public GameObject newMailIcon;
    public GameObject newMailIcon2;
    private Vector3 savePos;
    private Vector3 vel;
    [SerializeField] GameObject returnButton;

    private int counter = 0;
    private bool doJumpFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < mailScene.Count; i++)
        {
            mailScene[i].SetActive(false);
            if (i == 0)
            {
                mailScene[i].SetActive(true);
                
            }
        }
        readCheck = new bool[mailScene.Count];

        savePos = newMailIcon.transform.position;

        returnButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        newMailIcon.SetActive(false);
        newMailIcon2.SetActive(false);

        for (int i = 0; i < mailScene.Count; i++)
        {
            if (readCheck[i] == false)
            {
                newMailIcon.SetActive(true);
                newMailIcon2.SetActive(true);
            }
        }
        if(!doJumpFlag)
        {
            doJumpFlag = true;
            vel.y = 1.0f;
        }
        else
        {
            if (Jump()) doJumpFlag = false;
        }

    }

    public void ChangeScene(string sceneName)
    {
        for (int i = 0; i < mailScene.Count; i++) {
            mailScene[i].SetActive(false);
            
            if (sceneName == mailScene[i].name)
            {
                mailScene[i].SetActive(true);
                readCheck[i] = true;
            }
            else if (mailScene[i].name == "Mails")
            {
                for(int c = 0; c < mailScene[i].transform.childCount; c++)
                {
                    if(sceneName == mailScene[i].transform.GetChild(c).name)
                    {
                        mailScene[i].SetActive(true);
                        mailScene[i].transform.GetChild(c).gameObject.SetActive(true);

                        for(int p = 0; p < mailScene[i].transform.childCount; p++)
                        {
                            if(p != c)
                            {
                                mailScene[i].transform.GetChild(p).gameObject.SetActive(false);
                            }
                        }

                        readCheck[i] = true;
                        break;
                    }
                }
            }
        }

        if(sceneName == "Menu")
        {
            returnButton.SetActive(false);
        }
        else
        {
            returnButton.SetActive(true);
        }
    }

    private bool Jump()
    {
        Vector3 pos = newMailIcon.transform.position;
        pos.y += vel.y;
        vel.y -= 0.02f;
        newMailIcon.transform.position = pos;

        if (pos.y < savePos.y)
        {
            return true;
        }
        else return false;
    }

    
}
