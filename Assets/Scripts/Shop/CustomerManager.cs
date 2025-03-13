using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomerManager : MonoBehaviour 
{
    static public CustomerManager Instance;
    

    private int lastDay = 0;

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
        if (TimeManager.instance.day != lastDay)
        {
            lastDay = TimeManager.instance.day;
            if (ShrimpManager.instance.allShrimp.Count > 0)
            {
                float numToSell = Random.Range(0, ShrimpManager.instance.allShrimp.Count / 2 + 1);
                for (int i = 0; i < numToSell; i++)
                {
                    int RandShrimp = Random.Range(0, ShrimpManager.instance.allShrimp.Count);
                    if (!ToPurchase.Contains(ShrimpManager.instance.allShrimp[RandShrimp]))
                    {
                        AddShrimpToPurchase(ShrimpManager.instance.allShrimp[RandShrimp]);
                    }
                }
            }
        }
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
        if (ToPurchase.Contains(shrimp))
        {
            ToPurchase.Remove(shrimp);
            shrimp.tank.shrimpToRemove.Add(shrimp);
            Money.instance.AddMoney(shrimp.FindValue());
            EconomyManager.instance.UpdateTraitValues(false, shrimp.stats);
        }
        else
        {
            Debug.Log("What");
            Debug.Log("Ok that's not helpful. A shrimp disappeared while the sell screen was opened, and the screen wasn't updated");
        }
    }

    public void MakeRequest()
    {
        ShrimpStats s = ShrimpManager.instance.CreateRandomShrimp();
        ShrimpStats obfs = s;
        string message = "";

        List<int> traits = new List<int>{ 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int loop = 0;

        do
        {
            loop += 1;
            int index = traits[Random.Range(0, traits.Count)];
            traits.Remove(index);
            switch (index)
            {
                case 1:
                    obfs.pattern.obfuscated = true;
                    message += "Pattern: " + GeneManager.instance.GetTraitSO(obfs.pattern.activeGene.ID).name + ". ";
                    break;
                case 2:
                    obfs.body.obfuscated = true;
                    message += "Body: " + GeneManager.instance.GetTraitSO(obfs.body.activeGene.ID).name + ". ";
                    break;
                case 3:
                    obfs.head.obfuscated = true;
                    message += "Head: " + GeneManager.instance.GetTraitSO(obfs.head.activeGene.ID).name + ". ";
                    break;
                case 4:
                    obfs.secondaryColour.obfuscated = true;
                    message += "Secondary Colour: " + GeneManager.instance.GetTraitSO(obfs.secondaryColour.activeGene.ID).name + ". ";
                    break;
                case 5:
                    obfs.primaryColour.obfuscated = true;
                    message += "Primary Colour: " + GeneManager.instance.GetTraitSO(obfs.primaryColour.activeGene.ID).name + ". ";
                    break;
                case 6:
                    obfs.legs.obfuscated = true;
                    message += "Legs: " + GeneManager.instance.GetTraitSO(obfs.legs.activeGene.ID).name + ". ";
                    break;
                case 7:
                    obfs.tail.obfuscated = true;
                    message += "Tail: " + GeneManager.instance.GetTraitSO(obfs.tail.activeGene.ID).name + ". ";
                    break;
                case 8:
                    obfs.tailFan.obfuscated = true;
                    message += "Tail Fan: " + GeneManager.instance.GetTraitSO(obfs.tailFan.activeGene.ID).name + ". ";
                    break;
                case 9:
                    obfs.eyes.obfuscated = true;
                    message += "Eyes: " + GeneManager.instance.GetTraitSO(obfs.eyes.activeGene.ID).name + ". ";
                    break;

            }
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
    
}

public class Request
{
    public EmailScreen emailScreen;

    public ShrimpStats stats;
    public ShrimpStats obfstats;

    public void OpenShrimpSelection()
    {
        emailScreen.OpenSelection(this);
    }
}
