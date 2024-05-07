using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficult : MonoBehaviour
{
    public Difficulty selectedDifficulty;

    public void SelectEasy()
    {
        selectedDifficulty = Difficulty.Easy;
    }

    public void SelectNormal()
    {
        selectedDifficulty = Difficulty.Normal;
    }

    public void SelectHard()
    {
        selectedDifficulty = Difficulty.Hard;
    }
}
