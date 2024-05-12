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

        // Copia para evitar los side effects, trabajas con las copias y listo, no modificas otras cosas
        Transform[] puntosRechazadosOriginales = (Transform[])puntosPatrullajeRechazados.Clone();

        // Where
        IEnumerable<Transform> puntosValidos = puntosRechazadosOriginales.Where(p => p != null);

        // Select
        IEnumerable<Vector3> posiciones = puntosValidos.Select(p => p.position);

        // Una mezcla rara de skip y take
        Vector3 direccion = posiciones.Skip(indicePuntoActual).First() - transform.position;
        direccion.y = 0f;

        // logica para el siguiente punto
        if (direccion.magnitude < 0.1f)
        {
            indicePuntoActual = (indicePuntoActual + 1) % posiciones.Count();
        }
        else
        {
            transform.Translate(direccion.normalized * velocidad * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.LookRotation(direccion);
        }
    }

    public void AceptadosRoute()
    {
        if (puntosPatrullaje.Length == 0)
            return;

        // Copia para evitar los side effects, trabajas con las copias y listo, no modificas otras cosas
        Transform[] puntosAceptadosOriginales = (Transform[])puntosPatrullaje.Clone();

        // Where
        IEnumerable<Transform> puntosValidos = puntosAceptadosOriginales.Where(p => p != null);

        // Select
        IEnumerable<Vector3> posiciones = puntosValidos.Select(p => p.position);

        // Una mezcla rara de skip y take
        Vector3 direccion = posiciones.Skip(indicePuntoActual).First() - transform.position;
        direccion.y = 0f;

        // logica para el siguiente punto
        if (direccion.magnitude < 0.1f)
        {
            indicePuntoActual = (indicePuntoActual + 1) % posiciones.Count();
        }
        else
        {
            transform.Translate(direccion.normalized * velocidad * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.LookRotation(direccion);
        }
    }
}
