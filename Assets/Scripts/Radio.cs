using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Interactable
{
    [SerializeField] private DiageticScreen screen;

    private bool play = true;

    public List<AudioClip> clips;

    private Queue<AudioClip> playList = new Queue<AudioClip>();

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        foreach(AudioClip clip in clips)
        {
            playList.Enqueue(clip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MouseHover();
        if(!source.isPlaying && play)
        {
            AudioClip toPlay = playList.Dequeue();
            playList.Enqueue(toPlay);
            source.clip = toPlay;
            source.Play();
        }
    }

    

    public override void Action()
    {
        if (source.isPlaying)
        {
            source.Pause();
            play = false;
        }
        else
        {
            source.UnPause();
            play = true;
        }
    }

    public override void OnHover()
    {
        //screen.gameObject.SetActive(true);
    }

    
}
