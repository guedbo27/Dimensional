using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Munition
{
    public override void Trayectory()
    {
        transform.position += transform.forward * speed;
    }
}
