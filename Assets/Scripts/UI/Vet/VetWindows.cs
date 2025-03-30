using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VetWindows : MonoBehaviour
{
    [SerializeField]
    private int reputationReq = 0;

    public bool CheckReputation()
    {
        if (Reputation.GetReputation() >= reputationReq)
        {
            return true;
        }
        return false;
    }
}
