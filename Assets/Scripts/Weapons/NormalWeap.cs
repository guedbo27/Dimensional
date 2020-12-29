using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWeap : Weapon
{
    public GameObject bullet;

    public override void Shoot()
    {
        //GameObject _bullet = 
        Instantiate(bullet, transform.position, transform.rotation);
    //    RaycastHit hit;
    //    if (!Physics.Raycast(transform.position, transform.forward, out hit, LayerMask.NameToLayer("Portal"))) return;

    //    Transform otherSidePoint = hit.transform.GetComponent<Portal>().linkedPortal.transform.GetChild(0);
    //    debug = otherSidePoint;

    //    //Calculate vector of distance between two
    //    Vector3 _position = otherSidePoint.position + (otherSidePoint.position - playerCenter.position);
    //    //Quaternion _rotation = transform.rotation + otherSidePoint.rotation;

    //    //GameObject a = Instantiate(bullet, otherSidePoint.position, otherSidePoint.rotation);
    //    //a.layer = otherSidePoint.gameObject.layer;
    //    Physics.Raycast(otherSidePoint.position, otherSidePoint.forward);
    //    //Apply damage to enemies
    }
}
