using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TankViewScript : TabletInteraction
{
    
    private TankController tank;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    protected GameObject shrimpView;
    private Vector3 panelresting;
    protected Shrimp _shrimp;

    private void Start()
    {
        buttons = GetComponentsInChildren<RectTransform>().Where(x => x.CompareTag("Button")).ToArray();
        shelves = GetComponentInParent<ShelfSpawn>();
        tank = GetComponentInParent<TankController>();
        panelresting = panel.transform.position;
    }


    protected override void PressedButton()
    {
        _clickedButtonUsed = true;
        switch (_clickedButton)
        {
            case "Set Tank":
                shelves.SwitchSaleTank(tank);
                break;
            case "Hide Tank":
                SlideMenu();
                break;
            default:
                _clickedButtonUsed = false;
                break;
        }
        if (_clickedButtonUsed)
        {
            _clickedButton = null;
        }
    }


    private void SlideMenu()
    {
        if((panel.transform.position - panelresting).magnitude < 1)
        {
            panel.transform.position += Vector3.left * 250;
        }
        else
        {
            panel.transform.position = panelresting;
        }
    }

    public override void MouseClick(Vector3 point)
    {
        base.MouseClick(point);
        if(_clickedButton == null)
        {
            
            RaycastHit ray;
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(new Vector3(point.x, point.y, 30)) - Camera.main.transform.position, out ray, 3f, LayerMask.GetMask("Shrimp")))
            {
                _shrimp = ray.transform.GetComponent<Shrimp>();
                GameObject newitem = Instantiate(shrimpView);
                UIManager.instance.ChangeFocus(newitem.GetComponent<TabletInteraction>());
                newitem.GetComponent<ShrimpView>().Populate(_shrimp);
            }
        }
    }

    public override void Close()
    {
        Destroy(gameObject);
    }
}
