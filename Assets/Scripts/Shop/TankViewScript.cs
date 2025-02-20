using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TankViewScript : ScreenView
{
    
    private TankController tank;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    protected GameObject shrimpView;
    private Vector3 panelresting;
    protected Shrimp _shrimp;
    [SerializeField]
    protected TextMeshProUGUI tankPop;

    protected override void Start()
    {
        buttons = GetComponentsInChildren<RectTransform>().Where(x => x.CompareTag("Button")).ToArray();
        shelves = GetComponentInParent<ShelfSpawn>();
        tank = GetComponentInParent<TankController>();
        panelresting = panel.transform.position;
    }

    public override void Update()
    {
        base.Update();
        if(tankPop != null)
        {
            tankPop.text = "Tank Population: " + tank.shrimpInTank.Count.ToString();
        }
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

    public override void MouseClick(Vector3 point, bool pressed)
    {
        base.MouseClick(point, pressed);
        if(_clickedButton == null)
        {
            
            RaycastHit ray;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(point), out ray, 3f, LayerMask.GetMask("Shrimp")))
            {
                Debug.Log("Here");
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
