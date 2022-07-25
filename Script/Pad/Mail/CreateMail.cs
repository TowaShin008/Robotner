using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateMail : MonoBehaviour
{
    List<RectTransform> mails = new List<RectTransform>();
    [SerializeField] GameObject templateMails;

    int mailCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        templateMails.SetActive(false);

        for(int i = 0; i <  transform.childCount; i++)
        {
            mails.Add(transform.GetChild(i).gameObject.GetComponent<RectTransform>());
            Vector2 pos = new Vector2(-6.600006f, 117.3f - (30 * mailCount));
            mails[mails.Count - 1].anchoredPosition = pos;
            mailCount++;
        }
        
        
    }


    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.N))
        {
            CreateNewMail("test", 0);
        }
#endif


        if (mailCount == transform.childCount) return;

        mails.Add(transform.GetChild(transform.childCount - 1).gameObject.GetComponent<RectTransform>());
        Vector2 pos = new Vector2(-6.600006f, 104.0f - (30 * mailCount));
        //mails[mails.Count - 1].anchoredPosition = pos;

        mailCount++;
    }

    public void CreateNewMail(string title,int type)
    {
        GameObject chroneMail = Instantiate(templateMails.transform.GetChild(type).gameObject,gameObject.transform);

        chroneMail.name = "Mail" + mails.Count;

        chroneMail.transform.GetChild(1).GetComponent<Text>().text = title;

        chroneMail.SetActive(true);

    }
}
