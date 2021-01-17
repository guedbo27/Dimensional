using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : Weapon
{
    bool activated = false;
    public GameObject laser;
    GameObject instantLaser;
    private void Update()
    {
        if (activated && Input.touchCount < 1)
        {
            Destroy(instantLaser);
            activated = false;
        }
    }

    public override void Shoot()
    {
        if (activated) return;
        activated = true;
        instantLaser = Instantiate(laser, transform);
    }
}
