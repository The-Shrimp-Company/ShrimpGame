using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] float destroyTime;

    void Awake()
    {
        Destroy(gameObject, destroyTime);
    }
}
