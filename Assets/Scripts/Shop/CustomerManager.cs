using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour 
{
    static public CustomerManager Instance;

    public List<Shrimp> ToPurchase { get; private set; } = new List<Shrimp>();

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (ShrimpManager.instance.allShrimp.Count > 0)
        {
            if (Random.Range(0, 100) == 1)
            {
                int RandShrimp = Random.Range(0, ShrimpManager.instance.allShrimp.Count);
                if (!ToPurchase.Contains(ShrimpManager.instance.allShrimp[RandShrimp]))
                AddShrimpToPurchase(ShrimpManager.instance.allShrimp[RandShrimp]);
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
        }
        else
        {
            Debug.Log("What");
            Debug.Log("Ok that's not helpful. A shrimp disappeared while the sell screen was opened, and the screen wasn't updated");
        }
    }
}
