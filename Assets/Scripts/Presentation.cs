using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Presentation : MonoBehaviour
{
    public TMP_Text textUI;

    void Start()
    {
        Students();
    }

    void Students()
    {
        var names = new List<string> { "Marco", "Santiago", "Facundo" };
        var lastNames = new List<string> { "Lavacchielli", "Sanchez", "Tisera" };

        var studentData = names
            .Zip(lastNames, (name, lastName) => (Name: name, LastName: lastName))
            .OrderBy(student => student.LastName)
            .ThenBy(student => student.Name);

        string finalText = "Names of the students:\n";
        foreach (var student in studentData)
        {
            finalText += $"{student.Name} {student.LastName}\n";
        }

        textUI.text = finalText;
    }
}