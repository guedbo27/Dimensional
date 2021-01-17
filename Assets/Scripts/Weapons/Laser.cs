using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lr;
    public Transform particula;
    public bool create = false;
    public float dmg;

    Transform comparison;

    void Start()
    {
        lr = GetComponent<LineRenderer>();

        if (gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            particula.gameObject.SetActive(false);
            StartCoroutine(DefaultRay());
        }
        else StartCoroutine(AttackRay());
    }

    IEnumerator DefaultRay()
    {
        while (true)
        {
            lr.SetPosition(0, transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1 << 12))
            {
                if (hit.transform.position != comparison.position)
                {

                }

                if (hit.collider)
                {
                    lr.SetPosition(1, hit.point);
                }
            }
            else
            {
                lr.SetPosition(1, transform.forward * 250);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator AttackRay()
    {
        while (true)
        {
            lr.SetPosition(0, transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider)
                {
                    lr.SetPosition(1, hit.point);
                }
            }
            else
            {
                lr.SetPosition(1, transform.forward * 250);
            }

            particula.position = lr.GetPosition(1) - transform.forward * 0.3f;
            yield return new WaitForFixedUpdate();
        }
    }
}
