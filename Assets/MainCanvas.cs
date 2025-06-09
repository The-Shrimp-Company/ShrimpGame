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

    [SerializeField]
    private GameObject tablet;
    private RectTransform _tabletRect;
    [SerializeField]
    private RectTransform _tabletRestingCoord;
    [SerializeField]
    private RectTransform _tabletActiveCoord;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.SetCanvas(transform);
        _tabletRect = tablet.GetComponent<RectTransform>();
    }

    public GameObject RaiseScreen(GameObject screen)
    {
        RaiseTablet();
        lastCreated = Instantiate(screen, GetComponentInChildren<ShelfRef>().transform);
        UIManager.instance.OpenScreen(lastCreated.GetComponent<ScreenView>());
        UIManager.instance.GetCursor().GetComponent<Image>().maskable = true;
        return lastCreated;
    }

    public void LowerScreen()
    {
        LowerTablet();
        Destroy(lastCreated);
        UIManager.instance.GetCursor().GetComponent<Image>().maskable = false;
        UIManager.instance.CloseScreen();
    }

    public void RaiseTablet()
    {
        RectTools.ChangeRectTransform(_tabletRect, _tabletActiveCoord);
    }

    public void LowerTablet()
    {
        RectTools.ChangeRectTransform(_tabletRect, _tabletRestingCoord);
    }

    public ShelfSpawn GetShelves()
    {
        return shelves;
    }
}
