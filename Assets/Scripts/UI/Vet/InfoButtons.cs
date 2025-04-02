using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButtons : MonoBehaviour
{
    [SerializeField] private GameObject infoScreen;


    private Transform parent;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<VetWindows>().transform;
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            parent.gameObject.GetComponent<CanvasGroup>().interactable = false;
            Instantiate(infoScreen, parent);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
