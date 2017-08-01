using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour {
    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();  
    }
    // Update is called once per frame
    void Update () {
        AudioListener.volume = slider.value;
    }
}
