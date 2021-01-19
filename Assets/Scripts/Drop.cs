using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public enum Type
    {
        virus = 0,
        upgrade = 1
    }
    Transform target;
    public Type type;
    public Renderer rend;
    [HideInInspector]
    public PortalManager manag;
    public Material[] mat = new Material[4];

    private void Start()
    {
        GameManager.instance.getDrops += GetToPlayer;
        target = transform.parent.GetChild(0);
        //OnlyTest
        manag = transform.parent.GetComponent<PortalManager>();
        if (type == Type.upgrade)
        {
            switch(LayerMask.LayerToName(gameObject.layer))
            {
                case "Ice":
                    rend.material = mat[0];
                    break;
                case "Fire":
                    rend.material = mat[1];
                    break;
                case "Pirate":
                    rend.material = mat[2];
                    break;
                case "Cyber":
                    rend.material = mat[3];
                    break;
            }
        }
    }
    public void GetToPlayer(LayerMask mask)
    {
        if (mask == gameObject.layer)
        {
            transform.position += ((target.position + (Vector3.up * .5f)) - transform.position) * .05f;
        }
    }

    IEnumerator OutPlayer()
    {
        while (true)
        {
            transform.position += (GameManager.instance.transform.position - transform.position) * .01f;
            yield return null;
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            SimplifiedTeleport.Teleport(transform, other.transform, other.GetComponent<Portal>().linkedPortal.transform);
            GameManager.instance.getDrops -= GetToPlayer;
            StartCoroutine(OutPlayer());
        }
        if (other.CompareTag("MainCamera")) {GameManager.instance.GetDrop(type, manag); Destroy(gameObject); }
    }
}
