using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class Patrullaje : MonoBehaviour
{
    public Transform[] puntosPatrullaje;
    private int indicePuntoActual = 0;
    public float velocidad = 5f;
    public Transform[] puntosPatrullajeRechazados;
    public Transform[] BancoPatrullaje;
    public Transform[] ParquePatrullaje;
    public bool rechazadoMacBool;
    public bool rechazadoBankBool;
    public Characters characters;
    public List<CreditCards> creditCardsList;
    public bool IGoToThePark;
    public TextMeshPro textOfMoney;

    private void Awake()
    {
        characters = GetComponent<Characters>();
        textOfMoney = GetComponentInChildren<TextMeshPro>();
    }
    /* acá, dependiendo de la plata, van al mcdonalds si tienen 0 pesos se van al banco y obtendrán un random de plata, si les alcanza al recital
     * van. sino al mcdonalds*/
    private void Start()
    {
        if (characters.WantsToAttendConcert == false)
        {
            IGoToThePark = true;
        }
        StartCoroutine(EsperarYExecutar());

    }

    void Update()
    {
        if (IGoToThePark)
        {
            ParqueRoute();
        }
        else
        {
            MovementDecsision();

        }
    }
    public void ParqueRoute()
    {

        if (ParquePatrullaje.Length == 0)
            return;

        // Copia para evitar los side effects, trabajas con las copias y listo, no modificas otras cosas
        Transform[] puntosRechazadosOriginales = (Transform[])ParquePatrullaje.Clone();

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
    public void MovementDecsision()
    {
        if (rechazadoMacBool)
        {
            RechazadosRoute();

            /*if (characters.iHaveMoney != 0)
            {
            }
            else
            {
                BankMovement();
                //GettingMoneyInBank();
                //StartCoroutine(EsperarYExecutar());
            }*/

        }
        else if (rechazadoBankBool)
        {
            BankMovement();
        }
        else
        {
            AceptadosRoute();
        }
    }
    IEnumerator EsperarYExecutar()
    {
        // Esperar 10 segundos
        yield return new WaitForSeconds(5);
        //ejecutar en banco
        if (characters.iHaveMoney >= 10)
        {
            rechazadoBankBool = false;
            rechazadoMacBool = false;
        }
        if (rechazadoBankBool && characters.iHaveMoney < 10)
        {
            characters.iHaveMoney += ExtractMoney();//EJECUTAR FUNCION DE TARJETA DE CREDITO  ACÁ
            Debug.Log($"se ha conseguido{characters.iHaveMoney} pesos en el banco");
            StartCoroutine(EsperarYExecutar());

        }

        textOfMoney.text = $"dinero: ${characters.iHaveMoney}";
    }
    public int ExtractMoney()
    {
        CreditCards creditCards = creditCardsList.OfType<Premium>().First();
        if (creditCards == null)
        {
            CreditCards creditCardsTwo = creditCardsList.OfType<Basic>().First();
            return creditCardsTwo.GetCreditLimit();
        }
        return creditCards.GetCreditLimit();
    }
    public void GettingMoneyInBank()
    {
        StartCoroutine(EsperarYExecutar());
    }

    public void BankMovement()
    {
        //Debug.Log("Recorrido al banco");

        if (BancoPatrullaje.Length == 0)
            return;

        // Copia para evitar los side effects, trabajas con las copias y listo, no modificas otras cosas
        Transform[] puntosRechazadosOriginales = (Transform[])BancoPatrullaje.Clone();

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
