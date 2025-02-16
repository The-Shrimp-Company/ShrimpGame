using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public List<Shrimp> shrimpInTank;

    private float spawnTimer;
    public float spawnTime;

    private float updateTimer;
    public float updateTime;  // The time between each shrimp update, 0 will be every frame

    private bool _saleTank = false;
    [SerializeField] private GameObject sign;

    [SerializeField] Transform shrimpParent;
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
            for (int i = 0; i < 5; i++)
            {
                SpawnRandomShrimp();
            }
        }
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        updateTimer += Time.deltaTime;

        if (spawnTimer >= spawnTime)
        {
            spawnTimer = 0;

            //SpawnRandomShrimp();
        }



        if (updateTimer >= updateTime)
        {
            foreach(Shrimp shrimp in shrimpInTank)
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

        s.stats = ShrimpManager.instance.CreateShrimp();
        s.ChangeTank(this);
        newShrimp.name = s.stats.name;
        newShrimp.transform.parent = shrimpParent;

        shrimpInTank.Add(s);
    }


    public void SpawnShrimp()
    {
        SpawnRandomShrimp();
    }


    public Vector3 GetRandomTankPosition()
    {
        List<GridNode> freePoints = TankGrid.Instance.GetFreePoints();
        return freePoints[Random.Range(0, freePoints.Count)].worldPos;
    }


    public GridNode GetRandomTankNode()
    {
        List<GridNode> freePoints = TankGrid.Instance.GetFreePoints();
        return freePoints[Random.Range(0, freePoints.Count)];
    }


    public GameObject GetCam()
    {
        return camDock;
    }


    public void FocusTank()
    {
        GameObject newView = Instantiate(tankViewPrefab, transform);
        UIManager.instance.ChangeFocus(newView.GetComponent<TabletInteraction>());
    }
}
