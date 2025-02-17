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
    [SerializeField]
    private TextMeshProUGUI age;
    [SerializeField]
    private TextMeshProUGUI gender;
    [SerializeField]
    private RenderTexture texture;



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
        title.text = _shrimp.stats.name;
        age.text = "Age: " + _shrimp.stats.age.ToString();
        gender.text = "Gender: " + (_shrimp.stats.gender == true ? "M" : "F");
        
    }

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
}
