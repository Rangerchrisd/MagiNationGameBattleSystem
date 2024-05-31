using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Pet[] myBalls;
    public Pet[] activePets;

    public characterController[] characterControllers;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public virtual void doTurn()
    {


    }
    public bool onTeam(Character pokeman) {
        if (this == pokeman)
            return true;
        foreach (Character pokemon in myBalls) {
            if (pokemon == pokeman)
                return true;
        }
        return false;
    }

    public virtual void killPokemon(Pet oldYeller) {
        characterControllers[findPokemonIndexCharacterController(oldYeller)].thisCharacter = null;
        //activePets[findPokemonIndexActivePet(oldYeller)] = null;
        removeFromActivePets(findPokemonIndexActivePet(oldYeller));
        
    }
    public int firstAvalibleSlot() {
        for (int i = 0; i < characterControllers.Length; i++)
        {
            if (characterControllers[i].thisCharacter == null)
                return i;
        }
        return -1;
    }
    public int findPokemonIndexCharacterController(Pet finding) {
        for (int i = 0; i < characterControllers.Length; i++)
        {
            if (characterControllers[i].thisCharacter == finding)
                return i;
        }
        return -1;
    }
    public int findPokemonIndexActivePet(Pet finding)
    {
        for (int i = 0; i < activePets.Length; i++)
        {
            if (activePets[i] == finding)
                return i;
        }
        return -1;
    }
    public void removeFromActivePets(int toRemove) {
        Pet[] newActivePets = new Pet[activePets.Length-1];
        int i = 0, j =0;
        Debug.Log(toRemove);
        foreach (Pet activePet in activePets) {
            if (j != toRemove)
            {
                newActivePets[i] = activePet;
                i++;
            }
            j++;
        }
        activePets = newActivePets;
    }
}
