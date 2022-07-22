using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowAccessCode : MonoBehaviour
{
    //public Text accessCodeText;
    public TextMeshProUGUI accessCodeText;
    private int code = -1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //コード表示
        ShowCode();
    }

    void ShowCode()
    {
        AccessCode accessCode;
        GameObject obj = GameObject.Find("AccessCode");
        accessCode = obj.GetComponent<AccessCode>();
        code = accessCode.GetAccessCode();

        //コードの有無を確認して表示
        if (code == -1) NoCode();
        else CodeIs();  
    }

    void NoCode()
    {
        //コードがない場合はコードがないと表示
        accessCodeText.text = string.Format("NoAccessCode");
    }

    void CodeIs()
    {
        //コードがあればコードを表示
        accessCodeText.text = string.Format("AccessCode : {0}", code);
    }
}
