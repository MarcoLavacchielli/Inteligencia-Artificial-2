using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //[SerializeField] private GameObject pauseCanvas;
    public bool isPaused = false;

    // Lista de personajes
    public List<Characters> characters = new List<Characters>();

    public void TogglePause()
    {
        Time.timeScale = 0f;
        foreach (Characters character in characters)
        {
            character.OrderCharactersByLastNameAndNameAndAge();
        }
    }

    public void ToggleUnPause()
    {
        Time.timeScale = 1f;
    }
}
