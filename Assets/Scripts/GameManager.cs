using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Prefab de un portal
    public GameObject placePortal;

    //Portales ya colocados en escena para los mundos
    public Portal[] exitPortals = new Portal[4];

    public LayerMask layer;

    Weapon[] weapons = new Weapon[5];
    int selectedWeapon = 0;
    Coroutine shoot;
    Animator weaponAnim;
    Weapon.Type weaponType = Weapon.Type.normal;


    private void Start()
    {
        //Añadir las armas
        for(int  i = 1; i <= weapons.Length; i++)
        {
            Weapon _weapon = transform.GetChild(0).GetChild(i).GetComponent<Weapon>();
            _weapon.manag = this;
            weapons[(int)_weapon.type] = _weapon;
        }

        weaponAnim = transform.GetChild(0).GetChild(0).GetComponent<Animator>();

        foreach(Portal portal in exitPortals)
        {
            portal.tag = "Portal";
        }
        StartCoroutine(BeginGame());
    }
    //ColocarPortales
    IEnumerator BeginGame()
    {
        MainCamera camera = GetComponent<MainCamera>();
        Transform location = camera.transform.GetChild(1);
        int _a = 4;

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
                exitPortals[_a - 1].transform.parent.GetComponent<PortalManager>().lifeBar = _portal.transform.GetChild(2).GetChild(0).GetComponent<Image>();
                _a--;
                yield return new WaitForSeconds(2);
            }
            yield return null;


        }

        Destroy(location.gameObject);
        shoot = StartCoroutine(Shooting());
    }


    IEnumerator Shooting()
    {
        bool _shoot = false;
        while (!_shoot)
        {
            //if (Input.touchCount > 0)
            //{
            //    Touch touch = Input.GetTouch(0);
            //    if (touch.position.x > Screen.width / 2) shoot = true;
            //    //else aspiración
            //}
            if (Input.GetMouseButton(0)) _shoot = true;
            
            yield return null;
        }

        weapons[selectedWeapon].Shoot();

        yield return new WaitForSeconds(weapons[selectedWeapon].recoil);
        shoot = StartCoroutine(Shooting());
    }

    public void ChangeWeapon(Weapon.Type _type)
    {
        if (_type == weaponType) { weapons[(int)weaponType].UpdateDamage(); return; }
        weaponAnim.SetBool(_type.ToString(), true);
        weaponType = _type;
    }

    public void Stun()
    {
        //StopCoroutine(shoot);
        Debug.Log("Impact!");

        //Aqui va selección de stun
    }
}
