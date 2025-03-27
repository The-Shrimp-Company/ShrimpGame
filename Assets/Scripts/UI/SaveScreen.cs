using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveScreen : ScreenView
{
    [SerializeField] private List<Button> saveFileButtons;
    [SerializeField] private SaveController saveController;


    protected override void Start()
    {
        base.Start();
        saveController = GameObject.Find("Save Controller").GetComponent<SaveController>();
        foreach(Button button in saveFileButtons)
        {
            button.onClick.AddListener(() => {
                saveController.SaveGame(button.name);
                Debug.Log(button.name);
            });
        }
    }

    public void QuitToMain()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        saveController.SaveGame("Autosave");
        SceneManager.LoadScene("MainMenuScene");
    }
}
