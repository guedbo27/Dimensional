using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minijuego_01 : MonoBehaviour
{
    Material material;
    public int Distancia = 10;
    public GameObject Player;

    private void OnCollisionEnter(Collision collision)
    {
        material.SetVector("_PlayerPos", Player.transform.position);
        material.SetFloat("_Distance", Distancia);
    }
}
