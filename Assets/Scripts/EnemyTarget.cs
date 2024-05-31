using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTarget : MonoBehaviour
{
    public Enemy theirProtagonist;
    public Protagonist ourProtagonist;
    public Character thisCharacter;
    public Image myImage;
    public bool isPlayer;

    public Text hitBar;
    public void Start()
    {
        myImage = this.GetComponent<Image>();
        theirProtagonist = FindObjectOfType<Enemy>();
        ourProtagonist = FindObjectOfType<Protagonist>();

    }
    // Update is called once per frame
    void Update()
    {
        if (thisCharacter == null && myImage.enabled)
        {
            myImage.enabled = false;
            hitBar.enabled = false;

        }
        else if (thisCharacter != null && !myImage.enabled)
        {
            myImage.enabled = true;
            hitBar.enabled = true;

        }
        if (myImage.enabled == true)
            hitBar.text = thisCharacter.energy + "/" + thisCharacter.maxEnergy;


    }
    public void sellectThis()
    {
        if (ourProtagonist.activeMove != null&&(!isPlayer||theirProtagonist.activePets.Length==0))
        { 
            ourProtagonist.activeMove.useAbility(ourProtagonist.activeCharacter, thisCharacter);
            ourProtagonist.activeButton = null;
            ourProtagonist.activeCharacter = null;
            ourProtagonist.activeMove = null;
        }
    }
}
