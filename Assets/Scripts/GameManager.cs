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
        //forEachMac(mac(DeterminateRouteOfCharacters()));
        //forEachBank(Bank(DeterminateRouteOfCharacters()));
        
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
        //Characters[] charactersInScene = FindObjectsOfType<Characters>();

        List<Characters> ineligibleCharacters = Characters.Aggregate(new List<Characters>(), (list, character) =>
        {
            if (character.WantsToAttendConcert && (character.iHaveMoney <= 10 || character.Age < 18))
            {
                list.Add(character);
            }
            return list;
        });
        
        Debug.Log("Personajes inelegibles para asistir al recital: " + ineligibleCharacters.Count);
        
        return ineligibleCharacters;
    }
    /*1 primero hago un get component en el start para que tenga a todos los characters
     * 2 ejecuto una funcion con Linq que devuelva una lista con los que van al mac
     * 3 meto dicha funcion con linq como parametro en otra que ejecute el foreach que lleva al Mac
     
     
     */
    /*
    public void forEachMac(List<Characters> ineligibleCharacters)
    {
        foreach(Characters character in ineligibleCharacters)
        {
            character.MCDonaldsRoute();
        }
    }
    public List<Characters> mac(List<Characters> x)
    {
        List<Characters> ineligibleCharacters = x.Aggregate(new List<Characters>(), (list, character) =>
        {
            if (character.WantsToAttendConcert && (character.iHaveMoney != 0))
            {
                list.Add(character);
            }
            return list;
        });
        return ineligibleCharacters;
    }
    public List<Characters> Bank(List<Characters> x)
    {
        List<Characters> ineligibleCharacters = x.Aggregate(new List<Characters>(), (list, character) =>
        {
            if (character.WantsToAttendConcert && (character.iHaveMoney == 0))
            {
                list.Add(character);
            }
            return list;
        });
        return ineligibleCharacters;
    }
    public void forEachBank(List<Characters> ineligibleCharacters)
    {
        foreach (Characters character in ineligibleCharacters)
        {
            character.BankRoute();
        }
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


    }*/




}
