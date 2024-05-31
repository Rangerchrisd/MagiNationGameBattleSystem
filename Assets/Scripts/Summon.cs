using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summon : Move
{
    public int summonStage;
    public int input;
    public Character myCharacter;
    public Protagonist myProtag;
    public GameObject[] summonButtons;
    public GameObject summonInfoButton;

    public void Start()
    {
        input = -1;
    }
    private void Update()
    {

            if (myProtag&&summonStage==1)
            {
                if (myProtag.activeCharacter != null)
                    destroyButtons();
            }
        if (summonStage != 0&&input!=-1)
            waitingForInput();
        
    }
    public override void ability(Character user, Character target)
    {
        Debug.Log("SummonabilityA");
        myProtag = user.GetComponent<Protagonist>();
        summonStage = 1;
        myProtag.attaba -= timeCost;
        myCharacter = user;
        createButtons();
    }
    public bool isBusy(Protagonist thisProtag,Pet summonAttempt)
    {
        foreach (Pet thisPet in thisProtag.activePets)
        {
            if (thisPet == summonAttempt)
                return true;
        }
        return false;
    }

    public void waitingForInput()
    {
        Debug.Log("SummonwaitingForInputA");
        if (!isBusy(myProtag, myProtag.myBalls[input]))
        {
            int firstAvalibleSlot = myProtag.firstAvalibleSlot();
            Debug.Log("SummonwaitingForInputB");
            Pet[] newArray = new Pet[myProtag.activePets.Length + 1];
            int i = 0;
            foreach (Pet thisPet in myProtag.activePets)
            {
                newArray[i] = thisPet;
                i++;
            }
            myProtag.activePets = newArray;
            myProtag.activePets[firstAvalibleSlot] = myProtag.myBalls[input];
            myProtag.characterControllers[firstAvalibleSlot].thisCharacter = myProtag.activePets[myProtag.activePets.Length-1];
            myProtag.characterControllers[firstAvalibleSlot].loadCharacter();
            input = -1;
            summonStage = 0;
            //myProtag.summonTargets.SetActive(false);
            destroyButtons();
            myProtag.attaba += timeCost;
        }

    }
    public void cancelSummonInput() {
        input = 0;
        summonStage = 0;
    }
    public void summonInput(int summonInt) {
        input = summonInt;
    
    }
    
    public void createButtons() {
        summonButtons = new GameObject[myProtag.myBalls.Length];
        for (int i = 0; i < myProtag.myBalls.Length; i++) {
            summonButtons[i] =  Instantiate(summonInfoButton, myProtag.summonTargets.transform);
            summonButtons[i].GetComponentInChildren<Text>().text = myProtag.myBalls[i].characterName;
            summonButtons[i].transform.position = Vector3.right * 50 * i;
            summonButtons[i].GetComponent<ShowSummon>().summonScript = this;
            summonButtons[i].GetComponent<ShowSummon>().i = i;
        }
    
    }
    public void destroyButtons()
    {
        for (int i = 0; i < summonButtons.Length; i++)
        {
            Destroy(summonButtons[i]);
        }
        summonStage = 0;
    }

}
