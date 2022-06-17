using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public List<GameObject> gameObjects;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        //���j���[�ɖ߂�
        ChangeScreen(gameObjects[0].name);

    }

    private void Update()
    {
    }

    //�J�����̃A�v���̂݃A�N�e�B�u�ɂ���B
    public void ChangeScreen(string screenName)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[i].name == screenName)
            {
                gameObjects[i].SetActive(true);
            }
            else
            {
                gameObjects[i].SetActive(false);
            }
        }
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}