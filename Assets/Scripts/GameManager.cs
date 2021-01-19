using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text text;
    public GameObject[] stuns = new GameObject[0];
    public int toWin;
    int winGot = 0;
    //Prefab de un portal
    public GameObject placePortal;

    public Transform suckGun;

    //Portales ya colocados en escena para los mundos
    public Portal[] exitPortals = new Portal[4];

    public LayerMask layer;


    Weapon[] weapons = new Weapon[5];

    Coroutine shoot;
    Coroutine suck;
    Coroutine back;
    float countDown = 3;
    public Animator weaponAnim;
    public event Action<LayerMask> getDrops;
    Weapon.Type weaponType = Weapon.Type.normal;

    float weaponUpgradeCooldown;
    float upgradeCooldown;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Añadir las armas
        for(int  i = 1; i <= weapons.Length; i++)
        {
            Weapon _weapon = transform.GetChild(0).GetChild(i - 1).GetComponent<Weapon>();
            _weapon.manag = this;
            weapons[(int)_weapon.type] = _weapon;
        }


        foreach(Portal portal in exitPortals)
        {
            portal.tag = "Portal";
        }
       
        StartCoroutine(BeginGame());
    }

    private void FixedUpdate()
    {
        weaponAnim.transform.parent.position = Vector3.Lerp(weaponAnim.transform.parent.position, transform.position + (transform.GetChild(0).position - transform.position), .8f);
        weaponAnim.transform.parent.rotation = transform.GetChild(0).rotation;
    }

    //ColocarPortales
    IEnumerator BeginGame()
    {
        MainCamera camera = FindObjectOfType<MainCamera>();
        Transform location = transform.GetChild(1);
        int _a = 4;
        while (_a > 0)
        {
            if (Input.touchCount > 0)
            {
                Portal _portal = Instantiate(placePortal, location.position, location.rotation).GetComponent<Portal>();
                //_portal.transform.Rotate(Vector3.up * -180);
                _portal.linkedPortal = exitPortals[_a - 1];
                camera.portals.Add(_portal);
                exitPortals[_a - 1].linkedPortal = _portal;
                exitPortals[_a - 1].transform.parent.GetComponent<PortalManager>().lifeBar = _portal.transform.GetChild(2).GetChild(0).GetComponent<Image>();
                _a--;
                yield return new WaitForSeconds(.5f);
            }
            yield return null;
        }

        Destroy(location.gameObject);

        foreach (Portal portal in exitPortals)
        {
            StartCoroutine(portal.transform.parent.GetComponent<PortalManager>().EnemiesSpawn());
        }

        shoot = StartCoroutine(Shooting());
    }

    RaycastHit ray = new RaycastHit();
    ParticleSystem particle;
    PortalManager portal = null;

    public IEnumerator Shooting()
    {
        
        bool _shoot = false;
        Transform previousTrans;
        RaycastHit hit;
        while (!_shoot)
        {
            
            if (Input.touchCount > 0)
            {
                if (countDown < 3 && Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2)), out hit, 20, 1 << 12))
                {
                    if (!particle.isPlaying) particle.Play();
                    countDown -= Time.deltaTime;
                    if (hit.transform.position != ray.transform.position) countDown = 3;
                    yield return null;
                    continue;
                }
                else 
                { 
                    countDown = 3;
                    if (particle != null)
                    {
                        particle.Stop();
                        particle = null;
                    }
                }

                Touch touch = Input.GetTouch(0);

                if (touch.position.x < Screen.width / 2.5f)
                {
                    suck = StartCoroutine(Sucking());
                    yield break;
                }

                if (touch.position.x < Screen.width / 1.6f)
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2)), out ray, 20, 1 << 12))
                    {
                        if (ray.transform.Find("Charge") != null)
                        {
                            portal = ray.transform.GetComponent<Portal>().linkedPortal.transform.parent.GetComponent<PortalManager>();
                            particle = ray.transform.GetChild(3).GetComponent<ParticleSystem>();
                            countDown = 1;
                        }
                    }
                }
                _shoot = true;
            }
            else
            {
                if (countDown <= 0)
                {
                    portal.anim.speed = 1;
                    Destroy(ray.transform.Find("Charge").gameObject, 1);
                }

                countDown = 3;
                if (particle != null)
                {
                    particle.Stop();
                    particle = null;
                }
            }
            
            //if (Input.GetMouseButton(0)) _shoot = true;
            //if (Input.GetMouseButton(1)) suck = StartCoroutine(Sucking());

            yield return null;
        }

        weapons[(int)weaponType].Shoot();
        if (back != null) StopCoroutine(back);
        back = StartCoroutine(BackWeapon());
        yield return new WaitForSeconds(weapons[(int)weaponType].recoil);
        shoot = StartCoroutine(Shooting());
    }

    IEnumerator BackWeapon()
    {

        weaponAnim.transform.localPosition = new Vector3(UnityEngine.Random.Range(.1f, -.1f), UnityEngine.Random.Range(.1f, -.1f), 1.6f);
        float time = 0;
        while(time < 1)
        {
            weaponAnim.transform.localPosition = Vector3.Lerp(weaponAnim.transform.localPosition, Vector3.zero, time);
            time += 0.05f;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator Sucking()
    {
        RaycastHit ray;
        RaycastHit checkRay;
        Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2)), out ray, 20, 1 << 12);
        bool isDetected = false;
        Portal portal = null;
        Touch touch;

        float time = 1;
        while (time > 0)
        {
            suckGun.localEulerAngles = Vector3.right * Mathf.Lerp(0, 85, time);
            weaponAnim.transform.localPosition = Vector3.up * Mathf.Lerp(-3, 0, time);
            time -= Time.deltaTime * 2;
            yield return null;
        }

        while (true)
        {
            if (Input.touchCount <= 0) break;
            touch = Input.GetTouch(0);
            if (touch.pressure >= 1)
            //if (Input.GetMouseButton(1))
            {
                if (!isDetected)
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2)), out ray, 20, 1 << 12))
                    {
                        isDetected = true;
                        portal = ray.transform.GetComponent<Portal>();
                    }

                }
                else
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2)), out checkRay, 20, 1 << 12))
                    {
                        text.text = "Impacting";
                        if (ray.transform != checkRay.transform)
                        {
                            isDetected = false;
                            continue;
                        }
                    }
                    else
                    {
                        isDetected = false;
                        continue;
                    }

                    try
                    {
                        getDrops?.Invoke(portal.linkedPortal.gameObject.layer);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Weird Error?\n" + e);
                    }

                }
                yield return null;
            }
        }

        while (time < 1)
        {
            suckGun.localEulerAngles = Vector3.right * Mathf.Lerp(0, 85, time);
            weaponAnim.transform.localPosition = Vector3.up * Mathf.Lerp(-3, 0, time);
            time += Time.deltaTime * 2;
            yield return null;
        }

        shoot = StartCoroutine(Shooting());
    }

    public void GetDrop(Drop.Type type, PortalManager manag)
    {
        switch(type)
        {
            case Drop.Type.virus:
                winGot++;
                if (winGot >= toWin) FinishGame();
                break;
            case Drop.Type.upgrade:
                switch(manag.gameObject.layer)
                {
                    case 8:
                        ChangeWeapon(Weapon.Type.tele);
                        break;
                    case 9:
                        ChangeWeapon(Weapon.Type.escopeta);
                        break;
                    case 10:
                        ChangeWeapon(Weapon.Type.cañon);
                        break;
                    case 11:
                        ChangeWeapon(Weapon.Type.laser);
                        break;
                }
                break;
        }
    }

    public void ChangeWeapon(Weapon.Type _type)
    {
        if (_type == weaponType) { weapons[(int)weaponType].UpdateDamage(); return; }
        else weapons[(int)weaponType].upgradeLvl = 0;
        
        weaponAnim.SetBool("normal", true);
        weaponAnim.SetBool(_type.ToString(), true);
        weapons[(int)weaponType].gameObject.SetActive(false);
        weapons[(int)_type].gameObject.SetActive(true);
        weaponType = _type;

        if (_type == Weapon.Type.normal) return;

        if (weaponUpgradeCooldown <= 0) StartCoroutine(WeaponUpgrade());
        weaponUpgradeCooldown = 30;
    }

    IEnumerator WeaponUpgrade()
    {
        weaponUpgradeCooldown = 30;
        while (weaponUpgradeCooldown > 0)
        {
            weaponUpgradeCooldown -= Time.deltaTime;
            yield return null;
        }
        ChangeWeapon(Weapon.Type.normal);
    }

    public void Stun(LayerMask layer)
    {
        if (shoot == null && suck == null) return;

        if (shoot != null) { StopCoroutine(shoot); shoot = null; }
        if (particle != null)
        {
            particle.Stop();
            particle = null;
        }
        if (suck != null) { StopCoroutine(suck); suck = null; }

        string coso = LayerMask.LayerToName(layer);
        text.text = "A" + coso + " Oh no";

        switch (coso.ToString())
        {
            case "Ice":
                text.text = LayerMask.LayerToName(layer) + "works";
                stuns[0].SetActive(true);
                break;
            case "Fire":
                StartCoroutine(OnFire());
                text.text = LayerMask.LayerToName(layer) + "works";
                break;
            case "Pirate":
                text.text = LayerMask.LayerToName(layer) + "works";
                stuns[2].SetActive(true);
                break;
            case "Cyber":
                text.text = LayerMask.LayerToName(layer) + "works";
                stuns[3].SetActive(true);
                break;
        }
    }

    IEnumerator OnFire()
    {
        stuns[1].SetActive(true);

        float shake = 10;

        while (shake > 0)
        {
            if (Input.acceleration.sqrMagnitude > 1f) 
                shake -= 1;

            shake -= Time.deltaTime;
            yield return null;
        }

        transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
        shoot = StartCoroutine(Shooting());
    }

    void FinishGame()
    {
        Debug.Log("YOU WIN YO SONNA OF A BICH");
        text.text = "You Win";
    }
}
