using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
CRAWLING IN MY SKIN
THESE WOUNDS, THEY WILL NOT HEAL
FEAR IS HOW I FALL
CONFUSING WHAT IS REAL
THERE'S SOMETHING INSIDE ME THAT PULLS BENEATH THE SURFACE
CONSUMING, CONFUSING
THIS LACK OF SELF CONTROL I FEAR IS NEVER ENDING
CONTROLLING
I CAN'T SEEM
TO FIND MYSELF AGAIN
MY WALLS ARE CLOSING IN
(WITHOUT A SENSE OF CONFIDENCE, I'M CONVINCED)
(THAT THERE'S JUST TOO MUCH PRESSURE TO TAKE)
I'VE FELT THIS WAY BEFORE
SO INSECURE
CRAWLING IN MY SKIN
THESE WOUNDS, THEY WILL NOT HEAL
FEAR IS HOW I FALL
CONFUSING WHAT IS REAL
DISCOMFORT, ENDLESSLY HAS PULLED ITSELF UPON ME
DISTRACTING, REACTING
AGAINST MY WILL I STAND BESIDE MY OWN REFLECTION
IT'S HAUNTING
HOW I CAN'T SEEM
TO FIND MYSELF AGAIN
MY WALLS ARE CLOSING IN
(WITHOUT A SENSE OF CONFIDENCE, I'M CONVINCED)
(THAT THERE'S JUST TOO MUCH PRESSURE TO TAKE)
I'VE FELT THIS WAY BEFORE
SO INSECURE
CRAWLING IN MY SKIN
THESE WOUNDS, THEY WILL NOT HEAL
FEAR IS HOW I FALL
CONFUSING WHAT IS REAL
CRAWLING IN MY SKIN
THESE WOUNDS, THEY WILL NOT HEAL
FEAR IS HOW I FALL
CONFUSING, CONFUSING WHAT IS REAL
THERE'S SOMETHING INSIDE ME THAT PULLS BENEATH THE SURFACE
CONSUMING (CONFUSING WHAT IS REAL)
THIS LACK OF SELF CONTROL I FEAR IS NEVER ENDING
CONTROLLING (CONFUSING WHAT IS REAL)
*/
public class Laser : MonoBehaviour
{
    public LineRenderer lr;
    public Transform particula;
    public static float dmg = .1f;

    Transform reference = null;
    Transform comparison = null;
    GameObject otherRay;

    void Start()
    {
        lr = GetComponent<LineRenderer>();

        if (gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            particula.gameObject.SetActive(false);
            StartCoroutine(DefaultRay());
        }
        else StartCoroutine(AttackRay());
    }

    private void Update()
    {
        lr.material.mainTextureOffset = new Vector2(-Time.time, 0);
    }

    IEnumerator DefaultRay()
    {
        RaycastHit hit;
        while (true)
        {

            lr.SetPosition(0, transform.position);
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1 << 12))
            {
                if (otherRay != null)
                {
                     reference.transform.SetPositionAndRotation(hit.point, transform.rotation);
                     otherRay.transform.localPosition = reference.transform.localPosition;
                     otherRay.transform.localRotation = reference.transform.localRotation;
                     if (hit.transform.position != comparison.position || !hit.collider)
                     {
                            Destroy(otherRay);
                            otherRay = null;
                     }
                }
                if (hit.collider)
                {
                    if (otherRay == null)
                    {
                        reference = new GameObject().transform;
                        reference.transform.SetPositionAndRotation(hit.point, transform.rotation);
                        reference.transform.parent = hit.transform;

                        Transform otherPortal = hit.transform.GetComponent<Portal>().linkedPortal.transform;

                        otherRay = Instantiate(gameObject, otherPortal);
                        otherRay.transform.localPosition = reference.transform.localPosition;
                        otherRay.transform.localRotation = reference.transform.localRotation;
                        otherRay.layer = otherPortal.gameObject.layer;

                        comparison = hit.transform;
                    }

                    lr.SetPosition(1, hit.point + (transform.forward * 10));
                }
            }
            else
            {
                if (otherRay != null)
                {
                    Destroy(otherRay);
                    otherRay = null;
                }
                lr.SetPosition(1, transform.forward * 250);
            }
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator AttackRay()
    {
        RaycastHit hit;
        while (true)
        {
            lr.SetPosition(0, transform.position - (transform.forward * 10));
            if (Physics.Raycast(transform.position, transform.forward, out hit, transform.gameObject.layer))
            {
                if (hit.collider)
                {
                    lr.SetPosition(1, hit.point);
                    if (hit.transform.CompareTag("Enemy"))
                    {
                        hit.transform.GetComponent<Enemies>().RecieveDamage(dmg);
                    }
                }
            }
            else
            {
                lr.SetPosition(1, transform.position + transform.forward * 250);
            }

            particula.position = lr.GetPosition(1) - transform.forward * 0.3f;
            lr.SetPosition(1, transform.position + transform.forward * 250);

            yield return new WaitForFixedUpdate();
        }
    }

    private void OnDisable()
    {
        if (otherRay != null) Destroy(otherRay);
    }

    private void OnDestroy()
    {
        if (otherRay != null) Destroy(otherRay);
        if (reference != null) Destroy(reference.gameObject);
    }
}
