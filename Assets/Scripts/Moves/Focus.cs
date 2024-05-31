using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focus : Move
{
    public override void ability(Character user, Character target)
    {
        user.energy += (int)(user.maxEnergy * .1f); 
    }
}
