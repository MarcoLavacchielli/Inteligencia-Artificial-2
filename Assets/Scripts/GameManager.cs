using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.TextCore.Text;
using System;

public class GameManager : MonoBehaviour
{
    public Characters[] Characters;
   
    private void Awake()
    {
        Characters = FindObjectsOfType<Characters>();
    }
    private void Start()
    {
        ToBankOrMacDefinitive(DeterminateRouteOfCharacters());
    }
    public void ToBankOrMacDefinitive(List<Characters> FirstFilter)
    {
        var grupos = FirstFilter.Aggregate(
            Tuple.Create(new List<Characters>(), new List<Characters>()),
            (acumulado, personaje) =>
            {
                
                if (personaje.iHaveMoney==0)
                {
                    acumulado.Item1.Add(personaje); 
                }
                else
                {
                    acumulado.Item2.Add(personaje); 
                }
                return acumulado;
            });
        foreach (var personaje in grupos.Item1)
        {
            personaje.BankRoute();
        }
        foreach (var personaje in grupos.Item2)
        {
            personaje.MCDonaldsRoute();
        }
    }


    public List<Characters> DeterminateRouteOfCharacters()
    {
        

        List<Characters> ineligibleCharacters = Characters.Aggregate(new List<Characters>(), (list, character) =>
        {
            if (character.WantsToAttendConcert && (character.iHaveMoney <= 10 || character.Age < 18))
            {
                list.Add(character);
            }
            return list;
        });
        
        
        
        return ineligibleCharacters;
    }
}
