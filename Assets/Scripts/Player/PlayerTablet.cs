using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTablet : MonoBehaviour
{
    [SerializeField]
    private GameObject Tablet;

    // Start is called before the first frame update
    void Start()
    {
        Tablet.SetActive(false);
    }

    public void OnOpenTablet()
    {
        Tablet.SetActive(!Tablet.activeSelf);
    }
}
