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

    void Start()
    {
        // Obtener todas las instancias de Characters en la escena
        Characters[] charactersInScene = FindObjectsOfType<Characters>();

        // Filtrar los personajes mayores y menores de 18 años y obtener el color correspondiente
        var adultCharacters = GetCharactersAndColor(charactersInScene.Where(character => character.Age >= 18).ToList(), adultColor);
        var minorCharacters = GetCharactersAndColor(charactersInScene.Where(character => character.Age < 18).ToList(), minorColor);

        // Cambiar el color de los personajes y obtener las listas de personajes modificados
        var adultCharactersModified = ChangeCharacterColor(adultCharacters.Item1, adultCharacters.Item2);
        var minorCharactersModified = ChangeCharacterColor(minorCharacters.Item1, minorCharacters.Item2);

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
}