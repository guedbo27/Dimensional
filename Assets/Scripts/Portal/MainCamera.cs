using UnityEngine;
using System.Collections.Generic;

public class MainCamera : MonoBehaviour {

    [HideInInspector]
    public List<Portal> portals;

    void Awake () {
        foreach(Portal portal in FindObjectsOfType<Portal>())
        {
            portals.Add(portal);
        }
    }

    void OnPreCull () {

        for (int i = 0; i < portals.Count; i++) {
            portals[i].PrePortalRender ();
        }
        for (int i = 0; i < portals.Count; i++) {
            if (portals[i].linkedPortal != null) portals[i].Render ();
        }

        for (int i = 0; i < portals.Count; i++) {
            portals[i].PostPortalRender ();
        }

    }

}