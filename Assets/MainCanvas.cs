using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private GameObject lastCreated;

    private ShelfSpawn shelves;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.SetCanvas(transform);
    }

    public GameObject RaiseScreen(GameObject screen)
    {
        player.GetComponent<PlayerTablet>().OnOpenTablet();
        GetComponentInChildren<TabletInteraction>().gameObject.GetComponent<CanvasGroup>().interactable = false;
        lastCreated = Instantiate(screen, GetComponentInChildren<TabletInteraction>().transform.parent.transform);
        UIManager.instance.ChangeFocus(lastCreated.GetComponent<ScreenView>());
        UIManager.instance.GetCursor().GetComponent<Image>().maskable = true;
        return lastCreated;
    }

    public void LowerScreen()
    {
        player.GetComponent<PlayerTablet>().OnCloseTablet();
        Destroy(lastCreated);
        UIManager.instance.GetCursor().GetComponent<Image>().maskable = false;
        UIManager.instance.ChangeFocus();
    }

    public ShelfSpawn GetShelves()
    {
        return shelves;
    }
}
