using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplifiedTeleport : MonoBehaviour
{
    public static void Teleport(Transform obj, Transform portal, Transform portalTo)
    {
        var m = portalTo.transform.localToWorldMatrix * portal.worldToLocalMatrix * obj.localToWorldMatrix;
        obj.position = m.GetColumn(3);
        obj.rotation = m.rotation;
        //Added By Me
        obj.gameObject.layer = portalTo.gameObject.layer;
        foreach (Transform child in obj)
        {
            child.gameObject.layer = portalTo.gameObject.layer;
        }
    }
}
