using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int energy,maxEnergy, strength, dexterity, stamina, wisdom, luck, level;
    public int type0;
    public int type1;
    public bool isDefending, isParalyzed;
    
    public float attaba, paralysisTimer, maxAttaba;
    public Player team;
    public Move[] moveSet;
    public GameObject[] moveButtons;
    public GameObject activeButton;
    public Sprite myArt;
    public string characterName;
    void Update()
    {
        
        if (isParalyzed) {
            if (paralysisTimer <= 0)
            {
                isParalyzed = false;
            }
            else {
                paralysisTimer -= Time.deltaTime;
            }
        }else if (attaba >= 0)
        {
            attaba -= Time.deltaTime * (1 + dexterity * .1f);
        }
        else if (isDefending) {
            isDefending = false;
        }
    }
    public virtual void takeDamage(int energyTaken,int damageType) {

            if (!containsType(0))
            {
                this.energy -= energyTaken;
            }
            else {
                this.energy -= energyTaken;
            }
    }
    public bool containsType(int type) {
        if (type0 == type && type1 == type)
            return true;
        return false;
    }
}
