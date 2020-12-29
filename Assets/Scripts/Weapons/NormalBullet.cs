using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Munition
{
    public override void Trayectory()
    {
        transform.position += transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Portal")) SimplifiedTeleport.Teleport(transform, other.transform, other.GetComponent<Portal>().linkedPortal.transform);
    }
}
