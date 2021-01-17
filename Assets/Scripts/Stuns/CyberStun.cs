using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CyberStun : MonoBehaviour
{
    public List<GameObject> image;
    float[] xPos = {-95, 95};
    float[] yPos = {-115, 100};

    private void OnEnable()
    {
        for (int i = 0; i < 7; i++)
        {
            SpawnNotice();
        }
    }

    IEnumerator StartAnim(Transform obj)
    {
        float time = 0;
        while (time < 1)
        {
            obj.localScale = Vector3.one * time;
            time += Time.deltaTime * 2;
            yield return null;
        }
    }

    IEnumerator EndAnim(Transform obj)
    {
        float time = 1;
        while (time > 0)
        {
            obj.localScale = Vector3.one * time;
            time -= Time.deltaTime * 2;
            yield return new WaitForFixedUpdate();
        }
        Destroy(obj.gameObject);
    }

    public void EliminateNotice(Transform data)
    {
       
        StartCoroutine(EndAnim(data));
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) == 99)
        {
            SpawnNotice();
        }
    }

    public void AntiVirus()
    {
        StartCoroutine(GameManager.instance.Shooting());
        gameObject.SetActive(false);
    }

    void SpawnNotice()
    {
        GameObject obj = Instantiate(image[Random.Range(0, image.Count)], transform);
        StartCoroutine(StartAnim(obj.transform));
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { EliminateNotice(obj.transform); });
        obj.transform.GetChild(0).GetComponent<EventTrigger>().triggers.Add(entry);
        obj.transform.localPosition = new Vector2(Random.Range(xPos[0], xPos[1]), Random.Range(yPos[0], yPos[1]));
    }
}
