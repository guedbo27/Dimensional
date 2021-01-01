using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type
    {
        normal = 0,
        tele = 1,
        escopeta = 2,
        cañon = 3,
        laser = 4
    }

    [HideInInspector]
    public GameManager manag;

    [HideInInspector]
    public Type type;
    public float recoil;
    public float baseDmg;
    protected float dmg;
    public int upgradeLvl = 0;

    protected virtual void Start()
    {
        UpdateDamage();
    }

    public virtual void UpdateDamage()
    {
        upgradeLvl++;
        if (upgradeLvl == 1) { dmg = baseDmg; return; }
    }

    protected void TransferDmgToBullets(Munition mun)
    {
        mun.dmg = dmg;
    }

    public virtual void Shoot()
    {
        
    }
}
