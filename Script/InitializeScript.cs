using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeScript : MonoBehaviour
{
    public GameObject gameObject1;
    public GameObject gameObject2;
    private int processTime = 0;
    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        FadeManager.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameObject1.SetActive(false);
            gameObject2.SetActive(false);
        }
    }
}
