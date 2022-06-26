using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = slider.value;
    }
}
