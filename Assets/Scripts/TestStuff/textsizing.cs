using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textsizing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Canvas.ForceUpdateCanvases();
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        Rect rect = GetComponent<RectTransform>().rect;
        float fontsize = rect.height < rect.width / (text.text.Length * 0.7f) ? rect.height : rect.width / (text.text.Length * 0.7f);
        if(fontsize != rect.height)
        {
            Debug.Log("Get here?");
            if(fontsize < rect.height / 4)
            {
                Debug.Log("HEre");
                fontsize *= 2;
            }
        }
        text.fontSize = fontsize;
    }

}
