using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Prefab de un portal
    public GameObject placePortal;

    //Portales ya colocados en escena para los mundos
    public Portal[] exitPortals = new Portal[4];

    private void Start()
    {
        StartCoroutine(BeginGame());
    }
    //ColocarPortales
    IEnumerator BeginGame()
    {
        MainCamera camera = FindObjectOfType<MainCamera>();
        Transform location = camera.transform.GetChild(1);
        int _a = 4;
        //Input for compTesting
        while (_a > 0)
        {
            if (Input.touchCount > 0)
            //if (Input.GetMouseButtonDown(0))
            {
                Portal _portal = Instantiate(placePortal, location.position, location.rotation).GetComponent<Portal>();
                _portal.transform.Rotate(Vector3.right * 90);
                _portal.linkedPortal = exitPortals[_a - 1];
                camera.portals.Add(_portal);
                exitPortals[_a - 1].linkedPortal = _portal;
                _a--;
                yield return new WaitForSeconds(2);
            }
            yield return null;
        }

        Destroy(location.gameObject);
        
    }
}
