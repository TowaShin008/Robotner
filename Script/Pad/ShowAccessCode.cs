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
        CreateAccessCode accessCode;
        GameObject obj = GameObject.Find("AccessCode");
        accessCode = obj.GetComponent<CreateAccessCode>();
        code = accessCode.accessCodeNumber;
    }

    // Update is called once per frame
    void Update()
    {
        //�R�[�h�\��
        ShowCode();
    }

    void ShowCode()
    {
        //�R�[�h�̗L�����m�F���ĕ\��
        if (code == -1) NoCode();
        else CodeIs();  
    }

    void NoCode()
    {
        //�R�[�h���Ȃ��ꍇ�̓R�[�h���Ȃ��ƕ\��
        accessCodeText.text = string.Format("NoAccessCode");
    }

    void CodeIs()
    {
        //�R�[�h������΃R�[�h��\��
        accessCodeText.text = string.Format("AccessCode : {0}", code);
    }
}
