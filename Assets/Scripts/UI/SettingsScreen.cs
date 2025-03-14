using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : ScreenView
{
    public Slider slider;

    protected override void Start()
    {
        base.Start();
        if (!PlayerPrefs.HasKey("sensitivity"))
        {
            slider.value = 1f;
        }
        else
        {
            slider.value = PlayerPrefs.GetFloat("sensitivity");
        }
    }

    public void ValueChange()
    {
        player.GetComponent<CameraControls>().lookSenstivity = 0.5f * slider.value;
        PlayerPrefs.SetFloat("sensitivity", slider.value);
    }
}
