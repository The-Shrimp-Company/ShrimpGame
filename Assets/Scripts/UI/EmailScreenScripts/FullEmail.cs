using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullEmail : MonoBehaviour
{
    [SerializeField] private GameObject _button;
    private List<Button> buttons = new List<Button>();
    [SerializeField] private Transform buttonParent;

    private Email _email;

    public void SetEmail(Email email)
    {
        _email = email;
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = email.mainText;
        text.fontSize = 30;
        if(email.buttons != null)
        {
            Debug.Log("Buttons");
            foreach(MyButton button in email.buttons)
            {
                Debug.Log("One button");
                GameObject obj = Instantiate(_button, buttonParent);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = button.text;
                FontTools.SizeFont(obj.GetComponentInChildren<TextMeshProUGUI>());
                obj.GetComponent<Button>().onClick.AddListener(button.action);
                if(button.destroy == true)
                {
                    obj.GetComponent<Button>().onClick.AddListener(DeleteEmail);
                }
                buttons.Add(obj.GetComponent<Button>());
            }
        }
        if (!email.important)
        {
            GameObject obj = Instantiate(_button, buttonParent);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = "Delete";
            FontTools.SizeFont(obj.GetComponentInChildren<TextMeshProUGUI>());
            obj.GetComponent<Button>().onClick.AddListener(DeleteEmail);
        }
    }

    public void DeleteEmail()
    {
        EmailManager.instance.emails.Remove( _email );
        Destroy(transform.parent.gameObject);
    }
}
