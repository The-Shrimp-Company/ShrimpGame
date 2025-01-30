using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public List<Shrimp> shrimpInTank;

    private float spawnTimer;
    public float spawnTime;

    private float updateTimer;
    public float updateTime;  // The time between each shrimp update, 0 will be every frame

    private Vector3 tankPos;
    public Vector3 tankSize;

    public GameObject shrimpPrefab;


    void Start()
    {
        tankPos = transform.position;

        for (int i = 0; i < 5; i++) 
        {
            SpawnRandomShrimp();
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
            updateTime = 0;

            foreach(Shrimp shrimp in shrimpInTank)
            {
                shrimp.UpdateShrimp();
            }
        }
    }


    private void SpawnRandomShrimp()
    {
        GameObject newShrimp = Instantiate(shrimpPrefab, GetRandomTankPosition(), Quaternion.identity);
        Shrimp s = newShrimp.GetComponent<Shrimp>();

        s.stats = ShrimpManager.instance.CreateShrimp();
        newShrimp.name = s.stats.name;

        shrimpInTank.Add(s);
    }


    private Vector3 GetRandomTankPosition()
    {
        float x = Random.Range(-tankSize.x, tankSize.x) + tankPos.x;
        float y = Random.Range(-tankSize.y, tankSize.y) + tankPos.y;
        float z = Random.Range(-tankSize.z, tankSize.z) + tankPos.z;
        return new Vector3(x, y, z);
    }
}
