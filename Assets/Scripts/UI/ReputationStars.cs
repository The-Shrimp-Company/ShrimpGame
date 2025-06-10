using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReputationStars : MonoBehaviour
{

    [SerializeField] private Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image.transform.localScale = new Vector3(1 - Reputation.GetReputation()/100, 1f, 1f);
    }

    public void AddRep()
    {
        Reputation.AddReputation(10);
    }
}
