using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VetWindows : MonoBehaviour
{
    [SerializeField]
    private int reputationReq = 0;

    [SerializeField] private Button backButton;

    public bool CheckReputation()
    {
        if (Reputation.GetReputation() >= reputationReq)
        {
            return true;
        }
        return false;
    }

    public void toggleButton()
    {
        backButton.gameObject.SetActive(!backButton.gameObject.activeInHierarchy);
    }
}
