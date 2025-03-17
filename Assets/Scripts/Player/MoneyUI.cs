using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        string money = Money.instance.money.ToString();
        //if (money.Contains("."))
        //{
        //    money = money.Substring(0, money.IndexOf(".") + 3);
        //}
        text.text = ("£" + money);
    }
}
