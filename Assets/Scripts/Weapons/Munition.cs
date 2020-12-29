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
        Destroy(gameObject, 10);
    }
    private void FixedUpdate()
    {
        Trayectory();
    }

    public virtual void Trayectory()
    {

    }
}
