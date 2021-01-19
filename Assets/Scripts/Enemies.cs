using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public enum Type
    {
        normal = 0,
        shooter = 1,
        big = 2
    }

    public float life;
    public float dmg;
    //[HideInInspector]
    public Transform[] point;
    public Transform shootPoint;
    public GameObject shootBullet;
    public GameObject[] drops;

    PortalManager manag;
    Animator anim;
    public float fireRate;

    private void Start()
    {
        anim = GetComponent<Animator>();
        manag = transform.parent.parent.GetComponent<PortalManager>();
        manag.killAll += Die;
        gameObject.tag = "Enemy";
        StartCoroutine(RotateToCamera());
    }

    IEnumerator RotateToCamera()
    {
        Vector3 Target = Vector3.zero;
        Vector3 temp;
        Vector3 temp2;
        float counting = fireRate + Random.Range(1, 5);

        while(Target == Vector3.zero)
        {
            counting -= Time.deltaTime;
            if (counting <= 0)
            {
                temp = Vector3.Lerp(point[0].position, point[1].position, Random.Range(0f, 1f));
                temp2 = Vector3.Lerp(point[2].position, point[3].position, Random.Range(0f, 1f));
                Target = Vector3.Lerp(temp, temp2, Random.Range(0f, 1f));
                if (Vector3.Distance(transform.position, Target) < 10)
                {
                    yield break;
                }
                
            }
            yield return null;
        }
        
        Vector3 _direction;
        Quaternion _lookRotation;

        Transform rotator = transform.GetChild(0);
        float time = 0;
        //Girar al objectivo
        while(time < 1)
        {
            //find the vector pointing from our position to the target
            _direction = (Target - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            _lookRotation = Quaternion.LookRotation(_direction) * Quaternion.Euler(0, -90, 0);

            //rotate us over time according to speed until we are in the required rotation
            rotator.rotation = Quaternion.Slerp(rotator.rotation, _lookRotation, time);
            time += .01f;
            yield return null;
        }

        anim.Play("Shoot", -1, 0);

        //Esperar a que la anim termine
        float time2 = 0;
        while (anim.GetCurrentAnimatorStateInfo(1).length > time2)
        {
            //find the vector pointing from our position to the target
            _direction = (Target - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            _lookRotation = Quaternion.LookRotation(_direction) * Quaternion.Euler(0, -90, 0);

            //rotate us over time according to speed until we are in the required rotation
            rotator.rotation = Quaternion.Slerp(rotator.rotation, _lookRotation, time);

            _direction = (Target - shootPoint.position).normalized;

            shootPoint.rotation = Quaternion.LookRotation(_direction);

            time2 += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        //Volver a estado normal
        while (time > 0)
        {
            //find the vector pointing from our position to the target
            _direction = transform.forward;

            //create the rotation we need to be in to look at the target
            _lookRotation = Quaternion.LookRotation(_direction);

            //rotate us over time according to speed until we are in the required rotation
            rotator.rotation = Quaternion.Slerp(_lookRotation, rotator.rotation, time);
            time -= .02f;
            yield return null;
        }

        yield return new WaitForSeconds(2);
        StartCoroutine(RotateToCamera());
    }

    public void Shoot()
    {
        Instantiate(shootBullet, shootPoint.position, shootPoint.rotation);
    }

    public void RecieveDamage(float dmg)
    {
        life -= dmg;
        if (life <= 0) Die();
        else anim.Play("Damage", -1, 0);
    }

    public void DamagePortal()
    {
        manag.DamagePortal(dmg);
    }

    public void Die()
    {
        GetComponent<Collider>().enabled = false;
        StopAllCoroutines();
        anim.SetBool("Stop", true);
        Instantiate(drops[0], transform.position + new Vector3(Random.Range(1f, 3f), Random.Range(.1f, .3f), 0), Quaternion.identity,transform.parent);
        anim.SetFloat("Speed", 0);
        if (Random.Range(1, 5) == 3) Instantiate(drops[1], transform.position + new Vector3(Random.Range(1f, 3f), Random.Range(.1f, .3f), 0), Quaternion.identity, transform.parent);
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(1).length + 3);
    }

}
