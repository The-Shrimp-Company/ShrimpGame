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

    private bool toKill = false;  // If it should be destroyed at the end of this frame

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
            AddActivity(GetRandomActivity());
            AddActivity(GetRandomActivity());
            AddActivity(GetRandomActivity());
        }

        moltSpeed = ShrimpManager.instance.GetMoltTime(TimeManager.instance.GetShrimpAge(stats.birthTime));
        agent.shrimpModel.localScale = ShrimpManager.instance.GetShrimpSize(TimeManager.instance.GetShrimpAge(stats.birthTime));
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
            agent.shrimpModel.localScale = ShrimpManager.instance.GetShrimpSize(age);

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
                    if (stats.canBreed &&
                        s.stats.canBreed)
                    {
                        validShrimp.Add(s);
                    }
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

    public float FindValue()
    {
        return (EconomyManager.instance.GetShrimpValue(stats));
    }
}