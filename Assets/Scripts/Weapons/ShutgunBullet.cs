using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutgunBullet : NormalBullet
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) { Impact(other.GetComponent<Enemies>());}
        if (other.gameObject.layer == LayerMask.NameToLayer("Portal")) SimplifiedTeleport.Teleport(transform, other.transform, other.GetComponent<Portal>().linkedPortal.transform);
        if (other.CompareTag("Obstacle")) Destroy(gameObject);
    }
}
