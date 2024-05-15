using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class Characters : MonoBehaviour
{
    public string CharacterName;
    public string CharacterLastName;
    public int Age;
    private Color adultColor = Color.red;
    private Color minorColor = Color.green;
    public int iHaveMoney;
    public bool WantsToAttendConcert;
    public Patrullaje Patrullaje;
    public GameObject vipGameObject;
    public GameObject old;

    private Characters[] charactersInScene;
    private float averageAge;

    private void Awake()
    {
        Patrullaje = GetComponent<Patrullaje>();
    }

    void Start()
    {
        charactersInScene = FindObjectsOfType<Characters>();
        GeneralTestingStuff();
        CalculateAverageAge();
    }

    private void Update()
    {
        EnoughtOld();
        VIP();
    }

    private void CalculateAverageAge()
    {
        // Calcular la edad promedio de los personajes
        averageAge = (float)charactersInScene.Select(character => character.Age).Average();
        Debug.Log("Edad promedio de los personajes: " + averageAge);
    }

    private void EnoughtOld()
    {
        // Lista para almacenar los personajes que tienen m�s a�os que el promedio
        List<Characters> charactersToActivate = new List<Characters>();

        foreach (var character in charactersInScene)
        {
            // Verificar si el personaje tiene m�s a�os que el promedio
            if (character.Age > averageAge)
            {
                charactersToActivate.Add(character);
            }
        }

        // Activar los GameObjects de los personajes que tienen m�s a�os que el promedio
        foreach (var character in charactersToActivate)
        {
            if (character.old != null)
            {
                character.old.SetActive(true);
            }
        }
    }

    public void MCDonaldsRoute()
    {
        Patrullaje.rechazadoMacBool = true;
        Debug.Log($"Personajes inelegibles que van al mcdonalds{CharacterName} ");
    }


    public void BankRoute()
    {
        Debug.Log($"Personajes inelegibles que van al banco{CharacterName} ");
        Patrullaje.rechazadoBankBool = true;
        //StartCoroutine(EsperarYExecutar());
        //ejecutar funcion de tarjeta de credito y dale las monedas a este script
    }

    public void IdidntGetTheTicketRoute()
    {
        Debug.Log($"IdidntGetTheTicketRoute ejecutado {CharacterName}");
        Patrullaje.rechazadoMacBool = true;
        // no pase al recital ejecutar funcion en mi script de patrullaje para seguir determinado recorrido. Ejecutado en GM
    }

    public void GeneralTestingStuff()
    {
        // Filtrar los personajes mayores y menores de 18 a�os y obtener el color correspondiente
        var adultCharacters = charactersInScene.Where(character => character.Age >= 18).ToList();
        var minorCharacters = charactersInScene.Where(character => character.Age < 18).ToList();
        ChangeCharacterColor(adultCharacters, adultColor);
        ChangeCharacterColor(minorCharacters, minorColor);

        // Contar los personajes con suficiente dinero
        int charactersWithMoney = charactersInScene.Count(character => character.iHaveMoney > 10);
        Debug.Log("Personajes que podr�n comprar una entrada/comida/ser robados por el jhonny: " + charactersWithMoney);

        // Contar los personajes inelegibles para asistir al recital
        int ineligibleCharacters = charactersInScene.Count(character =>
            (character.WantsToAttendConcert && (character.iHaveMoney <= 10 || character.Age < 18)));
        Debug.Log("Cantidad de personajes inelegibles para asistir al recital: " + ineligibleCharacters);

        // Ordenar los personajes por apellido, luego por nombre y finalmente por edad
        var sortedCharacters = charactersInScene
            .OrderBy(character => character.CharacterLastName)
            .ThenBy(character => character.CharacterName)
            .ThenBy(character => character.Age)
            .ToList();

        // Espacio entre personajes
        float spacing = 3f;

        // Posici�n inicial en X
        float initialX = 0f;

        // Colocar los personajes en la escena
        for (int i = 0; i < sortedCharacters.Count; i++)
        {
            // Calcular la posici�n en X del personaje
            float xPos = initialX + i * spacing;

            // Asignar la nueva posici�n al personaje
            Vector3 newPosition = new Vector3(xPos, transform.position.y, transform.position.z);
            sortedCharacters[i].transform.position = newPosition;
        }

        // Usar Zip para combinar las edades de los personajes adultos y menores en una secuencia de tuplas
        var ageDifferences = adultCharacters.Select(adult => adult.Age)
                                .Zip(minorCharacters.Select(minor => minor.Age), (adultAge, minorAge) => adultAge - minorAge);

        // Calcular el promedio de las diferencias de edad
        float averageAgeDifference = (float)ageDifferences.Average();
        Debug.Log("Promedio de a�os que se llevan los personajes adultos con respecto a los menores: " + averageAgeDifference);
    }

    // Funci�n para cambiar el color de los personajes
    void ChangeCharacterColor(List<Characters> characters, Color color)
    {
        foreach (var character in characters)
        {
            Renderer renderer = character.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }
    }

    // Funci�n para trabajar con personajes VIP
    public void VIP()
    {
        // Tomar los personajes VIP mientras tengan suficiente dinero para ser VIP
        var vipCharacters = charactersInScene
            .OrderByDescending(character => character.iHaveMoney) // Ordenar por dinero en orden descendente
            .TakeWhile(character => character.iHaveMoney >= 20) // Tomar mientras tengan suficiente dinero
            .ToList();

        // Lista para almacenar los GameObjects de los personajes VIP
        List<GameObject> vipGameObjects = new List<GameObject>();

        // Imprimir mensajes de personajes VIP y agregar sus GameObjects a la lista
        foreach (var vipCharacter in vipCharacters)
        {
            //Debug.Log($"{vipCharacter.CharacterName} es VIP.");
            vipGameObjects.Add(vipCharacter.vipGameObject);
        }

        // Activar los GameObjects de los personajes VIP
        foreach (var vipGameObject in vipGameObjects)
        {
            vipGameObject.SetActive(true);
        }
    }
}