using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public enum Type
    {
        fast,
        shooter,
        big
    }

    public float life;
    public float dmg;


    Animator anim;
    public float fireRate;
    private void Awake()
    {
        //anim = GetComponent<Animator>();
    }

    void Shoot()
    {

    }

    void RecieveDamage()
    {

    }

    void Die()
    {

    }
}
