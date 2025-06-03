using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CrossHairSwitch : MonoBehaviour
{
    [SerializeField] private Image crosshair;
    [SerializeField] private TextMeshProUGUI text;
    private RectTransform crosshairRect;

    [SerializeField] private Sprite crosshairSprite;
    [SerializeField] private float crosshairSize = 1f;

    [SerializeField] private Sprite interactSprite;
    [SerializeField] private float interactSize = 1f;

    [SerializeField] private Ease crosshairEase;

    private void Start()
    {
        crosshairRect = crosshair.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(text.text == "" && text.enabled)
        {
            if (crosshair.sprite != crosshairSprite)
            {
                ChangeSprite(crosshairSprite, crosshairSize);
                text.DOFade(0, 0.1f);
            }
        }
        else
        {
            if (crosshair.sprite != interactSprite)
            {
                ChangeSprite(interactSprite, interactSize);
                text.DOFade(1, 0.1f);
            }
        }
    }

    private void ChangeSprite(Sprite sprite, float size)
    {
        crosshair.sprite = sprite;
        DOTween.Kill(crosshairRect);
        crosshairRect.DOScale(new Vector2(size, size), 0.2f).SetEase(crosshairEase);
    }
}
