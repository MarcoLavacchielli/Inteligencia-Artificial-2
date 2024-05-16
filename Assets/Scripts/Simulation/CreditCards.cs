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