using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update

    bool gameOverFlag = false;
    public GameObject player;
    public TextMeshProUGUI accessCodeText;
    void Start()
    {
        gameOverFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<FPSController>().GetDeadFlag()) 
        {
            gameOverFlag = true;
        }
        if (gameOverFlag) 
        {
            accessCodeText.text = string.Format("Game Over");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                FadeManager.FadeOut("Scene_Load");
            }

            return;
        }
    }
}
