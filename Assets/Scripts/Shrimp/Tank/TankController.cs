using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

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
    [HideInInspector] public List<ShrimpFood> foodToAdd = new List<ShrimpFood>();
    [HideInInspector] public List<ShrimpFood> foodToRemove = new List<ShrimpFood>();
    public Transform foodParent;

    //[Header("Upgrades")]

    [Header("Sale Tank")]
    [SerializeField] private GameObject sign;
    private bool _saleTank = false;

    [Header("Pathfinding")]
    public TankGrid tankGrid;  // The grid used for pathfinding

    [Header("Tank Focus")]
    [SerializeField] private GameObject camDock;
    [SerializeField] private GameObject tankViewPrefab;
    [HideInInspector] public TankViewScript tankViewScript;

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

        sign.SetActive(_saleTank);


        if (autoSpawnTestShrimp)
        {
            for (int i = 0; i < 10; i++)
            {
                SpawnRandomShrimp();
            }
        }
    }

    void Update()
    {
        updateTimer += Time.deltaTime;

        if (updateTimer >= updateTime)
        {
            FoodUpdates();
            ShrimpUpdates();

            updateTimer = 0;
        }


        autoSpawnFoodTimer += Time.deltaTime;

        if (autoSpawnFoodTimer >= autoSpawnFoodTime && autoSpawnFoodTime != 0)
        {
            GameObject newFood = GameObject.Instantiate(autoSpawnFoodPrefab, GetRandomSurfacePosition(), Quaternion.identity);
            newFood.GetComponent<ShrimpFood>().CreateFood(this);

            autoSpawnFoodTimer = 0;
        }
    }


    private void ShrimpUpdates()
    {
        if (shrimpToAdd.Count > 0)  // Add shrimp to the tank
        {
            for (int i = shrimpToAdd.Count - 1; i >= 0; i--)
            {
                shrimpInTank.Add(shrimpToAdd[i]);
                ShrimpManager.instance.allShrimp.Add(shrimpToAdd[i]);
                shrimpToAdd.RemoveAt(i);

                if (tankViewScript != null) tankViewScript.UpdateContent();
            }
        }


        if (shrimpToRemove.Count > 0)  // Remove shrimp from the tank
        {
            for (int i = shrimpToRemove.Count - 1; i >= 0; i--)
            {
                if (shrimpInTank.Contains(shrimpToRemove[i]))
                {
                    shrimpInTank.Remove(shrimpToRemove[i]);
                    ShrimpManager.instance.allShrimp.Remove(shrimpToRemove[i]);

                    if (tankViewScript != null) tankViewScript.UpdateContent();

                    shrimpToRemove[i].Destroy();
                }

                shrimpToRemove.RemoveAt(i);
            }
        }


        foreach (Shrimp shrimp in shrimpInTank)  // Update the shrimp in the tank
        {
            shrimp.UpdateShrimp(updateTimer);
        }
    }


    private void FoodUpdates()
    {
        if (foodToAdd.Count > 0)  // Add food to the tank
        {
            for (int i = foodToAdd.Count - 1; i >= 0; i--)
            {
                foodInTank.Add(foodToAdd[i]);
                foodToAdd.RemoveAt(i);
            }
        }

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

        foreach (ShrimpFood food in foodInTank)  // Update the food in the tank
        {
            food.UpdateFood(updateTimer);
        }
    }


    public void ToggleSaleTank()
    {
        _saleTank = !_saleTank;
        sign.SetActive(_saleTank);
    }


    private void SpawnRandomShrimp()
    {
        GameObject newShrimp = Instantiate(ShrimpManager.instance.shrimpPrefab, GetRandomTankPosition(), Quaternion.identity);
        Shrimp s = newShrimp.GetComponent<Shrimp>();

        s.stats = ShrimpManager.instance.CreateRandomShrimp();
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

    public void MoveShrimp(Shrimp shrimp)
    {
        shrimp.tank.shrimpInTank.Remove(shrimp);
        shrimp.transform.parent = shrimpParent;
        shrimp.transform.position = GetRandomTankPosition();
        shrimp.ChangeTank(this);
        shrimpToAdd.Add(shrimp);
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
        GameObject newView = Instantiate(tankViewPrefab, transform);
        UIManager.instance.ChangeFocus(newView.GetComponent<ScreenView>());
        newView.GetComponent<Canvas>().worldCamera = UIManager.instance.GetCamera();
        newView.GetComponent<Canvas>().planeDistance = 1;
        UIManager.instance.GetCursor().GetComponent<Image>().maskable = false;
    }
}
