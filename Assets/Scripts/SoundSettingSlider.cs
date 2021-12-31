using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingSlider : MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] Text valueText;

    [SerializeField] float startValue;


    private void Awake()
    {
        AttachSliderEvent();
        slider.value = startValue;
    }

    private void AttachSliderEvent()
    {
        slider.onValueChanged.AddListener(delegate { ChangeSoundText(); });
    }

    private void ChangeSoundText()
    {
        int sliderValue = (int)(slider.value * 100);
        valueText.text = sliderValue.ToString() + "%";
    }
}
