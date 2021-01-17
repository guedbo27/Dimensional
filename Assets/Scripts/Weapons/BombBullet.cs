using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : Munition
{
    public override void Trayectory()
    {
        transform.position += transform.forward * speed;
        transform.position -= transform.up * speed * .1f;
    }
}
