using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Device;

public class EmailContentBlock : ContentBlock
{
    [SerializeField] private TextMeshProUGUI title, subjectLine;
    [SerializeField] private GameObject fullEmail;

    private GameObject _fullEmail;
    
    private Email _email;


    private void Update()
    {
        if (!EmailManager.instance.emails.Contains(_email))
        {
            Debug.Log("aaaa");
            Destroy(transform.parent.gameObject);
        }
    }

    public void SetEmail(Email email)
    {
        _email = email;
        title.text = _email.title;
        subjectLine.text = _email.subjectLine;
        FontTools.SizeFont(title);
        FontTools.SizeFont(subjectLine);
        subjectLine.fontSize *= 0.8f;
    }

    public void Click()
    {
        if (_fullEmail == null)
        {
            _fullEmail = Instantiate(fullEmail, transform.parent.transform);
            _fullEmail.GetComponent<FullEmail>().SetEmail(_email);
        }
        else
        {
            Destroy(_fullEmail);
        }
    }

    public bool isImportant()
    {
        return _email.important;
    }

    public void DeleteEmail()
    {
        EmailManager.instance.emails.Remove(_email);
        Destroy(transform.parent.gameObject);
    }
}
