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

    public Type type;
    public float recoil;
    public float baseDmg;
    float dmg;
    public int upgradeLvl = 1;

    public virtual void Shoot()
    {
        
    }
}
