using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyContentBlock : ContentBlock
{

    private GameObject shop;

    [SerializeField]
    private GameObject shopPrefab;

    private BuyScreen _screen;

    public enum BackgroundSprites : int
    {
        Common,
        Bronze,
        Silver,
        Gold,
        Diamond
    }

    public void SetScreen(BuyScreen screen)
    {
        _screen = screen;
    }

    public void SetBackground(BackgroundSprites sprite)
    {
        GetComponent<Image>().sprite = backSprites[(int)sprite];
    }

    public void Click()
    {
        if (shop == null)
        {
            shop = Instantiate(shopPrefab, transform.parent.transform);
            shop.GetComponent<Button>().onClick.AddListener(_screen.BuyShrimp);
        }
        else
        {
            Destroy(shop);
        }
    }
}
