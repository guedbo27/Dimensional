using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAser : MonoBehaviour
{
    public LineRenderer lr;
    public Transform particula;
    public bool create = false;
    private GameObject toDestroy;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    void FixedUpdate()
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
    }
}
