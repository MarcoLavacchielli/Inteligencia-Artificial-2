using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : CreditCards
{
    public ParticleSystem PremiumBlueParticles;

    private void Start()
    {
        PremiumBlueParticles= GetComponentInChildren<ParticleSystem>();
    }
    public override int GetCreditLimit()
    {
        PremiumBlueParticles.Play();
        return Random.Range(1, 3) * multiplier;
    }
}
