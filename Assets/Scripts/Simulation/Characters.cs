using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    private Characters[] charactersInScene;

    private void Awake()
    {
        Patrullaje = GetComponent<Patrullaje>();
    }

    void Start()
    {
        charactersInScene = FindObjectsOfType<Characters>(); // Se busca una vez al inicio
        GeneralTestingStuff();
        VIP();
    }

    public void MCDonaldsRoute()
    {
        Debug.Log($"Personajes inelegibles que van al mcdonalds{CharacterName} ");

    }

    public void BankRoute()
    {
        Debug.Log($"Personajes inelegibles que van al banco{CharacterName} ");
    }

    public void IdidntGetTheTicketRoute()
    {
        Debug.Log($"IdidntGetTheTicketRoute ejecutado {CharacterName}");
        Patrullaje.rechazadoBool = true;
        // no pase al recital ejecutar funcion en mi script de patrullaje para seguir determinado recorrido. Ejecutado en GM
    }

    public void GeneralTestingStuff()
    {
        // Filtrar los personajes mayores y menores de 18 años y obtener el color correspondiente
        var adultCharacters = charactersInScene.Where(character => character.Age >= 18).ToList();
        var minorCharacters = charactersInScene.Where(character => character.Age < 18).ToList();
        ChangeCharacterColor(adultCharacters, adultColor);
        ChangeCharacterColor(minorCharacters, minorColor);

        // Calcular la edad promedio de los personajes
        float averageAge = (float)charactersInScene.Select(character => character.Age).Average();
        Debug.Log("Edad promedio de los personajes: " + averageAge);

        // Contar los personajes con suficiente dinero
        int charactersWithMoney = charactersInScene.Count(character => character.iHaveMoney > 10);
        Debug.Log("Personajes que podrán comprar una entrada/comida/ser robados por el jhonny: " + charactersWithMoney);

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

        // Posición inicial en X
        float initialX = 0f;

        // Colocar los personajes en la escena
        for (int i = 0; i < sortedCharacters.Count; i++)
        {
            // Calcular la posición en X del personaje
            float xPos = initialX + i * spacing;

            // Asignar la nueva posición al personaje
            Vector3 newPosition = new Vector3(xPos, transform.position.y, transform.position.z);
            sortedCharacters[i].transform.position = newPosition;
        }

        // Usar Zip para combinar las edades de los personajes adultos y menores en una secuencia de tuplas
        var ageDifferences = adultCharacters.Select(adult => adult.Age)
                                .Zip(minorCharacters.Select(minor => minor.Age), (adultAge, minorAge) => adultAge - minorAge);

        // Calcular el promedio de las diferencias de edad
        float averageAgeDifference = (float)ageDifferences.Average();
        Debug.Log("Promedio de años que se llevan los personajes adultos con respecto a los menores: " + averageAgeDifference);
    }

    // Función para cambiar el color de los personajes
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

    // Función para trabajar con personajes VIP
    public void VIP()
    {
        // Tomar los personajes VIP mientras tengan suficiente dinero para ser VIP
        var vipCharacters = charactersInScene
            .OrderByDescending(character => character.iHaveMoney) // Ordenar por dinero en orden descendente
            .TakeWhile(character => character.iHaveMoney > 20) // Tomar mientras tengan suficiente dinero
            .ToList();

        // Imprimir mensajes de personajes VIP
        foreach (var vipCharacter in vipCharacters)
        {
            Debug.Log($"{vipCharacter.CharacterName} es VIP.");
        }
    }
}