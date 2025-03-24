using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameControls : MonoBehaviour
{

    public static StartGameControls instance;

    public bool newGame;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
