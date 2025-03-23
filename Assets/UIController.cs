using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    private Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        startButton = root.Q<Button>("MainMenuButton");
        startButton.clicked += StartGame;
    }

    private void StartGame()
    {
        SceneManager.LoadScene("ShopScene");
    }
}
