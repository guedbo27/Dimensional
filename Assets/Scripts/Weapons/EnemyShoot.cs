using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float speed;

    //como enviamos lo de bloquear?

    private void Start()
    {
        Destroy(gameObject, 7);
    }
    private void FixedUpdate()
    {
        //Route
        transform.position += transform.forward * speed;
    }

    protected void OnTriggerEnter(Collider other)
    {
        //PlayerHit
        if (other.CompareTag("MainCamera")) { other.GetComponent<GameManager>().Stun(); Destroy(gameObject); }

        //if (other.CompareTag("Portal")) SimplifiedTeleport.Teleport(transform, other.transform, other.GetComponent<Portal>().linkedPortal.transform);

        if (other.CompareTag("Obstacle")) Destroy(gameObject);
    }
}
