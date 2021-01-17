using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateStun : MonoBehaviour
{
    float[] stunTent = new float[2];
    public Transform[] tentacles = new Transform[2];
    // Start is called before the first frame update
    private void OnEnable()
    {
        stunTent[0] = 100;
        stunTent[1] = 100;
        StartCoroutine(MoveIn());
    }

    IEnumerator MoveIn()
    {
        float time = 0;
        while(time < 1)
        {
            tentacles[0].localPosition = Vector3.Lerp(tentacles[0].localPosition, new Vector3(500, tentacles[0].localPosition.y, tentacles[0].localPosition.z), time);
            tentacles[1].localPosition = Vector3.Lerp(tentacles[1].localPosition, new Vector3(-500, tentacles[1].localPosition.y, tentacles[1].localPosition.z), time);
            time += Time.deltaTime;
            yield return null;
        }

    }

    IEnumerator MoveOut(int tent)
    {
        float time = 0;
        int count = 1 + (-2 * tent);
        while (time < 1)
        {
            tentacles[tent].localPosition = Vector3.Lerp(tentacles[tent].localPosition, new Vector3(1000 * count, tentacles[tent].localPosition.y, tentacles[tent].localPosition.z), time);
            time += Time.deltaTime;
            yield return null;
        }

        if (stunTent[1] <= 0 && stunTent[0] <= 0) ExitStun();
    }

    public void Funny(int tent)
    {
        stunTent[tent] -= 1;
        Debug.Log(stunTent[tent]);
        if (stunTent[tent] <= 0) StartCoroutine(MoveOut(tent));
    }

    void ExitStun()
    {
        StartCoroutine(GameManager.instance.Shooting());
        gameObject.SetActive(false);
    }
}
