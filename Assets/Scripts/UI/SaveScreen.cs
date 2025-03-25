using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScreen : ScreenView
{
    [SerializeField] private List<Button> saveFileButtons;


    protected override void Start()
    {
        base.Start();
        foreach(Button button in saveFileButtons)
        {
            button.onClick.AddListener(() => {
                SaveManager.SaveGame(button.name);
                Debug.Log(button.name);
            });
        }
    }

}
