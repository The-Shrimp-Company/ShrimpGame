using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationBar : MonoBehaviour
{
    void Start()
    {
        UIManager.instance.AssignNotifBar(GetComponent<TextMeshProUGUI>());
    }
}
