using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ContentBlock : MonoBehaviour
{
    [SerializeField]
    protected Sprite[] backSprites;

    [SerializeField]
    protected TextMeshProUGUI text;

    public void SetText(string textToSet)
    {
        text.text = textToSet;
        text.fontSize = GetComponent<RectTransform>().rect.width;
    }

    public void AssignFunction(UnityAction func)
    {
        GetComponent<Button>().onClick.AddListener(func);
    }
}
