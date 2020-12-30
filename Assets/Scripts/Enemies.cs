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
        while(time < 1)
        {
            //find the vector pointing from our position to the target
            _direction = (Target - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            _lookRotation = Quaternion.LookRotation(_direction);

            //rotate us over time according to speed until we are in the required rotation
            rotator.rotation = Quaternion.Slerp(rotator.rotation, _lookRotation, time);
            time += .02f;
            yield return null;
        }

        anim.Play("Shoot", -1);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(1).length);

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
        yield return new WaitForSeconds(3);
        StartCoroutine(RotateToCamera(point.position));
    }

    public void Shoot()
    {

    }

    public void RecieveDamage(float dmg)
    {
        life -= dmg;
    }

    void Die()
    {

    }
}
