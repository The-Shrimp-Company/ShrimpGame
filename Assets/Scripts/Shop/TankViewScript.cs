using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System;

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
    [SerializeField]
    private TMP_InputField Name;
    [SerializeField]
    private TMP_InputField salePrice;

    protected override void Start()
    {
        player = GameObject.Find("Player");
        shelves = GetComponentInParent<ShelfSpawn>();
        tank = GetComponentInParent<TankController>();
        tank.tankViewScript = this;
        panelresting = panel.transform.position;
        Name.text = tank.tankName;
        salePrice.text = tank.openTankPrice.ToString();
        UpdateContent();
    }


    public virtual void Update()
    {
        //base.Update();

    }

    public void UpdateContent()
    {
        foreach (Transform child in _content.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Shrimp shrimp in tank.shrimpInTank)
        {
            TankContentBlock temp = Instantiate(_contentBlock, _content.transform).GetComponent<TankContentBlock>();
            temp.SetShrimp(shrimp);
            temp.SetText(shrimp.name);
        }


        if (tankPop != null)
        {
            tankPop.text = "Tank Population: " + tank.shrimpInTank.Count.ToString();
        }
    }

    public void SetTank()
    {
        shelves.SwitchSaleTank(tank);
    }

    public void SetOpenTank()
    {
        tank.toggleTankOpen();
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
            newitem.GetComponent<Canvas>().worldCamera = UIManager.instance.GetCamera();
            newitem.GetComponent<Canvas>().planeDistance = 1;
            UIManager.instance.GetCursor().GetComponent<Image>().maskable = false;
            _shrimp.GetComponentInChildren<ShrimpCam>().SetCam(); 
        }
    }

    public void SetName(TextMeshProUGUI input)
    {
        tank.tankName = input.text;
        Name.text = input.text;
        tank.tankNameChanged = true;
    }

    public void SetPrice()
    {
        Debug.Log("input: " + salePrice.text);
        tank.SetTankPrice(Convert.ToInt32(salePrice.text));
    }

    public void UISelect()
    {
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap("Empty");
    }

    public void UIUnselect()
    {
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap("TankView");
    }

    public override void Close()
    {
        UIManager.instance.GetCursor().GetComponent<Image>().maskable = true;
        base.Close();
        
    }
}
