using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWeap : Weapon
{
    public GameObject bullet;
    Transform debug;
    private void OnDrawGizmos()
    {
        if (debug != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(debug.position, debug.position + debug.forward * 25);
        }
    }

    public override void Shoot()
    {
        //GameObject _bullet = 
        Instantiate(bullet, transform.position, transform.rotation);
        Transform otherSidePoint = manag.CheckPortalRaycast();
        debug = otherSidePoint;
        if (otherSidePoint == null) return;
        //GameObject a = Instantiate(bullet, otherSidePoint.position, otherSidePoint.rotation);
        //a.layer = otherSidePoint.gameObject.layer;
        Physics.Raycast(otherSidePoint.position, otherSidePoint.forward);
        //Apply damage to enemies


    }
}
