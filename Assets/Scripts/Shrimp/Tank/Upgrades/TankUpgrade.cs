using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUpgrade : MonoBehaviour
{
    [HideInInspector] public UpgradeSO upgrade;
    protected TankController tank;
    protected bool working = true;
    public GameObject brokenParticlesPrefab;
    private GameObject brokenParticles;

    public virtual void CreateUpgrade(UpgradeSO u, TankController t)
    {
        tank = t;
        upgrade = u;
        working = true;
    }


    public virtual void UpdateUpgrade(float elapsedTime)
    {
        if ((Random.value * 100) < (upgrade.breakRate / 300) * elapsedTime)
            BreakUpgrade();
    }


    public virtual void RemoveUpgrade()
    {

    }


    public virtual void FixUpgrade()
    {
        if (working == false)
        {
            working = true;

            if (brokenParticles != null)
            {
                brokenParticles.GetComponentInChildren<ParticleSystem>().Stop();
                brokenParticles.GetComponent<DestroyAfter>().enabled = true;
                brokenParticles = null;
            }
        }
    }


    public virtual void BreakUpgrade()
    {
        if (working == true)
        {
            working = false;

            if (brokenParticlesPrefab != null)
            {
                brokenParticles = GameObject.Instantiate(brokenParticlesPrefab, transform.position, transform.rotation, tank.particleParent);
            }
        }
    }

    public bool IsBroken()
    {
        if (working) return false;
        else return true;
    }
}
