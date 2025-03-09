using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShrimpView : ScreenView
{

    [SerializeField]
    private TMP_InputField title;
    [SerializeField]
    private GameObject tankView;
    [SerializeField]
    private TextMeshProUGUI age, gender, body, pattern, legs, tail, tailFan, eyes, head;
    [SerializeField]
    private RenderTexture texture;
    protected Shrimp _shrimp;
    [SerializeField]
    private GameObject currentTankScreen;
    [SerializeField] private Image primaryColour, secondaryColour;

    public void Click()
    {
        CurrentTankScreen screen = Instantiate(currentTankScreen, UIManager.instance.GetCanvas()).GetComponent<CurrentTankScreen>();
        UIManager.instance.ChangeFocus(screen);
        screen.SetShrimp(_shrimp);
    }

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

    /// <summary>
    /// Fills the shrimp view with the information about shrimp
    /// </summary>
    /// <param name="Shrimp"></param>
    public void Populate(Shrimp Shrimp)
    {
        _shrimp = Shrimp;
        title.text = _shrimp.stats.name;
        //title.placeholder.GetComponent<TextMeshProUGUI>().text = _shrimp.stats.name;
        age.text = "Age: " + TimeManager.instance.GetShrimpAge(_shrimp.stats.birthTime).ToString();
        gender.text = "Gender: " + (_shrimp.stats.gender == true ? "M" : "F");
        pattern.text = "Pattern: " + GeneManager.instance.GetTraitSO(_shrimp.stats.pattern.activeGene.ID).traitName;
        body.text = "Body: " + GeneManager.instance.GetTraitSO(_shrimp.stats.body.activeGene.ID).set;
        legs.text = "Legs: " + GeneManager.instance.GetTraitSO(_shrimp.stats.body.activeGene.ID).set;
        eyes.text = "Eyes: " + GeneManager.instance.GetTraitSO(_shrimp.stats.eyes.activeGene.ID).set;
        tail.text = "Tail: " + GeneManager.instance.GetTraitSO(_shrimp.stats.tail.activeGene.ID).set;
        head.text = "Head: " + GeneManager.instance.GetTraitSO(_shrimp.stats.head.activeGene.ID).set;
        tailFan.text = "Tail Fan: " + GeneManager.instance.GetTraitSO(_shrimp.stats.tailFan.activeGene.ID).set;
        primaryColour.color = GeneManager.instance.GetTraitSO(_shrimp.stats.primaryColour.activeGene.ID).color;
        secondaryColour.color = GeneManager.instance.GetTraitSO(_shrimp.stats.secondaryColour.activeGene.ID).color;
    }


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
        _shrimp.name = input.text;
        _shrimp.stats.name = input.text;
        title.text = input.text;

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
