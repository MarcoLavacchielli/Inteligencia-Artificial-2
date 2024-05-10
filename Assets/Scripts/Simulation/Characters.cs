using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Characters : MonoBehaviour
{
    public string CharacterName;
    public string CharacterLastName;

    void Start()
    {
        // Obtener todas las instancias de Characters en la escena
        Characters[] charactersInScene = FindObjectsOfType<Characters>();

        // Ordenar los personajes por apellido y luego por nombre
        var sortedCharacters = charactersInScene
            .OrderBy(character => character.CharacterLastName)
            .ThenBy(character => character.CharacterName)
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
}