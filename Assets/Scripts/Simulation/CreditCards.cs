using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditCards : MonoBehaviour
{
    public int multiplier;
    public virtual int GetCreditLimit()
    {
        return Random.Range(0, 10)*multiplier;
    }
}
/*
public class Basic : CreditCards
{
    
    public override int GetCreditLimit()
    {
        return Random.Range(1, 5); 
    }
}
public class Premium : CreditCards
{
  
    public override int GetCreditLimit()
    {
            return Random.Range(1, 10);
    }
}
*/