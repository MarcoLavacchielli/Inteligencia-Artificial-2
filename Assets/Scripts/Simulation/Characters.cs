using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

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
    public TextMeshProUGUI ageDifference;
    public TextMeshProUGUI listadeVIPS;

    private Characters[] charactersInScene;

    private void Awake()
    {
        Patrullaje = GetComponent<Patrullaje>();
    }

    void Start()
    {
        charactersInScene = FindObjectsOfType<Characters>();
        OrderCharactersByLastNameAndNameAndAge();
        EnoughtOld();
    }

    private void Update()
    {
        VIP();
    }

    private void EnoughtOld()
    {
        // Filtrar los personajes mayores y menores de 18 años y obtener el color correspondiente
        var adultCharacters = charactersInScene.Where(character => character.Age >= 18).ToList();
        var minorCharacters = charactersInScene.Where(character => character.Age < 18).ToList();

        ChangeCharacterColor(adultCharacters, adultColor);
        ChangeCharacterColor(minorCharacters, minorColor);

        // Usar Zip para combinar las edades de los personajes adultos y menores en una secuencia de tuplas
        var ageDifferences = adultCharacters.Select(adult => adult.Age)
                                .Zip(minorCharacters.Select(minor => minor.Age), (adultAge, minorAge) => adultAge - minorAge);

        // Calcular el promedio de la diferencia de edad entre los personajes adultos y menores
        float averageAgeDifference = (float)ageDifferences.Average();
        ageDifference.text = "Promedio de diferencia de edad entre personajes adultos y menores: " + averageAgeDifference;
        //Debug.Log("Promedio de diferencia de edad entre personajes adultos y menores: " + averageAgeDifference);

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
        //Debug.Log($"Personajes inelegibles que van al mcdonalds{CharacterName} ");
    }


    public void BankRoute()
    {
        //Debug.Log($"Personajes inelegibles que van al banco{CharacterName} ");
        Patrullaje.rechazadoBankBool = true;
        //StartCoroutine(EsperarYExecutar());
        //ejecutar funcion de tarjeta de credito y dale las monedas a este script
    }

    public void OrderCharactersByLastNameAndNameAndAge()
    {
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
    }

    // Función para cambiar el color de los personajes
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

    // Función para trabajar con personajes VIP
    public void VIP()
    {
        // Tomar los personajes VIP mientras tengan suficiente dinero para ser VIP
        var vipCharacters = charactersInScene
            .OrderByDescending(character => character.iHaveMoney) // Ordenar por dinero en orden descendente
            .Where(character => character.iHaveMoney >= 20) // Filtrar aquellos que tienen suficiente dinero
            .ToList();

        // Cadena para almacenar la información de los personajes VIP
        string vipInfo = "";

        // Iterar sobre los personajes VIP para construir la cadena de información
        foreach (var vipCharacter in vipCharacters)
        {
            vipInfo += "Nombre del VIP: " + vipCharacter.CharacterName + " " + vipCharacter.CharacterLastName + " Cantidad de dinero: " + vipCharacter.iHaveMoney + "\n";
        }

        // Asignar la cadena de información al texto que deseas mostrar
        listadeVIPS.text = vipInfo;

        // Lista para almacenar los GameObjects de los personajes VIP
        List<GameObject> vipGameObjects = new List<GameObject>();

        // Imprimir mensajes de personajes VIP y agregar sus GameObjects a la lista
        foreach (var vipCharacter in vipCharacters)
        {
            vipGameObjects.Add(vipCharacter.vipGameObject);
        }

        // Activar los GameObjects de los personajes VIP
        foreach (var vipGameObject in vipGameObjects)
        {
            vipGameObject.SetActive(true);
        }
    }
}