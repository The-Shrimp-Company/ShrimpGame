using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Checkbox : MonoBehaviour
{
    [SerializeField] private Transform check;
    [SerializeField] private float checkSize = 0.8f;
    [SerializeField] private float tweenSpeed = 0.3f;
    [SerializeField] private Ease tweenEase;
    private bool isChecked = false;


    public void Toggle()
    {
        if (isChecked) Uncheck();
        else Check();
    }

    public void Check(bool tween = true)
    {
        isChecked = true;

        if (tween) Tween(checkSize);   
        else check.localScale = new Vector3(checkSize, checkSize, checkSize);
    }



    public void Uncheck(bool tween = true)
    {
        isChecked = false;

        if (tween) Tween(0);
        else check.localScale = new Vector3(0, 0, 0);
    }

    private void Tween(float size)
    {
        DOTween.Kill(check);
        check.DOScale(size, 0.2f).SetEase(tweenEase);
    }
}
