using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStun : MonoBehaviour
{
    public List<GameObject> images = new List<GameObject>();
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(Crack());
    }

    IEnumerator Crack()
    {
        int cracksCount = 0;
        while(true)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                images[cracksCount].SetActive(true);
                if (cracksCount >= images.Count)
                {
                    StartCoroutine(GameManager.instance.Shooting());
                    gameObject.SetActive(false);
                    foreach (GameObject image in images)
                    {
                        image.SetActive(false);
                    }
                    yield break;
                }
                else cracksCount++;
            }

            yield return null;
        }
    }
}
