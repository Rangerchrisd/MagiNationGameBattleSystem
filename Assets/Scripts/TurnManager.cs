using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public Character thisCharacter;
    public Character targetCharacter;
    buttonScript[] moveButtons;
    public void loadCharacter(Character loadCharacter) {
        for (int j = 0; j < moveButtons.Length; j++) {
            moveButtons[j].gameObject.SetActive(true);
        }
        thisCharacter = loadCharacter;
        int i = 0;
        foreach (Move myMove in loadCharacter.moveSet) {
            moveButtons[i].myMove = myMove;
        }
        for (;i < moveButtons.Length; i++)
        {
            moveButtons[i].gameObject.SetActive(false);
        }

    }
    public void useMove(int moveNumber) {
        thisCharacter.moveSet[moveNumber].useAbility(thisCharacter, targetCharacter);
        //thisCharacter.attaba += thisCharacter.moveSet[moveNumber].timeCost;
    }
}
