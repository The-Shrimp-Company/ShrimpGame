using SaveLoadSystem;
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

        if (!SaveManager.loadingGameFromFile)
        {
            slider.value = 1f;
        }
        else
        {
            slider.value = GameSettings.settings.cameraSensitivity;
        }
    }

    public void ValueChange()
    {
        player.GetComponent<CameraControls>().lookSenstivity = 0.5f * slider.value;
        GameSettings.settings.cameraSensitivity = slider.value;
    }
}
