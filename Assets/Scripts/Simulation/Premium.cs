using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Premium : CreditCards
{
    private void Start()
    {
        multiplier = 5;
    }
    public override int GetCreditLimit()
    {
        return Random.Range(1, 10) + multiplier;
    }
}
