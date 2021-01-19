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
            if (Input.touchCount > 0)
            {
                yield return new WaitForSeconds(.5f);
                if (cracksCount >= images.Count)
                {
                    GameManager.instance.StartCoroutine(GameManager.instance.Shooting());
                    gameObject.SetActive(false);
                    foreach (GameObject image in images)
                    {
                        image.SetActive(false);
                    }
                    yield break;
                }
                else
                {
                    images[cracksCount].SetActive(true);
                    cracksCount++;
                }
            }

            yield return null;
        }
    }
}
