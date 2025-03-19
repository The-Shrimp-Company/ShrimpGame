using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomerManager : MonoBehaviour 
{
    static public CustomerManager Instance;

    private int count = 0;
    private int lastDay = 0;
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
                float chance = value / currentTank.openTankPrice;
                chance = Mathf.Log(chance) * 10;
                chance += 5;
                if (Random.Range(0f, 10f) < chance)
                {
                    ToPurchase.Add(shrimp);
                }
            }
        }

        count++;

        if(count < openTanks.Count)
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
        if(UIManager.instance.GetFocus() != null && UIManager.instance.GetFocus().GetComponent<SellScreenView>() != null)
        {
            UIManager.instance.GetFocus().GetComponent<SellScreenView>().UpdateList(shrimp);
        }
    }

    public void PurchaseShrimp(Shrimp shrimp)
    {
        if (shrimp != null)
        {
            ToPurchase.Remove(shrimp);
            shrimp.tank.shrimpToRemove.Add(shrimp);
            Money.instance.AddMoney(shrimp.tank.openTankPrice);
            EconomyManager.instance.UpdateTraitValues(false, shrimp.stats);
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
                    message += GeneManager.instance.GetTraitSO(obfs.pattern.activeGene.ID).traitName + " Pattern. ";
                    break;
                case 2:
                    obfs.body.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.body.activeGene.ID).traitName + " Body. ";
                    break;
                case 3:
                    obfs.head.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.head.activeGene.ID).traitName + " Head. ";
                    break;
                case 4:
                    obfs.secondaryColour.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.secondaryColour.activeGene.ID).traitName + " Secondary Colour. ";
                    break;
                case 5:
                    obfs.primaryColour.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.primaryColour.activeGene.ID).traitName + " Primary Colour. ";
                    break;
                case 6:
                    obfs.legs.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.legs.activeGene.ID).traitName + " Legs. ";
                    break;
                case 7:
                    obfs.tail.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.tail.activeGene.ID).traitName + " Tail. ";
                    break;
                case 8:
                    obfs.tailFan.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.tailFan.activeGene.ID).traitName + " Tail Fan. ";
                    break;
                case 9:
                    obfs.eyes.obfuscated = true;
                    message += GeneManager.instance.GetTraitSO(obfs.eyes.activeGene.ID).traitName + " Eyes. ";
                    break;

            }
            message += "\n";
        } while (loop < 4 && Random.Range(1, 5) > 1);


        Request request = new Request();
        request.stats = s;
        request.obfstats = obfs;
        Email email = new Email();
        email.title = "Shrimp Request";
        email.subjectLine = "I would like a shrimp";
        email.mainText = message;
        email.CreateEmailButton("Choose Shrimp", request.OpenShrimpSelection);
        Debug.Log(email.buttons);
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

    public ShrimpStats stats;
    public ShrimpStats obfstats;

    public Email email;


    static public string[] Words = { 
    "I would like a shrimp with ",
    "It should have ",
    "Please can it have ",
    "My most faviroute type of shrimp have ",
    "Please make sure it has ",
    "I will cry if it doesn't have "
    };


    public void OpenShrimpSelection()
    {
        emailScreen.OpenSelection(this);
    }
}
