using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = AudioManager.volumeSlider;
    }

    void Update()
    {
        AudioManager.volumeSlider = slider.value;
    }
}
