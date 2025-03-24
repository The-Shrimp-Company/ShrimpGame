using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    private Button newGameButton;
    private Button ContinueButton;
    private Label loadingText;
    private VisualElement mainScreen;

    private bool loading = false;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        newGameButton = root.Q<Button>("MainMenuButton");
        newGameButton.clicked += NewGame;

        ContinueButton = root.Q<Button>("ContinueButton");
        ContinueButton.clicked += ContinueGame;

        loadingText = root.Q<Label>("LoadingText");

        mainScreen = root.Q<VisualElement>("MainScreen");
    }

    private void Update()
    {
        if (loading)
        {
            if(count >= 10)
            {
                loadingText.text += ".";
                count = 0;
            }
            count++;
            Debug.Log("here");
        }
    }

    private void NewGame()
    {
        LoadingScreen();
        StartGameControls.instance.newGame = true;
        SceneManager.LoadScene("ShopScene");
    }

    private void ContinueGame()
    {
        LoadingScreen();
        StartGameControls.instance.newGame = false;
        SceneManager.LoadScene("ShopScene");
    }

    private void LoadingScreen()
    {
        mainScreen.visible = false;
        loading = true;
    }
}
