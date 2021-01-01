using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleBullet : Munition
{
    Transform enemyDetected = null;
    private void OnDrawGizmos()
    {
        if (enemyDetected == null) Gizmos.color = new Color(1, 1, 1, .5f);
        else Gizmos.color = new Color(0, 1, 0, .5f);
        Gizmos.DrawSphere(transform.position, 5);
    }

    public override void Trayectory()
    {
        if(enemyDetected == null)
        {
            Collider[] coll = Physics.OverlapSphere(transform.position, 5, gameObject.layer);
            foreach (Collider col in coll)
            {
                if (col.CompareTag("Enemy"))
                {
                    enemyDetected = col.transform;
                    break;
                }
            }
            transform.position += transform.forward * speed;
            return;
        }

        Vector3 direction = enemyDetected.position - transform.position;
        direction.Normalize();
        Quaternion _lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, .1f);

        transform.position += transform.forward * speed;
    }
}
