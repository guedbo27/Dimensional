using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalManager : MonoBehaviour
{
    [System.Serializable]
    public struct Routes
    {
        public Transform spawn;
        public AnimatorOverrideController animator;
    }

    public GameObject[] enemies = new GameObject[3];
    public List<Routes> spawnPoints;
    public Transform[] shootPoints = new Transform[4];
    public float spawnRate;
    float lifePoints = 100;

    //Portal portal;
    [HideInInspector]
    public Image lifeBar;

    void SpawnEnemy(Routes route, GameObject enemy)
    {
        enemy = Instantiate(enemy, route.spawn);
        enemy.GetComponent<Animator>().runtimeAnimatorController = route.animator;
        GetComponent<Enemies>().point = shootPoints;

    }

    public void DamagePortal(float dmg)
    {
        lifePoints -= dmg;
        if (lifePoints <= 0) DestroyPortal();
        lifeBar.fillAmount = lifePoints / 100;

    }

    void DestroyPortal()
    {
        Debug.Log("PortalDestroyed");
    }
}
