using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public enum Type
    {
        fast,
        shooter,
        big
    }

    public float life;
    public float dmg;
    public Transform point;
    Transform shootPoint;
    public GameObject shootBullet;


    Animator anim;
    public float fireRate;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        shootPoint = transform.Find("ShootPoint");
        gameObject.tag = "Enemy";
    }

    private void Start()
    {
        StartCoroutine(RotateToCamera(point.position));
    }

    IEnumerator RotateToCamera(Vector3 Target)
    {
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
            _lookRotation = Quaternion.LookRotation(_direction);

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
            _lookRotation = Quaternion.LookRotation(_direction);

            //rotate us over time according to speed until we are in the required rotation
            rotator.rotation = Quaternion.Slerp(rotator.rotation, _lookRotation, time);
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

    public void Die()
    {
        anim.SetBool("Stop", true);
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(1).length + 3);
    }
}
