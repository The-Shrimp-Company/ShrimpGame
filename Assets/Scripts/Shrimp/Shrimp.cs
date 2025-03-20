using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrimp : MonoBehaviour
{
    public ShrimpStats stats;
    public List<ShrimpActivity> shrimpActivities = new List<ShrimpActivity>();
    public TankController tank;
    private int minActivitiesInQueue = 2;
    public ShrimpAgent agent;
    public Transform camDock;
    private float moltTimer;
    private float moltSpeed;

    public float currentValue;

    private bool toKill = false;  // If it should be destroyed at the end of this frame

    public Transform particleParent;

    [Header("Breeding")]
    public GameObject breedingHeartParticles;


    public void Start()
    {
        agent.tankGrid = tank.tankGrid;

        Email email = new Email();
        email.title = "New Shrimp!!@!";
        email.subjectLine = "WOOOOOOWWWWW";
        email.mainText = "The shrimp is in: " + tank.tankName;
        EmailManager.SendEmail(email);

        if (shrimpActivities.Count == 0)
        {
            ShrimpActivityManager.instance.AddActivity(this, null, true);
            ShrimpActivityManager.instance.AddActivity(this, null, true); 
            ShrimpActivityManager.instance.AddActivity(this, null, true);
        }

        moltSpeed = ShrimpManager.instance.GetMoltTime(TimeManager.instance.GetShrimpAge(stats.birthTime));
        agent.shrimpModel.localScale = ShrimpManager.instance.GetShrimpSize(TimeManager.instance.GetShrimpAge(stats.birthTime), stats.geneticSize);
    }

    private void LateUpdate()
    {
        if (toKill)
        {
            Destroy(gameObject);
        }
    }

    public void ConstructShrimp()
    {
        GameObject newShrimp = Instantiate<GameObject>(GeneManager.instance.GetTraitSO(stats.body.activeGene.ID).part, agent.shrimpModel);
        newShrimp.GetComponent<Body>().Construct(stats);
        //GetComponent<ShrimpAgent>().shrimpModel = newShrimp.transform;
    }

    public void UpdateShrimp(float elapsedTime)
    {
        // Activities      
        float timeRemaining = elapsedTime;  // The time since the last update
        do
        {
            if (shrimpActivities.Count > 0 && shrimpActivities[0] != null)
            {
                timeRemaining = shrimpActivities[0].Activity(timeRemaining);
                if (timeRemaining != 0)
                {
                    EndActivity();
                }
            }
            else
            {
                timeRemaining = 0;
            }
        }
        while (timeRemaining > 0);


        // Molting
        moltTimer += elapsedTime;
        while (moltTimer >= moltSpeed && moltSpeed != 0)
        {
            int age = TimeManager.instance.GetShrimpAge(stats.birthTime);

            moltTimer -= moltSpeed;
            stats.moltHistory++;
            agent.shrimpModel.localScale = ShrimpManager.instance.GetShrimpSize(age, stats.geneticSize);

            if (age >= ShrimpManager.instance.GetAdultAge())  // If the shrimp is considered an adult
                stats.canBreed = true;  // The shrimp can breed

            if (ShrimpManager.instance.CheckForMoltFail(age))
                KillShrimp();  // Molt has failed, the shrimp will now die

            moltSpeed = ShrimpManager.instance.GetMoltTime(age);
        }
    }


    public void EndActivity()
    {
        shrimpActivities.RemoveAt(0);

        if (shrimpActivities.Count <= minActivitiesInQueue)  // If the shrimp doesn't have enough tasks
        {
            ShrimpActivityManager.instance.AddActivity(this, null, true);
        }
    }


 


    public void ChangeTank(TankController t)
    {
        tank = t;
        agent.tankGrid = tank.tankGrid;

        // Clear all activities
    }

    private void KillShrimp()
    {
        tank.shrimpToRemove.Add(this);
        // Spawn dead body
        // Notification message
    }

    public void Destroy()
    {
        toKill = true;
    }

    public void SellThis()
    {
        CustomerManager.Instance.PurchaseShrimp(this, currentValue);
    }

    public int Bonus()
    {
        return Mathf.RoundToInt(EconomyManager.instance.GetShrimpValue(stats));
    }

    public float FindValue()
    {
        return (EconomyManager.instance.GetShrimpValue(stats));
    }


    public void FocusShrimp()
    {
        tank.SwitchLODLevel(LODLevel.High);
    }

    public void StopFocussingShrimp()
    {
        tank.SwitchLODLevel(LODLevel.Mid);
    }


    public void SwitchLODLevel(LODLevel level)  // Focused on shrimp
    {
        switch(level)
        {
            case LODLevel.High:
            {
                SwitchToHighLOD();
                break;
            }
            case LODLevel.Mid:
            {
                SwitchToMidLOD();
                break;
            }
            case LODLevel.Low:
            {
                SwitchToLowLOD();
                break;
            }
            case LODLevel.SuperLow:
            {
                SwitchToSuperLowLOD();
                break;
            }
        }
    }

    public void SwitchToHighLOD()  // Focused on Shrimp
    {
        agent.shrimpModel.gameObject.SetActive(true);
        particleParent.gameObject.SetActive(true);
    }

    public void SwitchToMidLOD()  // Focused on tank
    {
        agent.shrimpModel.gameObject.SetActive(true);
        particleParent.gameObject.SetActive(true);
    }

    public void SwitchToLowLOD()  // Near tank
    {
        agent.shrimpModel.gameObject.SetActive(true);
        particleParent.gameObject.SetActive(false);
    }

    public void SwitchToSuperLowLOD()  // Far from tank
    {
        agent.shrimpModel.gameObject.SetActive(false);
        particleParent.gameObject.SetActive(false);
    }
}