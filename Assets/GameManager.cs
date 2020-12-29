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

    public LayerMask layer;

    Weapon[] weapons = new Weapon[5];
    int selectedWeapon = 0;


    private void Start()
    {
        //Añadir las armas
        for(int  i = 1; i <= weapons.Length; i++)
        {
            Weapon _weapon = transform.GetChild(0).GetChild(i).GetComponent<Weapon>();
            _weapon.manag = this;
            weapons[(int)_weapon.type] = _weapon;
        }

        StartCoroutine(BeginGame());
    }
    //ColocarPortales
    IEnumerator BeginGame()
    {
        MainCamera camera = GetComponent<MainCamera>();
        Transform location = camera.transform.GetChild(1);
        int _a = 4;
        //Input for compTesting
        while (_a > 0)
        {
            //if (Input.touchCount > 0)
            if (Input.GetMouseButtonDown(0))
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
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        //Touch touch = Input.GetTouch(0);
        //if (touch.position.x > Screen.width/2)
        while(!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        weapons[selectedWeapon].Shoot();

        yield return new WaitForSeconds(weapons[selectedWeapon].recoil);
        StartCoroutine(Shooting());
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, transform.position + transform.forward * 25);
    //}

    //public Transform CheckPortalRaycast()
    //{
    //    RaycastHit hit;
    //    Ray ray = new Ray(transform.position, transform.forward);
    //    if (Physics.Raycast(ray, out hit, 25, layer))
    //    {
    //        return hit.transform.GetComponent<Portal>().linkedPortal.transform.GetChild(0);
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}
}
