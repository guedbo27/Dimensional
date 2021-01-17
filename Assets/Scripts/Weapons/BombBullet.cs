using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : Munition
{
    public GameObject explosion;
    bool isQuit = false;
    public override void Trayectory()
    {
        transform.position += transform.forward * speed;
        transform.position -= transform.up * speed * .1f;
    }

    private void OnApplicationQuit()
    {
        isQuit = true;
    }

    private void OnDestroy()
    {
        if (isQuit) return;
        Instantiate(explosion, transform.position, Quaternion.identity);
        Collider[] coll = Physics.OverlapSphere(transform.position, 5, gameObject.layer);

        foreach (Collider col in coll)
        {
            if (col.CompareTag("Enemy")) Impact(col.GetComponent<Enemies>());
        }
    }
}
