﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float speed;
    LayerMask layer;

    private void Start()
    {
        Destroy(gameObject, 7);
        layer = gameObject.layer;
    }
    private void FixedUpdate()
    {
        //Route
        transform.position += transform.forward * speed;
    }

    protected void OnTriggerEnter(Collider other)
    {
        //PlayerHit
        if (other.CompareTag("MainCamera")) { GameManager.instance.Stun(layer); Destroy(gameObject); }

        if (other.CompareTag("Portal")) SimplifiedTeleport.Teleport(transform, other.transform, other.GetComponent<Portal>().linkedPortal.transform);

        if (other.CompareTag("Obstacle")) Destroy(gameObject);
    }
}
