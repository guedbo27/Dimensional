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
    public float lifePoints;

    //Portal portal;
    [HideInInspector]
    public Image lifeBar;

    private void Awake()
    {
        //portal = transform.GetChild(0).GetComponent<Portal>();
    }

    void SpawnEnemy(Routes route, GameObject enemy)
    {
        enemy = Instantiate(enemy, route.spawn);
        enemy.GetComponent<Animator>().runtimeAnimatorController = route.animator;
    }
}
