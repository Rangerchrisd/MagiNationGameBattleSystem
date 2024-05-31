using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punch : Move
{
    public override void ability(Character user, Character target) {
        target.takeDamage(user.strength +10,0);
    }

}
