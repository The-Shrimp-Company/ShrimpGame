using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System;
using System.Xml;
using DG.Tweening;

public class TankViewScript : ScreenView
{
    
    private TankController tank;
    [SerializeField] private GameObject leftPanel;
    [SerializeField] protected GameObject shrimpView;
    protected Shrimp _shrimp;
    [SerializeField] protected TextMeshProUGUI tankPop;
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _contentBlock;
    private List<TankContentBlock> contentBlocks = new List<TankContentBlock>();

    [SerializeField] private TMP_InputField Name;
    [SerializeField] private TMP_InputField salePrice;
    [SerializeField] private TextMeshProUGUI foodAmount;
    [SerializeField] private GameObject inventoryScreen;

    [SerializeField] private GameObject currentTankScreen;

    private List<Shrimp> selectedShrimp = new List<Shrimp>();
    private bool allSelected = false;
    [SerializeField] Checkbox multiSelect;

    [SerializeField] private Animator contextBox;
    [SerializeField] private Animator upgradeBox;

    [SerializeField] private UpgradePanel upgrades;


    [Header("Panel Tweening")]
    private Vector3 leftPanelResting, upgradeBoxResting;
    [SerializeField] private Vector3 leftPanelTweenPos;
    [SerializeField] private Vector3 leftPanelSwitchInPos;
    [SerializeField] private Vector3 leftPanelSwitchOutPos;
    [SerializeField] private Vector3 upgradeBoxTweenPos;
    [SerializeField] private Vector3 upgradeBoxSwitchInPos;
    [SerializeField] private Vector3 upgradeBoxSwitchOutPos;


    public override void Open(bool switchTab)
    {
        player = GameObject.Find("Player");
        shelves = GetComponentInParent<ShelfSpawn>();
        tank = GetComponentInParent<TankController>();
        tank.tankViewScript = this;
        leftPanelResting = leftPanel.transform.position;
        upgradeBoxResting = upgradeBox.transform.position;
        Name.text = tank.tankName;
        salePrice.text = tank.openTankPrice.ToString();
        selectedShrimp = new List<Shrimp>();
        upgrades.Tank = tank;
        multiSelect.Uncheck(false);
        StartCoroutine(OpenTab(switchTab));
        UpdateContent();
        base.Open(switchTab);
    }


    public virtual void Update()
    {
        //base.Update();
        foodAmount.text = tank.FoodStore.ToString();

        if(selectedShrimp.Count > 0)
        {
            contextBox.SetBool("Selection", true);
            upgradeBox.SetBool("Expand", false);
        }
        else
        {
            contextBox.SetBool("Selection", false);
            upgradeBox.SetBool("Expand", true);
        }

        if(selectedShrimp.Count == 0)
        {
            if (allSelected)
            {
                multiSelect.Uncheck();
                allSelected = false;
            }
        }
        else if(selectedShrimp.Count == tank.shrimpInTank.Count)
        {
            if (!allSelected)
            {
                multiSelect.Check();
                allSelected = true;
            }
        }
    }

    public void MoveShrimp()
    {
        CurrentTankScreen screen = Instantiate(currentTankScreen, UIManager.instance.GetCanvas()).GetComponent<CurrentTankScreen>();
        UIManager.instance.ChangeFocus(screen);
        screen.SetShrimp(selectedShrimp.ToArray());
    }

    public void MedicateShrimp()
    {
        InventoryScreen screen = Instantiate(inventoryScreen, UIManager.instance.GetCanvas()).GetComponent<InventoryScreen>();
        screen.GetComponentInChildren<InventoryContent>().MedAssignment(this, selectedShrimp.ToArray(), screen.gameObject);
    }

    public void ChangeHeater()
    {
        InventoryScreen screen = Instantiate(inventoryScreen, UIManager.instance.GetCanvas()).GetComponent<InventoryScreen>();
        screen.GetComponentInChildren<InventoryContent>().UpgradeAssignment(tank.GetComponent<TankUpgradeController>(), UpgradeTypes.Heater, this, screen.gameObject);
    }

    public void ChangeFilter()
    {
        InventoryScreen screen = Instantiate(inventoryScreen, UIManager.instance.GetCanvas()).GetComponent<InventoryScreen>();
        screen.GetComponentInChildren<InventoryContent>().UpgradeAssignment(tank.GetComponent<TankUpgradeController>(), UpgradeTypes.Filter, this, screen.gameObject);
    }

    public void ChangeDecor()
    {
        InventoryScreen screen = Instantiate(inventoryScreen, UIManager.instance.GetCanvas()).GetComponent<InventoryScreen>();
        screen.GetComponentInChildren<InventoryContent>().UpgradeAssignment(tank.GetComponent<TankUpgradeController>(), UpgradeTypes.Decorations, this, screen.gameObject);
    }

    public void SelectAll()
    {
        if (allSelected)
        {
            selectedShrimp = new List<Shrimp>();
            foreach(TankContentBlock block in contentBlocks)
            {
                block.checkbutton.GetComponent<Checkbox>().Uncheck();
            }
            multiSelect.Uncheck();
        }
        else
        {
            foreach (Shrimp shrimp in tank.shrimpInTank)
            {
                selectedShrimp.Add(shrimp);
            }
            foreach(TankContentBlock block in contentBlocks)
            {
                block.checkbutton.GetComponent<Checkbox>().Check();
            }
            multiSelect.Check();
        }
        allSelected = !allSelected;
    }

