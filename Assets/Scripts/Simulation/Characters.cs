using System.Collections;
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
    private Color VIPColor = Color.magenta;
    public int iHaveMoney;
    public bool WantsToAttendConcert;
    public Patrullaje Patrullaje;

    private void Awake()
    {
        Patrullaje = GetComponent<Patrullaje>();
    }

    void Start()
    {
        GeneralTestingStuff();
        VIP();
    }

    public void IdidntGetTheTicketRoute()
    {
        Debug.Log($"IdidntGetTheTicketRoute ejecutado {CharacterName}");
        Patrullaje.rechazadoBool = true;
        // no pase al recital ejecutar funcion en mi script de patrullaje para seguir determinado recorrido. Ejecutado en GM
    }

    public void GeneralTestingStuff()
    {
        // Obtener todas las instancias de Characters en la escena
        Characters[] charactersInScene = FindObjectsOfType<Characters>();

        // Filtrar los personajes mayores y menores de 18 años y obtener el color correspondiente
        var adultCharacters = GetCharactersAndColor(charactersInScene.Where(character => character.Age >= 18).ToList(), adultColor);
        var minorCharacters = GetCharactersAndColor(charactersInScene.Where(character => character.Age < 18).ToList(), minorColor);

        // Cambiar el color de los personajes y obtener las listas de personajes modificados
        var adultCharactersModified = ChangeCharacterColor(adultCharacters.Item1, adultCharacters.Item2);
        var minorCharactersModified = ChangeCharacterColor(minorCharacters.Item1, minorCharacters.Item2);

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
        var ageDifferences = adultCharacters.Item1.Select(adult => adult.Age)
                                .Zip(minorCharacters.Item1.Select(minor => minor.Age), (adultAge, minorAge) => adultAge - minorAge);

        // Calcular el promedio de las diferencias de edad
        float averageAgeDifference = (float)ageDifferences.Average();
        Debug.Log("Promedio de años que se llevan los personajes adultos con respecto a los menores: " + averageAgeDifference);
    }

    // Función para cambiar el color de los personajes y devolver la lista de personajes modificados
    List<Characters> ChangeCharacterColor(List<Characters> characters, Color color)
    {
        foreach (var character in characters)
        {
            Renderer renderer = character.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }
        return characters;
    }

    // Función para obtener la lista de personajes y el color correspondiente en una tupla
    (List<Characters>, Color) GetCharactersAndColor(List<Characters> characters, Color color)
    {
        return (characters, color);
    }

    // Función para trabajar con personajes VIP
    public void VIP()
    {
        // Obtener todas las instancias de Characters en la escena
        Characters[] charactersInScene = FindObjectsOfType<Characters>();

        // Tomar los personajes VIP mientras tengan suficiente dinero para ser VIP
        var vipCharacters = charactersInScene
            .OrderByDescending(character => character.iHaveMoney) // Ordenar por dinero en orden descendente
            .TakeWhile(character => character.iHaveMoney > 10) // Tomar mientras tengan suficiente dinero
            .ToList();

        // Imprimir mensajes de personajes VIP
        foreach (var vipCharacter in vipCharacters)
        {
            Debug.Log($"{vipCharacter.CharacterName} es VIP.");
        }
    }
}
