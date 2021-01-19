using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalManager : MonoBehaviour
{
    [Serializable]
    public struct Routes
    {
        public Enemies.Type enemyType;
        public Transform spawn;
        public AnimatorOverrideController animator;
    }

    public GameObject[] enemies = new GameObject[3];
    public List<Routes> spawnPoints;
    public Transform[] shootPoints = new Transform[4];
    public event Action killAll;
    public float spawnRate;
    float lifePoints = 100;
    public Animator anim;
    //Portal portal;
    [HideInInspector]
    public Image lifeBar;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0;
    }

    void SpawnEnemy(Routes route)
    {
        if (route.spawn.childCount > 0) return;
        GameObject enemy = Instantiate(enemies[(int)route.enemyType], route.spawn);
        enemy.GetComponent<Animator>().runtimeAnimatorController = route.animator;
        enemy.GetComponent<Enemies>().point = shootPoints;
    }

    public IEnumerator EnemiesSpawn()
    {
        float spawnRate;
        while (true)
        {
            spawnRate = UnityEngine.Random.Range(4, 22);
            while(spawnRate > 0)
            {
                spawnRate -= Time.deltaTime;
                yield return null;
            }

            SpawnEnemy(spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)]);
        }
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
        killAll.Invoke();
    }

    void DestroyPortal()
    {
        Debug.Log("PortalDestroyed");
    }
}
