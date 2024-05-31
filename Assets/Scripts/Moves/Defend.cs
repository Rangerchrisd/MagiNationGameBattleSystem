using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : Move
{
    public override void ability(Character user, Character target)
    {
        user.isDefending = true;
    }
}
