using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TankController : MonoBehaviour
{
    [Header("Shrimp")]
    public List<Shrimp> shrimpInTank = new List<Shrimp>();
    [HideInInspector] public List<Shrimp> shrimpToAdd = new List<Shrimp>();
    [HideInInspector] public List<Shrimp> shrimpToRemove = new List<Shrimp>();
    public Transform shrimpParent;

    [Header("Tank")]
    public string tankName = null;

    [Header("Updates")]
    private float updateTimer;
    public float updateTime;  // The time between each shrimp update, 0 will be every frame

    [Header("Food")]
    public List<ShrimpFood> foodInTank = new List<ShrimpFood>();
    public int FoodStore = 0;
    [HideInInspector] public List<ShrimpFood> foodToAdd = new List<ShrimpFood>();
    [HideInInspector] public List<ShrimpFood> foodToRemove = new List<ShrimpFood>();
    public Transform foodParent;

    //[Header("Upgrades")]

    [Header("Sale Tank")]
    [SerializeField] private GameObject sign;
    public bool destinationTank { get; private set; } = false;
    [SerializeField] private GameObject SaleSign;
    public bool openTank { get; private set; } = false;
    [SerializeField] private TextMeshProUGUI label;
    public float openTankPrice = 50;
    [SerializeField] private TextMeshProUGUI openTankLabel;

    [Header("Pathfinding")]
    public TankGrid tankGrid;  // The grid used for pathfinding

    [Header("Tank Focus")]
    private bool focussingTank;
    [SerializeField] private GameObject camDock;
    [SerializeField] private GameObject tankViewPrefab;
    [HideInInspector] public TankViewScript tankViewScript;
    [HideInInspector] public bool tankNameChanged;

    [Header("Optimisation")]
    private LODLevel currentLODLevel;
    [SerializeField] float distanceCheckTime = 1;
    private float distanceCheckTimer;
    private Transform player;

    [Header("Debugging")]
    [SerializeField] bool autoSpawnTestShrimp;
    [SerializeField] float autoSpawnFoodTime;
    private float autoSpawnFoodTimer;
    [SerializeField] GameObject autoSpawnFoodPrefab;


    void Start()
    {
        if (tankGrid == null) Debug.LogError("Pathfinding grid is missing");
        if (shrimpParent == null) Debug.LogError("Shrimp Parent is missing");

        if (string.IsNullOrEmpty(tankName))
        {
            tankName = "Tank";
        }

        sign.SetActive(destinationTank);


        if (autoSpawnTestShrimp)
        {
            for (int i = 0; i < 10; i++)
            {
                SpawnRandomShrimp();
            }
        }

        SwitchLODLevel(LODLevel.Low);
    }

    void Update()
    {
        updateTimer += Time.deltaTime;

        if (updateTimer >= updateTime)
        {
            AddItems();
            RemoveItems();
            UpdateItems();
            updateTimer = 0;


        }


        distanceCheckTimer += Time.deltaTime;

        if (distanceCheckTimer >= distanceCheckTime)
        {
            CheckLODDistance();
            distanceCheckTimer = 0;
        }


         autoSpawnFoodTimer += Time.deltaTime;
        if (FoodStore > 0)
        {
            if (autoSpawnFoodTimer >= autoSpawnFoodTime && autoSpawnFoodTime != 0)
            {
                FoodStore -= 1;
                GameObject newFood = GameObject.Instantiate(autoSpawnFoodPrefab, GetRandomSurfacePosition(), Quaternion.identity);
                newFood.GetComponent<ShrimpFood>().CreateFood(this);

                autoSpawnFoodTimer = 0;
            }
        }


        if (openTank)
        {
            if(Random.Range(0, 1000) == 1)
            {
                if (shrimpInTank.Count > 0)
                {
                    Shrimp shrimp = shrimpInTank[Random.Range(0, shrimpInTank.Count)];
                    if (!CustomerManager.Instance.ToPurchase.Contains(shrimp))
                    {
                        CustomerManager.Instance.AddShrimpToPurchase(shrimp);
                    }
                }
            }
        }

        label.text = tankName;

        if (focussingTank) PlayerStats.stats.timeSpentFocusingTank += Time.deltaTime;
    }

    public void SetTankPrice(float price)
    {
        openTankPrice = price;
        openTankLabel.text = "All Shrimp " + price.ToString();
    }

    public void toggleTankOpen()
    {
        openTank = !openTank;
        if (openTank) CustomerManager.Instance.openTanks.Add(this);
        else CustomerManager.Instance.openTanks.Remove(this);
        SaleSign.SetActive(openTank);
        openTankLabel.text = "All Shrimp " + openTankPrice;
    }

    private void AddItems()
    {
        if (foodToAdd.Count > 0)  // Add food to the tank
        {
            for (int i = foodToAdd.Count - 1; i >= 0; i--)
            {
                foodInTank.Add(foodToAdd[i]);
                foodToAdd.RemoveAt(i);
            }
        }

        if (shrimpToAdd.Count > 0)  // Add shrimp to the tank
        {
            for (int i = shrimpToAdd.Count - 1; i >= 0; i--)
            {
                shrimpInTank.Add(shrimpToAdd[i]);
                ShrimpManager.instance.AddShrimpToStore(shrimpToAdd[i]);
                ShrimpManager.instance.allShrimp.Add(shrimpToAdd[i]);
                shrimpToAdd[i].SwitchLODLevel(currentLODLevel);
                shrimpToAdd.RemoveAt(i);

                if (tankViewScript != null) tankViewScript.UpdateContent();
            }
        }
    }

    private void RemoveItems()
    {
        if (foodToRemove.Count > 0)  // Remove food from the tank
        {
            for (int i = foodToRemove.Count - 1; i >= 0; i--)
            {
                if (foodInTank.Contains(foodToRemove[i]))
                {
                    foodInTank.Remove(foodToRemove[i]);

                    Destroy(foodToRemove[i].gameObject);
                }

                foodToRemove.RemoveAt(i);
            }
        }

        if (shrimpToRemove.Count > 0)  // Remove shrimp from the tank
        {
            for (int i = shrimpToRemove.Count - 1; i >= 0; i--)
            {
                if (shrimpInTank.Contains(shrimpToRemove[i]))
                {
                    shrimpInTank.Remove(shrimpToRemove[i]);
                    ShrimpManager.instance.RemoveShrimpFromStore(shrimpToRemove[i].stats);
                    ShrimpManager.instance.allShrimp.Remove(shrimpToRemove[i]);

                    if (tankViewScript != null) tankViewScript.UpdateContent();

                    shrimpToRemove[i].Destroy();
                }

                shrimpToRemove.RemoveAt(i);
            }
        }
    }

    private void UpdateItems()
    {
        foreach (ShrimpFood food in foodInTank)  // Update the food in the tank
        {
            food.UpdateFood(updateTimer);
        }

        foreach (Shrimp shrimp in shrimpInTank)  // Update the shrimp in the tank
        {
            shrimp.UpdateShrimp(updateTimer);
        }
    }



    public void ToggleDestinationTank()
    {
        destinationTank = !destinationTank;
        sign.SetActive(destinationTank);
    }


    private void SpawnRandomShrimp()
    {
        GameObject newShrimp = Instantiate(ShrimpManager.instance.shrimpPrefab, GetRandomTankPosition(), Quaternion.identity);
        Shrimp s = newShrimp.GetComponent<Shrimp>();

        s.stats = ShrimpManager.instance.CreateRandomShrimp(false);
        s.ChangeTank(this);
        newShrimp.name = s.stats.name;
        newShrimp.transform.parent = shrimpParent;
        newShrimp.transform.position = GetRandomTankPosition();
        s.ConstructShrimp();

        shrimpToAdd.Add(s);
    }


    public void SpawnShrimp()
    {
        SpawnRandomShrimp();
    }

    public void SpawnShrimp(ShrimpStats s, bool gameLoading = false)
    {
        GameObject newShrimp = Instantiate(ShrimpManager.instance.shrimpPrefab, GetRandomTankPosition(), Quaternion.identity);
        Shrimp shrimp = newShrimp.GetComponent<Shrimp>();
        shrimp.stats = s;

        shrimp.ChangeTank(this);
        shrimp.loadedShrimp = gameLoading;
        newShrimp.name = shrimp.stats.name;
        newShrimp.transform.parent = shrimpParent;
        newShrimp.transform.position = GetRandomTankPosition();
        shrimp.ConstructShrimp();

        shrimpToAdd.Add(shrimp);
    }
 
    public void MoveShrimp(Shrimp shrimp)
    {
        shrimp.tank.shrimpToRemove.Add(shrimp);
        shrimp.transform.parent = shrimpParent;
        shrimp.transform.position = GetRandomTankPosition();
        shrimp.ChangeTank(this);
        shrimpToAdd.Add(shrimp);
        PlayerStats.stats.shrimpMoved++;
    }

    public Vector3 GetRandomTankPosition()
    {
        List<GridNode> freePoints = tankGrid.GetFreePoints();
        return freePoints[Random.Range(0, freePoints.Count)].worldPos;
    }


    public Vector3 GetRandomSurfacePosition()
    {
        List<GridNode> freePoints = tankGrid.GetSurfacePoints();
        return freePoints[Random.Range(0, freePoints.Count)].worldPos;
    }


    public GridNode GetRandomTankNode()
    {
        List<GridNode> freePoints = tankGrid.GetFreePoints();
        return freePoints[Random.Range(0, freePoints.Count)];
    }


    public GameObject GetCam()
    {
        return camDock;
    }

    public void Ref()
    {
        Debug.Log("Yes");
    }

    public void FocusTank()
    {
        focussingTank = true;
        GameObject newView = Instantiate(tankViewPrefab, transform);
        UIManager.instance.ChangeFocus(newView.GetComponent<ScreenView>());
        newView.GetComponent<Canvas>().worldCamera = UIManager.instance.GetCamera();
        newView.GetComponent<Canvas>().planeDistance = 1;
        UIManager.instance.GetCursor().GetComponent<Image>().maskable = false;
        SwitchLODLevel(LODLevel.Mid);
        tankNameChanged = false;
    }

    public void StopFocussingTank()
    {
        focussingTank = false;
        SwitchLODLevel(LODLevel.Low);
        CheckLODDistance();

        if (tankNameChanged)
            PlayerStats.stats.tanksNamed++;
        tankNameChanged = false;
    }


    private void CheckLODDistance()
    {
        if (currentLODLevel != LODLevel.High)
        {
            if (player == null) player = GameObject.Find("Player").transform;

            float dist = Vector3.Distance(player.position, transform.position);


            if (dist < 4)
                SwitchLODLevel(LODLevel.Mid);

            else if (dist < 10)
                SwitchLODLevel(LODLevel.Low);

            else
                SwitchLODLevel(LODLevel.SuperLow);
        }
    }

    public void SwitchLODLevel(LODLevel level)  // Focused on shrimp
    {
        if (currentLODLevel == level) return;

        currentLODLevel = level;

        foreach (Shrimp s in shrimpInTank)
        {
            s.SwitchLODLevel(level);
        }

        switch (level)
        {
            case LODLevel.High:
                {
                    updateTime = 0;
                    break;
                }
            case LODLevel.Mid:
                {
                    updateTime = 0.01f;
                    updateTime *= ((ShrimpManager.instance.allShrimp.Count / 200) + 1);
                    break;
                }
            case LODLevel.Low:
                {
                    updateTime = 0.1f;
                    updateTime *= ((ShrimpManager.instance.allShrimp.Count / 200) + 1);
                    break;
                }
            case LODLevel.SuperLow:
                {
                    updateTime = 1.0f;
                    updateTime *= ((ShrimpManager.instance.allShrimp.Count / 200) + 1);
                    break;
                }
        }
    }
}

public enum LODLevel
{
    High,
    Mid,
    Low,
    SuperLow
}