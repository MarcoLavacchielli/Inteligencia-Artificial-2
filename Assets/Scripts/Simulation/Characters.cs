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
    public GameObject vipGameObject;
    public GameObject old;

    private Characters[] charactersInScene;

    private void Awake()
    {
        Patrullaje = GetComponent<Patrullaje>();
    }

    void Start()
    {
        charactersInScene = FindObjectsOfType<Characters>();
        OrderCharactersByLastNameAndNameAndAge();
        CountCharactersWithMoney();
        CountIneligibleCharactersForConcert();
        EnoughtOld();
    }

    private void Update()
    {
        VIP();
    }

    private void EnoughtOld()
    {
        // Filtrar los personajes mayores y menores de 18 a�os y obtener el color correspondiente
        var adultCharacters = charactersInScene.Where(character => character.Age >= 18).ToList();
        var minorCharacters = charactersInScene.Where(character => character.Age < 18).ToList();

        ChangeCharacterColor(adultCharacters, adultColor);
        ChangeCharacterColor(minorCharacters, minorColor);

        // Usar Zip para combinar las edades de los personajes adultos y menores en una secuencia de tuplas
        var ageDifferences = adultCharacters.Select(adult => adult.Age)
                                .Zip(minorCharacters.Select(minor => minor.Age), (adultAge, minorAge) => adultAge - minorAge);

        // Calcular el promedio de la diferencia de edad entre los personajes adultos y menores
        float averageAgeDifference = (float)ageDifferences.Average();
        Debug.Log("Promedio de diferencia de edad entre personajes adultos y menores: " + averageAgeDifference);

        foreach (var character in charactersInScene)
        {
            // Calcular la diferencia de edad de este personaje con respecto a la diferencia promedio
            int characterAgeDifference = character.Age - (int)averageAgeDifference;

            // Verificar si la diferencia de edad del personaje es mayor que cero
            if (characterAgeDifference > 0)
            {
                // Activar el GameObject correspondiente
                if (character.old != null)
                {
                    character.old.SetActive(true);
                }
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

    private void CountCharactersWithMoney()
    {
        // Contar los personajes con suficiente dinero
        int charactersWithMoney = charactersInScene.Count(character => character.iHaveMoney > 10);
        Debug.Log("Personajes que podr�n comprar una entrada/comida/ser robados por el jhonny: " + charactersWithMoney);
    }

    private void CountIneligibleCharactersForConcert()
    {
        // Contar los personajes inelegibles para asistir al recital
        int ineligibleCharacters = charactersInScene.Count(character =>
            (character.WantsToAttendConcert && (character.iHaveMoney <= 10 || character.Age < 18)));
        Debug.Log("Cantidad de personajes inelegibles para asistir al recital: " + ineligibleCharacters);
    }

    private void OrderCharactersByLastNameAndNameAndAge()
    {
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
    }

    // Funci�n para cambiar el color de los personajes
    private void ChangeCharacterColor(List<Characters> characters, Color color)
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