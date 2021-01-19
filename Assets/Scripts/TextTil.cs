using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTil : MonoBehaviour
{
    Renderer render;
    void Start()
    {
        render = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        render.material.mainTextureOffset = new Vector2(0, Time.time * .02f);
    }
}
