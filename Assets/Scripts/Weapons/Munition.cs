using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munition : MonoBehaviour
{
    public float speed;
    [HideInInspector]
    public float dmg;
    private void Start()
    {
        Destroy(gameObject, 7);
    }
    private void FixedUpdate()
    {
        Trayectory();
    }

    public virtual void Trayectory()
    {

    }

    public virtual void Impact(Enemies enemy)
    {
        enemy.RecieveDamage(dmg);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) { Impact(other.GetComponent<Enemies>()); Destroy(gameObject); }
        if (other.gameObject.layer == LayerMask.NameToLayer("Portal")) SimplifiedTeleport.Teleport(transform, other.transform, other.GetComponent<Portal>().linkedPortal.transform);
        if (other.CompareTag("Obstacle")) Destroy(gameObject);
    }
}
