using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class characterController : MonoBehaviour
{
    public Protagonist ourProtagonist;
    public Character thisCharacter;
    public GameObject childButton;
    public GameObject[] buttons;
    public Image myImage;

    public Text hitBarAtt;
    public Image Attabar;
    public void Start()
    {
        ourProtagonist = FindObjectOfType<Protagonist>();
        myImage = this.GetComponent<Image>();
        if(thisCharacter)
            loadCharacter();
    }
    public void loadCharacter() {
        int i=0;
        childButton.SetActive(true);
        foreach (GameObject myButton in buttons)
        {
            if (i >= thisCharacter.moveSet.Length)
                break;
            myButton.SetActive(true);
            myButton.GetComponent<buttonScript>().myMove = thisCharacter.moveSet[i];
            myButton.GetComponent<buttonScript>().ourProtagonist = ourProtagonist;
            myButton.GetComponent<buttonScript>().thisCharacter = thisCharacter;
            myButton.GetComponentInChildren<Text>().text = thisCharacter.moveSet[i].nameOfMove;
            i++;
        }
        int j = 0;
        foreach (GameObject myButton in buttons)
        {
            if (j >= i)
            {
                myButton.SetActive(false);
            }
                j++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (ourProtagonist.activeButton != this.gameObject)
        {
            childButton.SetActive(false);
        }
        if (thisCharacter == null && myImage.enabled)
        {
            myImage.enabled = false;
            Attabar.enabled = false;
            hitBarAtt.enabled = false;
            childButton.SetActive(false);

        }
        else if (thisCharacter != null && !myImage.enabled) {
            myImage.enabled = true;
            hitBarAtt.enabled = true;

        }
        if (myImage.enabled == true)
        {
            hitBarAtt.text = thisCharacter.energy + "/" + thisCharacter.maxEnergy + "\n" + thisCharacter.attaba;
            if (Attabar.enabled == false)
            {
                Attabar.enabled = true;
            }
            Attabar.fillAmount = thisCharacter.attaba / thisCharacter.maxAttaba;
        }

    }
    public void sellectThis()
    {
        if (ourProtagonist.activeMove == null)
        {
            if (ourProtagonist.activeButton == this.gameObject)
            {
                ourProtagonist.activeCharacter = thisCharacter;
                ourProtagonist.activeButton = null;
                childButton.SetActive(false);
            }
            else
            {
                ourProtagonist.activeButton = this.gameObject;
                childButton.SetActive(true);
            }
        }
        else{
            ourProtagonist.activeMove.useAbility(ourProtagonist.activeCharacter, thisCharacter);
            ourProtagonist.activeCharacter = null;

            ourProtagonist.activeMove = null;
        }
    }
}
