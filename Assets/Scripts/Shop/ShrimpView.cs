using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShrimpView : TankViewScript
{

    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private GameObject tankView;



    public override void Update()
    {
        base.Update();
        if (_shrimp != null)
        {
            Camera.main.transform.position = _shrimp.transform.position - Vector3.right;
            Camera.main.transform.LookAt(_shrimp.transform.position);
        }
    }

    public void Populate(Shrimp Shrimp)
    {
        _shrimp = Shrimp;
        title.text = _shrimp.name;
    }

    protected override void PressedButton()
    {
        _clickedButtonUsed = true;
        base.PressedButton();
        _clickedButtonUsed = true;
        switch (_clickedButton)
        {
            case "Exit":
                Debug.Log("Hello");
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
}
