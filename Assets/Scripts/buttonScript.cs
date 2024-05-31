using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
    public Move myMove;
    public Character thisCharacter;
    public Protagonist ourProtagonist;
    public Enemy theirProtagonist;
    public GameObject thisCharacterOption;

    public void changeCharacter() {
        if (this.thisCharacter.attaba <= 0)
        {
            ourProtagonist.changeCharacter(thisCharacter);
        }
    }
    public void useMyMove() {
        ourProtagonist.activeMove = myMove;
        ourProtagonist.activeCharacter = thisCharacter;
        if (myMove.zeroTarget)
        {
            myMove.useAbility(thisCharacter, thisCharacter);
            ourProtagonist.activeCharacter = null;
            ourProtagonist.activeMove = null;
        }
    }
}