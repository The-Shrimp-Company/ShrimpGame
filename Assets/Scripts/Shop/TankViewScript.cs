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
    [SerializeField]
    private GameObject _content;
    [SerializeField]
    private GameObject _contentBlock;

    protected override void Start()
    {
        shelves = GetComponentInParent<ShelfSpawn>();
        tank = GetComponentInParent<TankController>();
        panelresting = panel.transform.position;
        foreach(Shrimp shrimp in tank.shrimpInTank)
        {
            Instantiate(_contentBlock, _content.transform).GetComponent<ContentBlock>().SetText(shrimp.name);
        }
    }


    public virtual void Update()
    {
        //base.Update();
        if(tankPop != null)
        {
            tankPop.text = "Tank Population: " + tank.shrimpInTank.Count.ToString();
        }
    }

    /*
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
    */

    public void SetTank()
    {
        shelves.SwitchSaleTank(tank);
    }

    public void SlideMenu()
    {
        if((panel.transform.position - panelresting).magnitude < 1)
        {
            panel.transform.position += Vector3.left * 250 * UIManager.instance.GetCanvas().transform.localScale.x;
        }
        else
        {
            panel.transform.position = panelresting;
        }
    }

    
    public void MouseClick(Vector3 point, bool pressed)
    {
        RaycastHit ray;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(point), out ray, 3f, LayerMask.GetMask("Shrimp")))
        {
            _shrimp = ray.transform.GetComponent<Shrimp>();
            GameObject newitem = Instantiate(shrimpView);
            UIManager.instance.ChangeFocus(newitem.GetComponent<ScreenView>());
            newitem.GetComponent<ShrimpView>().Populate(_shrimp);
            _shrimp.GetComponentInChildren<ShrimpCam>().SetCam(); 
        }
    }
}
