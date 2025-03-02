using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShrimpView : ScreenView
{

    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private GameObject tankView;
    [SerializeField]
    private TextMeshProUGUI age;
    [SerializeField]
    private TextMeshProUGUI gender;
    [SerializeField]
    private RenderTexture texture;
    protected Shrimp _shrimp;

    protected override void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Update()
    {
        //base.Update();
        if (_shrimp != null)
        {
            player.GetComponent<PlayerUIController>().SetShrimpCam(_shrimp.GetComponentInChildren<ShrimpCam>());
            //Camera.main.transform.position = _shrimp.camDock.transform.position;
            //Camera.main.transform.LookAt(_shrimp.transform.position);
        }
    }

    public void Populate(Shrimp Shrimp)
    {
        _shrimp = Shrimp;
        title.text = _shrimp.stats.name;
        age.text = "Age: " + _shrimp.stats.age.ToString();
        gender.text = "Gender: " + (_shrimp.stats.gender == true ? "M" : "F");
    }

    /*
    protected override void PressedButton()
    {
        _clickedButtonUsed = true;
        base.PressedButton();
        _clickedButtonUsed = true;
        switch (_clickedButton)
        {
            case "Exit":
                GameObject newitem = Instantiate(tankView, _shrimp.tank.transform);
                Camera.main.transform.position = _shrimp.tank.GetCam().transform.position;
                Camera.main.transform.rotation = _shrimp.tank.GetCam().transform.rotation;
                UIManager.instance.ChangeFocus(newitem.GetComponent<TabletInteraction>());
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

    public override void Exit()
    {
        _shrimp.GetComponentInChildren<ShrimpCam>().Deactivate();
        player.GetComponent<PlayerUIController>().UnsetShrimpCam();
        GameObject newitem = Instantiate(tankView, _shrimp.tank.transform);
        TankController tank = _shrimp.tank.GetComponent<TankController>();
        Camera.main.transform.position = tank.GetCam().transform.position;
        Camera.main.transform.rotation = tank.GetCam().transform.rotation;
        UIManager.instance.ChangeFocus(newitem.GetComponent<ScreenView>());
    }

    public void SetName(TextMeshProUGUI input)
    {
        Debug.Log(input.text);

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

        _shrimp.GetComponentInChildren<ShrimpCam>().Deactivate();
        player.GetComponent<PlayerUIController>().UnsetShrimpCam();
        base.Close();
    }
}
