using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //���ꂼ��̃A�v���p�̃I�u�W�F�N�g��p�ӂ���B
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject cameraPanel;
    [SerializeField] GameObject switchPanel;
    [SerializeField] GameObject mapPanel;
    [SerializeField] GameObject AccessCodePanel;

    

    // Start is called before the first frame update
    void Start()
    {
        //���j���[�ɖ߂�
        BackToMenu();
    }


    //�J�����̃A�v���̂݃A�N�e�B�u�ɂ���B
    public void SelectCameraDescription()
    {
        menuPanel.SetActive(false);
        switchPanel.SetActive(false);
        cameraPanel.SetActive(true);
        mapPanel.SetActive(false);
        AccessCodePanel.SetActive(false);
    }


    //�X�C�b�`�̃A�v���̃A�N�e�B�u�ɂ���
    public void SelectSwitchDescription()
    {
        menuPanel.SetActive(false);
        cameraPanel.SetActive(false);
        switchPanel.SetActive(true);
        mapPanel.SetActive(false);
        AccessCodePanel.SetActive(false);
    }

    //�}�b�v�̃A�v���̂݃A�N�e�B�u�ɂ���
    public void SelectMapDescription()
    {
        menuPanel.SetActive(false);
        cameraPanel.SetActive(false);
        switchPanel.SetActive(false);
        mapPanel.SetActive(true);
        AccessCodePanel.SetActive(false);
    }

    //A�A�N�Z�X�R�[�h�̂݃A�N�e�B�u�ɂ���
    public void SelectAccessCodeDescription()
    {
        menuPanel.SetActive(false);
        cameraPanel.SetActive(false);
        switchPanel.SetActive(false);
        mapPanel.SetActive(false);
        AccessCodePanel.SetActive(true);
    }


    //���j���[�̂݃A�N�e�B�u�ɂ���
    public void BackToMenu()
    {
        menuPanel.SetActive(true);
        cameraPanel.SetActive(false);
        switchPanel.SetActive(false);
        mapPanel.SetActive(false);
        AccessCodePanel.SetActive(false);
    }
}