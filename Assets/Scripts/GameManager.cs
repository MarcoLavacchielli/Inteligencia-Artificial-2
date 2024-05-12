using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public List<Characters> Characters;
    private void Start()
    {
        Characters=DeterminateRouteOfCharacters();
    }

    public void ListOfTheOnesThatGoToMac()
    {
        List<Characters> ineligibleCharacters = Characters.Aggregate(new List<Characters>(), (list, character) =>
        {
            if (character.WantsToAttendConcert && (character.iHaveMoney!=0))
            {
                list.Add(character);
            }
            return list;
        });
        foreach (Characters character in ineligibleCharacters)
        {
            character.MCDonaldsRoute();
        }

        Debug.Log("Personajes inelegibles que van al mcdonalds: " + ineligibleCharacters.Count);
        
        
    }
    public void ListOfTheOnesThatGoToBank()
    {
        List<Characters> ineligibleCharacters = Characters.Aggregate(new List<Characters>(), (list, character) =>
        {
            if (character.WantsToAttendConcert && (character.iHaveMoney == 0))
            {
                list.Add(character);
            }
            return list;
        });
        foreach (Characters character in ineligibleCharacters)
        {
            character.BankRoute();
        }

        Debug.Log("Personajes inelegibles que van al banco: " + ineligibleCharacters.Count);


    }


    public List<Characters> DeterminateRouteOfCharacters()
    {
        Characters[] charactersInScene = FindObjectsOfType<Characters>();

        List<Characters> ineligibleCharacters = charactersInScene.Aggregate(new List<Characters>(), (list, character) =>
        {
            if (character.WantsToAttendConcert && (character.iHaveMoney <= 10 || character.Age < 18))
            {
                list.Add(character);
            }
            return list;
        });
        foreach (Characters character in ineligibleCharacters)
        {
            character.IdidntGetTheTicketRoute();
        }

        Debug.Log("Personajes inelegibles para asistir al recital: " + ineligibleCharacters.Count);
        /*
         int ineligibleCharacters = charactersInScene.Aggregate(0, (count, character) =>
             (character.WantsToAttendConcert && (character.iHaveMoney <= 10 || character.Age < 18)) ? count + 1 : count);

         Debug.Log("Cantidad de personajes inelegibles para asistir al recital: " + ineligibleCharacters);*/
        return ineligibleCharacters;
    }



}
