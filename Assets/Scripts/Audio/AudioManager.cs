using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource buttonSource;
    private AudioSource buttonClick;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        
    }

    public void GetButtonClick()
    {
        if(buttonClick == null)
        {
            buttonClick = Instantiate(buttonSource).GetComponent<AudioSource>();
        }
        buttonClick.Play();
    }
}
