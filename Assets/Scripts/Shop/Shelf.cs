using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    private Transform[] _tanks;

    // Start is called before the first frame update
    void Start()
    {


       
    }

    private void Awake()
    {
        
    }


    private void OnDisable()
    {
        _tanks = GetComponentsInChildren<Transform>();



        _tanks = _tanks.Where(x => x.name.Contains("TankSocket")).ToArray();

        foreach (Transform tank in _tanks)
        {
            tank.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        
    }

    public GameObject AddTank()
    {
        foreach(Transform tank in _tanks)
        {
            if (!tank.gameObject.activeSelf)
            {
                tank.gameObject.SetActive(true);
                return tank.gameObject;
            }
        }
        return null;
    }

}
