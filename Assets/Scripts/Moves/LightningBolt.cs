using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : Move
{
    public override void ability(Character user, Character target)
    {
        //user.isDefending = user.isDefending & user.isParalyzed;

        target.takeDamage((int)(user.wisdom*.5f+5),2);
        //if(Random.Range(0,100)>50)
        target.isParalyzed = true;
        target.paralysisTimer = 2;
    }
}
