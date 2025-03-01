using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public List<Shrimp> shrimpInTank = new List<Shrimp>();
    [HideInInspector] public List<Shrimp> shrimpToAdd = new List<Shrimp>();
    public List<Shrimp> shrimpToRemove = new List<Shrimp>();

    private float updateTimer;
    public float updateTime;  // The time between each shrimp update, 0 will be every frame

    private bool _saleTank = false;
    [SerializeField] private GameObject sign;

    public Transform shrimpParent;
    public TankGrid tankGrid;  // The grid used for pathfinding

    [SerializeField] private GameObject camDock;

    public GameObject shrimpPrefab;

    [SerializeField] private GameObject tankViewPrefab;

    [SerializeField] bool autoSpawnTestShrimp;

    void Start()
    {
        if (tankGrid == null) Debug.LogError("Pathfinding grid is missing");
        if (shrimpParent == null) Debug.LogError("Shrimp Parent is missing");


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

            if (shrimpToAdd.Count > 0)  // Add shrimp to the tank
            {
                for (int i = shrimpToAdd.Count - 1; i >= 0; i--)
                {
                    shrimpInTank.Add(shrimpToAdd[i]);
                    ShrimpManager.instance.allShrimp.Add(shrimpToAdd[i]);
                    shrimpToAdd.RemoveAt(i);
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
                    }

                    shrimpToRemove.RemoveAt(i);
                }
            }


            foreach (Shrimp shrimp in shrimpInTank)  // Update the shrimp in the tank
            {
                shrimp.UpdateShrimp(updateTimer);
            }

            updateTimer = 0;
        }
    }


    public void ToggleSaleTank()
    {
        _saleTank = !_saleTank;
        sign.SetActive(_saleTank);
    }


    private void SpawnRandomShrimp()
    {
        GameObject newShrimp = Instantiate(shrimpPrefab, GetRandomTankPosition(), Quaternion.identity);
        Shrimp s = newShrimp.GetComponent<Shrimp>();

        s.stats = ShrimpManager.instance.CreateRandomShrimp();
        s.ChangeTank(this);
        newShrimp.name = s.stats.name;
        newShrimp.transform.parent = shrimpParent;

        shrimpToAdd.Add(s);
    }


    public void SpawnShrimp()
    {
        SpawnRandomShrimp();
    }


    public Vector3 GetRandomTankPosition()
    {
        List<GridNode> freePoints = tankGrid.GetFreePoints();
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


    public void FocusTank()
    {
        GameObject newView = Instantiate(tankViewPrefab, transform);
        UIManager.instance.ChangeFocus(newView.GetComponent<ScreenView>());
        Debug.Log(UIManager.instance.GetCurrentRect());
    }
}
