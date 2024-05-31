using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : Character
{
    public void FixedUpdate()
    {
        if (energy <= 0)
        {
            team.killPokemon(this);
            this.energy=1;
        }
    }
}
