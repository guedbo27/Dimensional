﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalManager : MonoBehaviour
{
    [Serializable]
    public struct Routes
    {
        public Transform spawn;
        public AnimatorOverrideController animator;
    }

    public GameObject[] enemies = new GameObject[3];
    public List<Routes> spawnPoints;
    public Transform[] shootPoints = new Transform[4];
    public event Action killAll;
    public float spawnRate;
    float lifePoints = 100;
    Animator anim;
    //Portal portal;
    [HideInInspector]
    public Image lifeBar;

    void SpawnEnemy(Routes route, GameObject enemy)
    {
        enemy = Instantiate(enemy, route.spawn);
        enemy.GetComponent<Animator>().runtimeAnimatorController = route.animator;
        GetComponent<Enemies>().point = shootPoints;
        anim = GetComponent<Animator>();
        anim.speed = 0;
    }

    public void InvokeUltimate()
    {
        anim.speed = 1;
    }

    public void DamagePortal(float dmg)
    {
        try
        {
            lifePoints -= dmg;
            if (lifePoints <= 0) DestroyPortal();
            lifeBar.fillAmount = lifePoints / 100;
        }
        catch (Exception e)
        {

        }

    }

    public void Ultimate()
    {
        killAll();
    }

    void DestroyPortal()
    {
        Debug.Log("PortalDestroyed");
    }
}
