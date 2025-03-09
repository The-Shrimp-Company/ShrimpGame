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

    private bool toKill = false;  // If it should be destroyed at the end of this frame

    [Header("Breeding")]
    public GameObject breedingHeartParticles;


    public void Start()
    {
        transform.position = tank.GetRandomTankPosition();
        agent.tankGrid = tank.tankGrid;

        Email email = new Email();
        email.title = "New Shrimp!!@!";
        email.subjectLine = "WOOOOOOWWWWW";
        email.mainText = "The shrimp is in: " + tank.tankName;
        EmailManager.SendEmail(email);

        if (shrimpActivities.Count == 0)
        {
            AddActivity(GetRandomActivity());
            AddActivity(GetRandomActivity());
            AddActivity(GetRandomActivity());
        }
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
        Instantiate(GeneManager.instance.GetTraitSO(stats.body.activeGene.ID).part, transform).GetComponent<Body>().Construct(stats);

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
        float moltSpeed = ShrimpManager.instance.GetMoltTime(TimeManager.instance.GetShrimpAge(stats.birthTime)) * 60;
        while (moltTimer >= moltSpeed && moltSpeed != 0)
        {
            moltTimer -= moltSpeed;
            stats.moltHistory++;

            if (ShrimpManager.instance.CheckForMoltFail(TimeManager.instance.GetShrimpAge(stats.birthTime)))
            {
                // Molt has failed, the shrimp will now die
                KillShrimp();
            }

            moltSpeed = ShrimpManager.instance.GetMoltTime(TimeManager.instance.GetShrimpAge(stats.birthTime));
        }
    }


    public void EndActivity()
    {
        shrimpActivities.RemoveAt(0);

        if (shrimpActivities.Count <= minActivitiesInQueue)  // If the shrimp doesn't have enough tasks
        {
            AddActivity(GetRandomActivity());
        }
    }


    private ShrimpActivity GetRandomActivity()
    {
        int i = Random.Range(0, 3);
        if (i == 0) return new ShrimpMovement();
        if (i == 1) return new ShrimpSleeping();
        if (i == 2) return new ShrimpBreeding();
        return (new ShrimpActivity());
    }


    private void AddActivity(ShrimpActivity activity)
    {
        if (activity is ShrimpMovement)
        {
            ShrimpMovement movement = (ShrimpMovement) activity;  // Casts the activity to the derrived shrimpMovement activity
            movement.randomDestination = true;
        }


        else if (activity is ShrimpSleeping)
        {
            ShrimpSleeping sleeping = (ShrimpSleeping) activity;

            sleeping.taskTime = Random.Range(4, 8);
        }


        else if (activity is ShrimpBreeding)
        {
            // Find other shrimp
            List<Shrimp> validShrimp = new List<Shrimp>();
            foreach(Shrimp s in tank.shrimpInTank)
            {
                if (s.stats.gender != stats.gender)  // Get all shrimp of the opposite gender, also excludes this shrimp
                {
                    // Other logic for who can breed here
                    // Once every molt for female

                    validShrimp.Add(s);
                }
            }

            if (validShrimp.Count == 0)  // If there are no valid shrimp
            {
                AddActivity(GetRandomActivity());
                return;  // Cancel this and find a different activity
            }

            int i = Random.Range(0, validShrimp.Count);
            Shrimp otherShrimp = validShrimp[i];

            ShrimpBreeding otherBreeding = new ShrimpBreeding();
            otherBreeding.instigator = false;
            otherBreeding.shrimp = otherShrimp;
            otherBreeding.otherShrimp = this;
            otherShrimp.shrimpActivities.Add(otherBreeding);



            ShrimpBreeding breeding = (ShrimpBreeding)activity;
            breeding.instigator = true;
            breeding.otherShrimp = otherShrimp;
        }


        else
        {
            Debug.Log("Activity logic is missing");
        }


        activity.shrimp = this;
        shrimpActivities.Add(activity);
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

    public int FindValue()
    {
        // Put value funtion here
        return 11;
    }
}