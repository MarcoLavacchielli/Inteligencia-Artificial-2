using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : CreditCards
{

    public override int GetCreditLimit()
    {
        return Random.Range(1, 5) * multiplier;
    }
}
