using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Patrullaje : MonoBehaviour
{
    public Transform[] puntosPatrullaje;
    private int indicePuntoActual = 0;
    public float velocidad = 5f;
    public Transform[] puntosPatrullajeRechazados;
    public bool rechazadoBool;

    void Update()
    {
        if (rechazadoBool)
        {
           
            RechazadosRoute();
        }
        else
        {
            AceptadosRoute();
        }
    }
    public void RechazadosRoute()
    {
        if (puntosPatrullajeRechazados.Length == 0)
            return;

        // Where
        puntosPatrullajeRechazados = puntosPatrullajeRechazados.Where(p => p != null).ToArray();//sidee effect

        // Select
        IEnumerable<Vector3> posiciones = puntosPatrullajeRechazados.Select(p => p.position);

        // Una mezcla rara de skip y take
        Vector3 direccion = posiciones.Skip(indicePuntoActual).First() - transform.position;
        direccion.y = 0f;

        // Si la distancia al punto de patrullaje es pequeña, pasa al siguiente punto
        if (direccion.magnitude < 0.1f)
        {
            indicePuntoActual = (indicePuntoActual + 1) % posiciones.Count();//sidee effect
        }
        else
        {
            transform.Translate(direccion.normalized * velocidad * Time.deltaTime, Space.World);//sidee effect
            transform.rotation = Quaternion.LookRotation(direccion);//sidee effect
        }
    }

    public void AceptadosRoute()
    {
        if (puntosPatrullaje.Length == 0)
            return;

        // Where
        puntosPatrullaje = puntosPatrullaje.Where(p => p != null).ToArray();//sidee effect

        // Select
        IEnumerable<Vector3> posiciones = puntosPatrullaje.Select(p => p.position);

        // Una mezcla rara de skip y take
        Vector3 direccion = posiciones.Skip(indicePuntoActual).First() - transform.position;
        direccion.y = 0f;

        // Si la distancia al punto de patrullaje es pequeña, pasa al siguiente punto
        if (direccion.magnitude < 0.1f)
        {
            indicePuntoActual = (indicePuntoActual + 1) % posiciones.Count();//sidee effect
        }
        else
        {
            transform.Translate(direccion.normalized * velocidad * Time.deltaTime, Space.World);//sidee effect
            transform.rotation = Quaternion.LookRotation(direccion);//sidee effect
        }
    }

}
