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
    //MAMA LUIGI
    Coroutine suck;
    Coroutine back;
    public Animator weaponAnim;
    Weapon.Type weaponType = Weapon.Type.normal;


    private void Start()
    {
        //Añadir las armas
        for(int  i = 1; i <= weapons.Length; i++)
        {
            Weapon _weapon = transform.GetChild(0).GetChild(i - 1).GetComponent<Weapon>();
            _weapon.manag = this;
            weapons[(int)_weapon.type] = _weapon;
        }

        //weaponAnim = transform.GetChild(0).GetChild(0).GetComponent<Animator>();

        foreach(Portal portal in exitPortals)
        {
            portal.tag = "Portal";
        }
        //StartCoroutine(BeginGame());
        StartCoroutine(Shooting());
    }

    private void Update()
    {
        weaponAnim.transform.parent.position = Vector3.Lerp(weaponAnim.transform.parent.position, transform.position + (transform.GetChild(0).position - transform.position), .8f);
        weaponAnim.transform.parent.rotation = transform.GetChild(0).rotation;
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
            /*
            if (Input.touchCount > 0)
            {
               Touch touch = Input.GetTouch(0);
               _shoot = true;
               if (touch.position.x < Screen.width / 2)
                {
                    suck = StartCoroutine(Sucking(touch));
                    yield break;
                }
            }
            */
            if (Input.GetMouseButton(0)) _shoot = true;
            
            yield return null;
        }

        weapons[selectedWeapon].Shoot();
        if (back != null) StopCoroutine(back);
        back = StartCoroutine(BackWeapon());
        yield return new WaitForSeconds(weapons[selectedWeapon].recoil);
        shoot = StartCoroutine(Shooting());
    }

    IEnumerator BackWeapon()
    {

        weaponAnim.transform.localPosition = new Vector3(Random.Range(.1f, -.1f), Random.Range(.1f, -.1f), 1.6f);
        float time = 0;
        while(time < 1)
        {
            weaponAnim.transform.localPosition = Vector3.Lerp(weaponAnim.transform.localPosition, Vector3.zero, time);
            time += 0.05f;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator Sucking(Touch touch)
    {
        while(touch.pressure >= 1)
        {
            SuckDrops();
            yield return null;
        }

        shoot = StartCoroutine(Shooting());
    }

    void SuckDrops() 
    {

    }

    public void ChangeWeapon(Weapon.Type _type)
    {
        if (_type == weaponType) { weapons[(int)weaponType].UpdateDamage(); return; }
        weaponAnim.SetBool(_type.ToString(), true);
        weaponType = _type;
    }

    public void Stun()
    {
        if (shoot != null) StopCoroutine(shoot);
        if (suck != null) StopCoroutine(suck);

        Debug.Log("Impact!");

        //Aqui va selección de stun
    }
}
