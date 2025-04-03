using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBackButton : MonoBehaviour
{
    private GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Image>().gameObject;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GetComponentInParent<VetWindows>().toggleButton();
            Destroy(transform.parent.gameObject);
        });
    }

}
