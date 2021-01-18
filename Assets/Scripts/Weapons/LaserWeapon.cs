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
        if (activated && (Input.touchCount < 1 || Input.GetTouch(0).position.x < Screen.width / 1.6f))
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
        Laser.dmg = dmg;
    }
}
