using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Premium : CreditCards
{
    public ParticleSystem PremiumRedParticles;
    private void Start()
    {
        PremiumRedParticles = GetComponentInChildren<ParticleSystem>();

        multiplier = 5;
    }
    public override int GetCreditLimit()
    {
        
        PremiumRedParticles.Play();
        return Random.Range(1, 4) + multiplier;
    }
}
