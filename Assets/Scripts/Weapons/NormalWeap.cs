using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWeap : Weapon
{
    public GameObject bullet;

    protected override void Start()
    {
        base.Start();
        type = Type.normal;
    }

    protected override void UpdateDamage()
    {
        base.UpdateDamage();
        //Actualizar Daño Del Arma
    }

    public override void Shoot()
    {
        Munition mun = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Munition>();
        TransferDmgToBullets(mun);
    }
}
