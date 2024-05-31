using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public bool acceptingInput;
    public string[] dialogueLines = new string[0];
    public int lineNumber = 0;
    public int dialogueLinesCount;
    public DialogueManager dialogueManagerPrefab;
    public Dialogue nextLevelDialogue;
    DialogueManager dialogueManager;

    public string dialogueLine;
    public string characterName;
    public string dialogueDescription;
    public Sprite characterImage;
    public Sprite[] MyImages;

    public Protagonist protagonist;

    public Sprite getImage(int index) {
        if (MyImages.Length < index)
        {
            return null;
        }
        return MyImages[index];
    }
    public Sprite getImage(string faceIndex)
    {
        int index = int.Parse(faceIndex);
        return getImage(index);
    }
    public void Start()
    {
        Debug.Log("a");
        DialogueAnswer.onAnswer += answerQuestion;
        if (!dialogueManager)
        {
            dialogueManager = Instantiate(dialogueManagerPrefab);
        }
        //protagonist.inDialogue = true;
        dialogueLinesCount =  dialogueLines.Length;
        parseLine();
    }
    public void OnDestroy()
    {
        DialogueAnswer.onAnswer -= answerQuestion;
    }
    public void Update()
    {

        if (acceptingInput)
        {
            CheckInput();
        }
    }
    public void answerQuestion(int answer) {
        Debug.Log(answer);
        //if you answer 0, new conversation
        if (answer == 0)
        {
            Dialogue dialogue = Instantiate(nextLevelDialogue);
            dialogueManager.destroyAnswers();
            dialogue.dialogueManager = dialogueManager;
            acceptingInput = true;
            Destroy(this.gameObject);

        }
        else {
            //if you answer 1, next line
            acceptingInput = true;
            dialogueManager.destroyAnswers();
            lineNumber++;
            parseLine();
        }
    }
    public void CheckInput() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (lineNumber+1 < dialogueLinesCount)
            {
                lineNumber++;
                parseLine();
            }
            else {
                Destroy(dialogueManager.gameObject);
                Destroy(this.gameObject);
            
            }
        }
    }
    public void parseLine() {

        processLine();
        dialogueManager.newLine(characterName,characterImage,dialogueLine);

    }
    public void resetLine()
    {
        characterImage = null;
        characterName = "";
        dialogueLine = "";
    }
    public void processLine()
    {
        resetLine();
        string read = dialogueLines[lineNumber];
        if (read.StartsWith("q:"))
        {
            acceptingInput = false;
            read = read.Substring(2);
            string[] answers = read.Split(':');
            dialogueManager.newQuestion(answers);
            return;
        }
        else if (read.StartsWith("b:"))
        {
            read = read.Substring(2);
            int backgroundIndex = int.Parse(read);
            if (backgroundIndex < MyImages.Length)
                dialogueManager.newBackground(MyImages[backgroundIndex]);
            lineNumber++;
            parseLine();
            return;
        }
        string[] words = read.Split(':');
        if (words.Length == 0) {
            return;
        }
        else if (words.Length == 1) {
            dialogueLine = words[0];
            
        } else if (words.Length == 2)
        {
            characterImage = getImage(words[0]);
            dialogueLine = words[1];
        } else if (words.Length == 3)
        {
            characterName = words[0];
            characterImage = getImage(words[1]);
            dialogueLine = words[2];
        }
        else {
            if (read.StartsWith("?:")){
                Debug.Log("you forgot to press the needsParty button or it couldn't find it");
            }
            else {
                Debug.Log("you fool." +
                    " you absolute buffoon. you think you can challenge me in my own realm?" +
                    " you think you can rebel against my authority? ");
            }
        }
    }
}
