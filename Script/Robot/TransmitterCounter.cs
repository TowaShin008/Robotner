using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransmitterCounter : MonoBehaviour
{
    //[SerializeField] private GameObject TextObject;
    [SerializeField] private GameObject textObject;
    private Text textComponent;

    public int remaining = 3;

    public void SubRemaining()
    {
        if(remaining >= 1)
        {
            remaining--;
            if (textComponent != null) textComponent.text = "発信機:" + remaining;
            Debug.Log("発信機:" + remaining);
        }   
    }
    public void AddRemaining()
    {
        remaining++;
        textComponent.text = "発信機:" + remaining;
        Debug.Log("発信機:" + remaining);
    }

    // Start is called before the first frame update
    void Start()
    {
        textComponent = textObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
