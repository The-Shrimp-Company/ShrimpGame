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
            foreach(Shrimp shrimp in shrimpInTank)
            {
                shrimp.UpdateShrimp(updateTimer);
            }

            updateTimer = 0;
        }
    }


    private void SpawnRandomShrimp()
    {
        GameObject newShrimp = Instantiate(shrimpPrefab, GetRandomTankPosition(), Quaternion.identity);
        Shrimp s = newShrimp.GetComponent<Shrimp>();

        s.stats = ShrimpManager.instance.CreateShrimp();
        newShrimp.name = s.stats.name;

        ShrimpMovement sm = new ShrimpMovement(); 
        s.shrimpActivities.Add(sm);
        sm.taskTime = Random.Range(4, 8);
        sm.shrimp = s.gameObject;
        sm.SetDestination(GetRandomTankPosition());

        ShrimpSleeping ss = new ShrimpSleeping();
        s.shrimpActivities.Add(ss);
        ss.taskTime = Random.Range(4, 8);
        ss.shrimp = s.gameObject;

        sm = new ShrimpMovement();
        s.shrimpActivities.Add(sm);
        sm.taskTime = Random.Range(4, 8);
        sm.shrimp = s.gameObject;
        sm.SetDestination(GetRandomTankPosition());

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
