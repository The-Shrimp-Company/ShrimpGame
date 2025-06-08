using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// Root screen view class. Used by all of the different screens the player can
/// open, from tank view to tablet apps.
/// </summary>
public class ScreenView : MonoBehaviour
{


    [SerializeField]
    protected ShelfSpawn shelves;

    protected GameObject player;

    public bool locksPlayerMovement = false;


    protected virtual void Start()
    {
        player = GameObject.Find("Player");
    }


    public virtual void Open(bool switchTab)
    {

    }

    public virtual void Close(bool switchTab)
    {
        Destroy(gameObject);
    }

    public virtual void Close()
    {
        Destroy(gameObject);
    }

    public virtual void Exit()
    {
        UIManager.instance.CloseScreen();
    }
}
