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
    public float updateTime;                            // The time between each shrimp update, 0 will be every frame

    private bool _saleTank = false;
    [SerializeField]
    private GameObject sign;

    private Vector3 tankPos;
    private Vector3 tankSize;

    public GameObject shrimpPrefab;


    void Start()
    {
        tankPos = transform.position;

        sign.SetActive(_saleTank);

        tankSize = GetComponent<Collider>().bounds.size / 2;

        /*
        for (int i = 0; i < 5; i++) 
        {
            SpawnRandomShrimp();
        }*/
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

    public void switchSale()
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

        shrimpInTank.Add(s);
    }


    public void SpawnShrimp()
    {
        SpawnRandomShrimp();
    }

    public Vector3 GetRandomTankPosition()
    {
        float x = Random.Range(-tankSize.x, tankSize.x) + tankPos.x;
        float y = Random.Range(0, tankSize.y*2) + tankPos.y;
        float z = Random.Range(-tankSize.z, tankSize.z) + tankPos.z;
        return new Vector3(x, y, z);
    }
}
