using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomerManager : MonoBehaviour 
{
    static public CustomerManager Instance;

    private int count = 0;
    private int coolDown = 0;


    public List<TankController> openTanks = new List<TankController>();
    public List<Shrimp> ToPurchase { get; private set; } = new List<Shrimp>();

    private List<Request> requests = new List<Request>();


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if(count < openTanks.Count)
        {
            TankController currentTank = openTanks[count];
            foreach(Shrimp shrimp in currentTank.shrimpInTank)
            {
                float value = EconomyManager.instance.GetShrimpValue(shrimp.stats);
                float chance = currentTank.openTankPrice / value;
                //Debug.Log(chance);
                if (Random.value * 2 > chance)
                {
                    PurchaseShrimp(shrimp);
                }
            }
        }

        count++;

        if(count >= openTanks.Count)
        {
            count = 0;
        }

        if(Random.Range(0, 1000) == 1 && requests.Count < 5 && coolDown < 0)
        {
            coolDown = 300;
            MakeRequest();
        }
        coolDown--;
    }



    public void AddShrimpToPurchase(Shrimp shrimp)
    {
        ToPurchase.Add(shrimp);
        if(UIManager.instance.GetScreen() != null && UIManager.instance.GetScreen().GetComponent<SellScreenView>() != null)
        {
            UIManager.instance.GetScreen().GetComponent<SellScreenView>().UpdateList(shrimp);
        }
    }

    public void PurchaseShrimp(Shrimp shrimp)
    {
        if (shrimp != null)
        {
            shrimp.tank.shrimpToRemove.Add(shrimp);
            Money.instance.AddMoney(shrimp.tank.openTankPrice);
            Reputation.AddReputation(1);
            EconomyManager.instance.UpdateTraitValues(false, shrimp.stats);

            Email email = new Email();
            email.title = shrimp.stats.name + " has been sold";
            email.subjectLine = "�" + shrimp.tank.openTankPrice + " has been deposited into your account";
            email.mainText = shrimp.stats.name + " was in " + shrimp.tank.tankName;
            EmailManager.SendEmail(email);

            PlayerStats.stats.shrimpSold++;
            Destroy(shrimp.gameObject);
        }
    }

    public void PurchaseShrimp(Shrimp shrimp, float value)
    {
        if (shrimp != null)
        {
            ToPurchase.Remove(shrimp);
            shrimp.tank.shrimpToRemove.Add(shrimp);
            Debug.Log(value);
            Money.instance.AddMoney(value);
            EconomyManager.instance.UpdateTraitValues(false, shrimp.stats);
            Destroy(shrimp.gameObject);

            Reputation.AddReputation(1);
            Debug.Log("Reputation: " + Reputation.GetReputation());

            Email email = new Email();
            email.title = "Thanks!";
            email.subjectLine = "I Love this shrimp!";
            email.mainText = "It's just what I wanted, so I got you this bonus!";
            email.value = Mathf.RoundToInt(shrimp.GetValue());
            email.CreateEmailButton("Add money", email.GiveMoney, true);
            EmailManager.SendEmail(email, true, Random.Range(10, 30));
        }
    }

    public void MakeRequest()
    {
        ShrimpStats s = ShrimpManager.instance.CreateRequestShrimp();
        ShrimpStats obfs = s;
        string message = "";

        List<int> traits = new List<int>{ 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int loop = 0;

        do
        {
            loop += 1;
            int index = traits[Random.Range(0, traits.Count)];
            traits.Remove(index);
            message += RequestPrepend();
            switch (index)
            {
                case 1:
                    obfs.pattern.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.pattern.activeGene.ID).traitName;
                    break;
                case 2:
                    obfs.body.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.body.activeGene.ID).traitName;
                    break;
                case 3:
                    obfs.head.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.head.activeGene.ID).traitName;
                    break;
                case 4:
                    obfs.secondaryColour.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.secondaryColour.activeGene.ID).traitName + " secondary colour";
                    break;
                case 5:
                    obfs.primaryColour.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.primaryColour.activeGene.ID).traitName + " primary colour";
                    break;
                case 6:
                    obfs.legs.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.legs.activeGene.ID).traitName;
                    break;
                case 7:
                    obfs.tail.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.tail.activeGene.ID).traitName;
                    break;
                case 8:
                    obfs.tailFan.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.tailFan.activeGene.ID).traitName;
                    break;
                case 9:
                    obfs.eyes.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.eyes.activeGene.ID).traitName;
                    break;

            }
            message += ".\n";
        } while (loop < 4 && Random.Range(1, 5) > 1);


        float value = EconomyManager.instance.GetObfsShrimpValue(obfs);
        message += "I will pay " + value + " plus a bonus if the shrimp is very good";

        Request request = new Request();
        request.stats = s;
        request.obfstats = obfs;
        request.value = value;
        Email email = new Email();
        email.title = "Shrimp Request";
        email.subjectLine = "I would like a shrimp";
        email.mainText = message;
        email.CreateEmailButton("Choose Shrimp", request.OpenShrimpSelection);
        EmailManager.SendEmail(email, true);
        request.email = email;
        requests.Add(request);
    }

    public void EmailOpen(EmailScreen emailScreen)
    {
        Debug.Log("Recieved");
        foreach(Request request in requests)
        {
            request.emailScreen = emailScreen;
        }
    }

    public void CompleteRequest(Request request)
    {
        requests.Remove(request);
    }
    
    private string RequestPrepend()
    {
        string message;

        message = Request.Words[Random.Range(0, Request.Words.Length)];

        return message;
    }
}



public class Request
{
    public EmailScreen emailScreen;

    public float value;

    public ShrimpStats stats;
    public ShrimpStats obfstats;

    public Email email;


    static public string[] Words = { 
    "I would like a shrimp with ",
    "It should have ",
    "Please can it have ",
    "My most favorite type of shrimp have ",
    "Please make sure it has ",
    "I will cry if it doesn't have "
    };


    public void OpenShrimpSelection()
    {
        emailScreen.OpenSelection(this);
    }

    
}
