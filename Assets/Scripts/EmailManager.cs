using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct MyButton
{
    public UnityAction action;
    public string text;
}


public struct Email
{
    public int ID;

    public string title;
    public string subjectLine;
    public string mainText;

    public bool important;

    public List<MyButton> buttons;
}

public class EmailManager
{

    static public EmailManager instance = new EmailManager();

    public List<Email> emails { get; private set; } = new List<Email>();

    static public void SendEmail(Email email, bool important = false)
    {
        email.important = important;
        instance.emails.Add(email);
        UIManager.instance.SendNotification(email.title);
    }
}

public static class EmailTools
{
    static public void CreateEmailButton(ref this Email email, string text, UnityAction action)
    {
        if(email.buttons == null)
        {
            email.buttons = new List<MyButton>();
        }
        MyButton button = new MyButton();
        button.text = text;
        button.action = action;
        email.buttons.Add(button);
        Debug.Log("Added button");
    }
}
