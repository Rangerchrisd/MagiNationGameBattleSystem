using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Player
{
    public Protagonist playerscript;
    public EnemyTarget[] enemyTargets;
    public int exp;
    public int gold;
    public override void doTurn() {
        //activePets[0].moveSet[0].useAbility(activePets[0], playerscript.activePets[0]);
        //playerscript.isTurn = true;
        moveSet[0].useAbility(this, playerscript);
    }


    public override void killPokemon(Pet oldYeller)
    {
        enemyTargets[findPokemonIndexEnemyTarget(oldYeller)].thisCharacter = null;
        //activePets[findPokemonIndexActivePet(oldYeller)] = null;
        removeFromActivePets(findPokemonIndexActivePet(oldYeller));
    }

    public int findPokemonIndexEnemyTarget(Pet finding)
    {
        for (int i = 0; i < enemyTargets.Length; i++)
        {
            if (enemyTargets[i].thisCharacter == finding)
                return i;
        }
        return -1;
    }

    private void FixedUpdate()
    {
        if (attaba <= 0) {
            doTurn();
        }
        if (energy <= 0)
        {
            die();
        }
    }
    public void die() {
        playerscript.endFight();
        playerscript.gold += gold;
        playerscript.getXP(exp);
        Destroy(this.gameObject);
        
    }

}
