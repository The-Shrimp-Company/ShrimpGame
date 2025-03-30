using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSwitching : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject panel;
    [SerializeField] private VetWindows[] windows;
    [SerializeField] private GameObject BackButton;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<HorizontalLayoutGroup>().enabled = true;
        foreach(Button button in buttons)
        {
            button.transform.SetAsLastSibling();
        }

        for(int i = 0; i < windows.Length; i++)
        {
            VetWindows window = windows[i];
            window.transform.SetAsLastSibling();
            if (!window.CheckReputation())
            {
                window.gameObject.SetActive(false);
                buttons[i].gameObject.SetActive(false);
            }
            else
            {
                window.gameObject.SetActive(true);
                buttons[i].gameObject.SetActive(true);
            }

            BackButton.transform.SetAsLastSibling();
        }

        panel.transform.SetSiblingIndex(buttons.Length - 1);

        for(int i = 0; i < buttons.Length; i++)
        {
            int x = i;
            buttons[i].onClick.AddListener(() => {
                GetComponent<HorizontalLayoutGroup>().enabled = false;
                Debug.Log("Clicked: " + x);
                panel.transform.SetAsLastSibling();
                buttons[x].transform.SetAsLastSibling();
                windows[x].transform.SetAsLastSibling();
                BackButton.transform.SetAsLastSibling();
            });
        }
    }

    
}
