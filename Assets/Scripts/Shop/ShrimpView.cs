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
    private GameObject currentTankScreen, medScreen;
    [SerializeField] private Image primaryColour, secondaryColour;
    [SerializeField] private Slider hunger;

    public void Update()
    {
        if (_shrimp != null)
        {
            hunger.value = _shrimp.stats.hunger;
        }
    }

    public void Click()
    {
        CurrentTankScreen screen = Instantiate(currentTankScreen, UIManager.instance.GetCanvas()).GetComponent<CurrentTankScreen>();
        UIManager.instance.ChangeFocus(screen, true);
        screen.SetShrimp(_shrimp);
    }

    public void MedScreen()
    {
        GameObject screen = Instantiate(medScreen, UIManager.instance.GetCanvas());
        //UIManager.instance.ChangeFocus(screen.GetComponent<ScreenView>());
        UIManager.instance.subMenu = true;
        screen.GetComponentInChildren<InventoryContent>().MedAssignment(this, _shrimp, screen);
    }

    public void MouseClick(Vector2 point)
    {
        RaycastHit ray;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(point), out ray, 3f, LayerMask.GetMask("Shrimp")))
        {

            _shrimp.gameObject.layer = LayerMask.NameToLayer("Shrimp");
            _shrimp.GetComponentInChildren<ShrimpCam>().Deactivate();
            player.GetComponent<PlayerUIController>().UnsetShrimpCam();
            _shrimp = ray.transform.GetComponent<Shrimp>();
            Populate(_shrimp);
            GetComponent<Canvas>().worldCamera = UIManager.instance.GetCamera();
            //GetComponent<Canvas>().planeDistance = 1;
            UIManager.instance.GetCursor().GetComponent<Image>().maskable = false;
            _shrimp.GetComponentInChildren<ShrimpCam>().SetCam();
            
        }
    }

    

    /// <summary>
    /// Fills the shrimp view with the information about shrimp
    /// </summary>
    /// <param name="Shrimp"></param>
    public void Populate(Shrimp Shrimp)
    {
        player = GameObject.Find("Player");
        _shrimp = Shrimp;
        title.text = _shrimp.stats.name;
        //title.placeholder.GetComponent<TextMeshProUGUI>().text = _shrimp.stats.name;
        age.text = "Age: " + (TimeManager.instance.GetShrimpAge(_shrimp.stats.birthTime) < 60 ? "Child" : "Adult");
        gender.text = "Gender: " + (_shrimp.stats.gender == true ? "M" : "F");
        pattern.text = "Pattern: " + GeneManager.instance.GetTraitSO(_shrimp.stats.pattern.activeGene.ID).traitName;
        body.text = "Body: " + GeneManager.instance.GetTraitSO(_shrimp.stats.body.activeGene.ID).set;
        legs.text = "Legs: " + GeneManager.instance.GetTraitSO(_shrimp.stats.legs.activeGene.ID).set;
        eyes.text = "Eyes: " + GeneManager.instance.GetTraitSO(_shrimp.stats.eyes.activeGene.ID).set;
        tail.text = "Tail: " + GeneManager.instance.GetTraitSO(_shrimp.stats.tail.activeGene.ID).set;
        head.text = "Head: " + GeneManager.instance.GetTraitSO(_shrimp.stats.head.activeGene.ID).set;
        tailFan.text = "Tail Fan: " + GeneManager.instance.GetTraitSO(_shrimp.stats.tailFan.activeGene.ID).set;
        primaryColour.color = GeneManager.instance.GetTraitSO(_shrimp.stats.primaryColour.activeGene.ID).colour;
        secondaryColour.color = GeneManager.instance.GetTraitSO(_shrimp.stats.secondaryColour.activeGene.ID).colour;
        hunger.value = _shrimp.stats.hunger;
        _shrimp.FocusShrimp();
        _shrimp.gameObject.layer = LayerMask.NameToLayer("SelectedShrimp");
        player.GetComponent<PlayerUIController>().SetShrimpCam(_shrimp.GetComponentInChildren<ShrimpCam>());
    }


    public override void Exit()
    {

        _shrimp.gameObject.layer = LayerMask.NameToLayer("Shrimp");
        _shrimp.GetComponentInChildren<ShrimpCam>().Deactivate();
        player.GetComponent<PlayerUIController>().UnsetShrimpCam();
        GameObject newitem = Instantiate(tankView, _shrimp.tank.transform);
        TankController tank = _shrimp.tank.GetComponent<TankController>();
        newitem.GetComponent<Canvas>().worldCamera = UIManager.instance.GetCamera();
        newitem.GetComponent<Canvas>().planeDistance = 1;
        Camera.main.transform.position = tank.GetCam().transform.position;
        Camera.main.transform.rotation = tank.GetCam().transform.rotation;
        UIManager.instance.ChangeFocus(newitem.GetComponent<ScreenView>());
    }

    public void SetName(TextMeshProUGUI input)
    {
        _shrimp.name = input.text;
        _shrimp.stats.name = input.text;
        title.text = input.text;
        _shrimp.shrimpNameChanged = true;
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

        _shrimp.gameObject.layer = LayerMask.NameToLayer("Shrimp");
        _shrimp.GetComponentInChildren<ShrimpCam>().Deactivate();
        _shrimp.StopFocussingShrimp();
        player.GetComponent<PlayerUIController>().UnsetShrimpCam();
        base.Close();
    }
}