    public void UpdateContent()
    {
        foreach (Transform child in _content.transform)
        {
            Destroy(child.gameObject);
        }

        contentBlocks.Clear();

        foreach (Shrimp shrimp in tank.shrimpInTank)
        {
            TankContentBlock temp = Instantiate(_contentBlock, _content.transform).GetComponent<TankContentBlock>();
            contentBlocks.Add(temp);
            Shrimp thisShrimp = shrimp;
            temp.main.onClick.AddListener(() =>
            {
                GameObject newitem = Instantiate(shrimpView);
                UIManager.instance.ChangeFocus(newitem.GetComponent<ScreenView>());
                newitem.GetComponent<ShrimpView>().Populate(thisShrimp);
                thisShrimp.GetComponentInChildren<ShrimpCam>().SetCam();
                newitem.GetComponent<Canvas>().worldCamera = UIManager.instance.GetCamera();
                newitem.GetComponent<Canvas>().planeDistance = 1;
                UIManager.instance.GetCursor().GetComponent<Image>().maskable = false;
            });
            temp.checkbutton.onClick.AddListener(() =>
            {
                if(selectedShrimp.Contains(thisShrimp))
                {
                    temp.checkbutton.GetComponent<Checkbox>().Uncheck();
                    selectedShrimp.Remove(thisShrimp);
                }
                else
                {
                    temp.checkbutton.GetComponent<Checkbox>().Check();
                    selectedShrimp.Add(thisShrimp);
                }
            });
            if (selectedShrimp.Contains(shrimp)) temp.checkbutton.GetComponent<Checkbox>().Check();
            else temp.checkbutton.GetComponent<Checkbox>().Uncheck();
            temp.SetText(shrimp.name);
            temp.SetShrimp(shrimp);
        }


        if (tankPop != null)
        {
            tankPop.text = "Tank Population: " + tank.shrimpInTank.Count.ToString();
        }
    }

    public void SetTank()
    {
        shelves.SwitchDestinationTank(tank);
    }

    public void SetOpenTank()
    {
        tank.toggleTankOpen();
    }

    public void SlideMenu()
    {
        if((leftPanel.transform.position - leftPanelResting).magnitude < 1)
        {
            leftPanel.transform.position += Vector3.left * 250 * UIManager.instance.GetCanvas().transform.localScale.x;
        }
        else
        {
            leftPanel.transform.position = leftPanelResting;
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
        tank.SetTankPrice(float.Parse(salePrice.text));
    }

    public void UISelect()
    {
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap("Empty");
    }

    public void UIUnselect()
    {
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap("TankView");
    }

    public override void Close(bool switchTab)
    {
        StartCoroutine(CloseTab(switchTab));
    }

    public void AddFood()
    {
        GameObject screen = Instantiate(inventoryScreen, UIManager.instance.GetCanvas());
        screen.GetComponentInChildren<InventoryContent>().FoodAssignement(this, tank, screen);
    }

    public IEnumerator OpenTab(bool switchTab)
    {
        UIManager.instance.GetCursor().GetComponent<Image>().maskable = true;

        leftPanel.transform.localPosition = switchTab ? leftPanelSwitchInPos : leftPanelTweenPos;
        upgradeBox.transform.localPosition = switchTab ? upgradeBoxSwitchInPos : upgradeBoxTweenPos;
        upgradeBox.enabled = false;
        contextBox.enabled = false;
        contextBox.gameObject.SetActive(false);

        if (switchTab)
        {
            yield return new WaitForSeconds(0.4f);

            leftPanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.OutBack, 1.4f);
            upgradeBox.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.OutBack, 1.4f);

            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            GetComponent<CanvasGroup>().alpha = 0f;
            GetComponent<CanvasGroup>().DOFade(1, 0.3f).SetEase(Ease.OutCubic);

            leftPanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.3f).SetEase(Ease.OutBack);
            upgradeBox.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.3f).SetEase(Ease.OutBack);

            yield return new WaitForSeconds(0.3f);
        }

        contextBox.gameObject.SetActive(true);
        upgradeBox.enabled = true;
        contextBox.enabled = true;
    }



    public IEnumerator CloseTab(bool switchTab)
    {
        UIManager.instance.GetCursor().GetComponent<Image>().maskable = true;
        DOTween.Kill(leftPanel);
        DOTween.Kill(upgradeBox);
        upgradeBox.enabled = false;
        contextBox.enabled = false;

        if (switchTab)
        {
            leftPanel.GetComponent<RectTransform>().DOAnchorPos(leftPanelSwitchOutPos, 0.5f).SetEase(Ease.InBack, 1.4f);
            upgradeBox.GetComponent<RectTransform>().DOAnchorPos(upgradeBoxSwitchOutPos, 0.5f).SetEase(Ease.InBack, 1.4f);
            contextBox.GetComponent<RectTransform>().DOAnchorPos(upgradeBoxSwitchOutPos, 0.5f).SetEase(Ease.InBack, 1.4f);

            yield return new WaitForSeconds(0.5f);
        }
        else  // Fully closing
        {
            leftPanel.GetComponent<RectTransform>().DOAnchorPos(leftPanelTweenPos, 0.3f).SetEase(Ease.InBack);
            upgradeBox.GetComponent<RectTransform>().DOAnchorPos(upgradeBoxTweenPos, 0.3f).SetEase(Ease.InBack);
            contextBox.GetComponent<RectTransform>().DOAnchorPos(upgradeBoxTweenPos, 0.3f).SetEase(Ease.InBack);
            GetComponent<CanvasGroup>().DOFade(0, 0.3f).SetEase(Ease.InCubic);

            yield return new WaitForSeconds(0.3f);
        }

        DOTween.Kill(leftPanel);
        DOTween.Kill(upgradeBox);
        DOTween.Kill(contextBox);
        base.Close(switchTab);
    }
}