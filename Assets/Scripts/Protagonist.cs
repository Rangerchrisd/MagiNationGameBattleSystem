using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class Protagonist : Player
{
    public Enemy enemyTarget;
    public bool isTurn;
    public Move activeMove;
    public int summonStep;

    public Dialogue dialogue;
    public Character activeCharacter;
    public GameObject[] characterUi;

    public GameObject summonTargets;

    public GameObject battleManagerPrefab;
    public GameObject battleMangerObject;
    public GameObject enemyPrefab;

    public Rigidbody2D thisRigidbody;

    public bool[] storySwitches;

    public int gold, xp;
    public bool testing;
    public float speed;

    public string input;


    public GameObject textWindow;
    public Text textLine;
    public Image textProfilePick;

    public bool inDialogue;
    public void getXP(int xpAmount) {
        xp += xpAmount;
        if (xp <= Mathf.Pow(2,level-1) * 1000)
            level++;
    }

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(this.gameObject);
        if (FindObjectsOfType<Protagonist>().Length == 2)
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (testing)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                newTestBattle();
            }else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                save("AutoSave");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                load("AutoSave");
            }
        }
        if (energy <= 0)
        {
            die();
        }
        if (!battleMangerObject||inDialogue)
            move();
    }

    public void move() {
            thisRigidbody.velocity = speed * Vector2.right * Input.GetAxis("Horizontal")
            + speed * Vector2.up * Input.GetAxis("Vertical");
    }


    public void die()
    {
        
        SceneManager.LoadScene("MainMenu");
        Destroy(this.gameObject);
    }
    public void selectThis(Character selecting)
    {
        //has
        if (selecting.attaba <= 0 && team == this) {
            changeCharacter(selecting);
        }
    }
    public void changeCharacter(Character newCharacter) {
        if (newCharacter == this)
        {
            characterUi[0].SetActive(true);
            for (int i = 1; i < characterUi.Length; i++) {
                characterUi[i].SetActive(false);
            }
        }
        else {
            for (int i = 0; i < characterUi.Length; i++)
            {
                characterUi[i].SetActive(false);
            }
            characterUi[findPokemon((Pet)newCharacter)+1].SetActive(true);
        }
    }

    public int findPokemon(Pet pokeman) {
        int i = 0;
        foreach (Pet thisOne in myBalls) {
            if(thisOne == pokeman)
                return i;
            i++;
        }
        return -1;
    }
    public void newTestBattle() {
        encounter(enemyPrefab);
    }
    public void encounter(GameObject encounter) {
        thisRigidbody.velocity = Vector3.zero;
        battleMangerObject = Instantiate(battleManagerPrefab);
        BattleManager manager = battleMangerObject.GetComponent<BattleManager>();
        characterUi = manager.CharacterUI;
        summonTargets = manager.summonTargets;
        manager.allControllers[0].thisCharacter = this;
        for (int i = 1; i < manager.allControllers.Length; i++)
            characterControllers[i - 1] = manager.allControllers[i];
        Enemy enemyObject = Instantiate(encounter).GetComponent<Enemy>();
        manager.enemyUI[0].GetComponent<EnemyTarget>().thisCharacter = enemyObject;
        manager.enemyUI[0].GetComponent<Image>().sprite = enemyObject.GetComponent<Enemy>().myArt;
        manager.enemyUI[0].thisCharacter.maxAttaba = 100;
        manager.enemyUI[0].thisCharacter.attaba = 100;
        foreach (EnemyTarget enemyUImember in manager.enemyUI)
        {
            enemyUImember.theirProtagonist = enemyObject;
        }
        for (int i = 0; i < enemyObject.activePets.Length; i++)
        {
            manager.enemyUI[i + 1].GetComponent<EnemyTarget>().thisCharacter = enemyObject.activePets[i];
            enemyObject.activePets[i].maxAttaba = 100;
            enemyObject.activePets[i].attaba = 100;
            manager.enemyUI[i + 1].GetComponent<Image>().sprite = enemyObject.activePets[i].myArt;
        }
        enemyObject.enemyTargets = manager.enemyUI;
        enemyObject.playerscript = this;
        this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled= false;
        //enemyPrefab

    }
    public void endFight() {
        for (int i = 0; i < activePets.Length; i++)
            activePets[i] = null;
        Destroy(battleMangerObject);
        this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    public override void killPokemon(Pet oldYeller)
    {
        characterControllers[findPokemonIndexCharacterController(oldYeller)].thisCharacter = null;
        //activePets[findPokemonIndexActivePet(oldYeller)] = null;
        removeFromActivePets(findPokemonIndexActivePet(oldYeller));
        if (activeCharacter == oldYeller) {
            activeCharacter = null;
            activeMove = null;
        }
    }
    
    public void save(string fileNameExtension) {
        
        string path = "Assets/" + fileNameExtension+".txt";
        File.Delete(path);
        StreamWriter reader = new StreamWriter(path);
        reader.WriteLine(characterName);
        reader.WriteLine(this.gameObject.transform.position.x+"@"+this.transform.position.y);
        reader.WriteLine(SceneManager.GetActiveScene().name);
        reader.WriteLine(level+"-"+xp+"-");
        reader.WriteLine(myBalls.Length+"-"+moveSet.Length);
        //reader.Write("\n");
        for (int i = 0; i < moveSet.Length; i++)
        {
            reader.Write(moveSet[i].nameOfMove+","+moveSet[i].cost + "," + moveSet[i].timeCost+"-");
        }
        reader.WriteLine();
        for (int i = 0; i < myBalls.Length; i++) {
            reader.WriteLine(myBalls[i].characterName+"-"+myBalls[i].moveSet.Length+"-"+myBalls[i].level+"-"+myBalls[i].energy + "-"+myBalls[i].maxEnergy+"-" 
                + myBalls[i].strength+"-"+myBalls[i].dexterity+"-"+myBalls[i].luck+ "-"+myBalls[i].wisdom);
            for(int j = 0; j<myBalls[i].moveSet.Length;j++)
                reader.WriteLine(myBalls[i].moveSet[j].nameOfMove + "-"+ myBalls[i].moveSet[j].cost+"-"+ myBalls[i].moveSet[j].timeCost);
        }
        reader.WriteLine(storySwitches.Length);
        for (int i = 0; i < storySwitches.Length; i++)
            reader.WriteLine("-"+ storySwitches[i]);
        reader.Write("endOfFile");
        reader.Close();
    }

    public void load(string fileNameExtension)
    {
        string path = "Assets/" + fileNameExtension + ".txt";
        input = "";
        StreamReader reader = new StreamReader(path);
        characterName = reader.ReadLine();
        input = reader.ReadLine();
        this.transform.position = new Vector2(getAndProcessFloat(input), getAndProcessFloat(input));
        input = reader.ReadLine();
        SceneManager.LoadScene(input);
        input = reader.ReadLine();
        level = getAndProcessInt(input);
        xp = getAndProcessInt(input);
        input = reader.ReadLine();
        int numberOfMoves, numberOfBalls;
        numberOfBalls = getAndProcessInt(input);
        numberOfMoves = getAndProcessInt(input);
        input = reader.ReadLine();
        foreach (Move i in moveSet) {
            Destroy(i);
        }
        foreach (Pet i in myBalls)
        {
            destroyPet(i);
        }

        for (int i = 0; i < numberOfMoves; i++)
        {
            loadProtagMove(i, getAndProcess(input));
        }
        for (int i = 0; i < numberOfBalls; i++)
        {
            input = reader.ReadLine();
            Pet adding = loadPokemon(input);
            for (int j = 0; j < adding.moveSet.Length; j++)
            {
                input = reader.ReadLine();
                adding.moveSet[j] = loadPetMove(input);
            }
        }
        input = reader.ReadLine();

        storySwitches = new bool[getAndProcessInt(input)];
        for (int i = 0; i < storySwitches.Length; i++)
            storySwitches[i] = (getAndProcess(input) == "true");
        reader.Close();
    }
    //TODO need to add ability to moveset
    public void loadProtagMove(int iteration, string input) {
        string ripped = input.Substring(0, input.IndexOf(','));
        input = input.Substring(input.IndexOf(',') + 1);
        Debug.Log(ripped);
        Debug.Log(input);
        if (ripped == "Summon" || ripped == "Defend" || ripped == "Focus" || ripped == "Punch")
        {
            Debug.Log("this place");
            if (ripped == "Summon")
            {
                Summon adding =
                this.gameObject.AddComponent<Summon>();
                adding.nameOfMove = "Summon";

                Debug.Log(input.Substring(0, input.IndexOf(",")));
                adding.cost = int.Parse(input.Substring(0, input.IndexOf(",")));
                input = input.Substring(input.IndexOf(",") + 1);
                Debug.Log(input);
                adding.timeCost = int.Parse(input);
                moveSet[iteration] = adding;
            }
            else if (ripped == "Defend")
            {
                Defend adding =
                this.gameObject.AddComponent<Defend>();
                adding.nameOfMove = "Defend";

                Debug.Log(input.Substring(0, input.IndexOf(",")));
                adding.cost = int.Parse(input.Substring(0, input.IndexOf(",")));
                input = input.Substring(input.IndexOf(",") + 1);
                Debug.Log(input);
                adding.timeCost = int.Parse(input);
                moveSet[iteration] = adding;
            }
            else if (ripped == "Focus")
            {
                Focus adding =
                this.gameObject.AddComponent<Focus>();
                adding.nameOfMove = "Focus";

                Debug.Log(input.Substring(0, input.IndexOf(",")));
                adding.cost = int.Parse(input.Substring(0, input.IndexOf(",")));
                input = input.Substring(input.IndexOf(",") + 1);
                Debug.Log(input);
                adding.timeCost = int.Parse(input);
                moveSet[iteration] = adding;
            }
            else if (ripped == "Punch")
            {
                punch adding =
                this.gameObject.AddComponent<punch>();
                adding.nameOfMove = "Punch";

                Debug.Log(input.Substring(0, input.IndexOf(",")));
                adding.cost = int.Parse(input.Substring(0, input.IndexOf(",")));
                input = input.Substring(input.IndexOf(",") + 1);
                Debug.Log(input);
                adding.timeCost = int.Parse(input);
                moveSet[iteration] = adding;
            }
        }

        else if (ripped == "Thor")
        {
            LightningBolt adding =
            this.gameObject.AddComponent<LightningBolt>();
            adding.nameOfMove = "Thor";
            adding.cost = int.Parse(input.Substring(input.IndexOf(",")));
            input = input.Substring(input.IndexOf(","));
            adding.timeCost = int.Parse(input.Substring(input.IndexOf(",")));

        }

    }
    public Move loadPetMove(string inputVal)
    {
        Move returner = null;
        string checker = getAndProcess(inputVal);
        if (checker == "summon")
        {
            returner = this.gameObject.AddComponent<Summon>();
        }
        else {
            returner = this.gameObject.AddComponent<punch>();
        }
        returner.cost = getAndProcessInt(input);
        returner.timeCost = getAndProcessInt(input);
        return returner;

    }
    public Pet loadPokemon(string inputVal)
    {
        input = inputVal;
        Debug.Log(input);
        string nameOfAdd = getAndProcess(input);
        Pet adding = this.gameObject.AddComponent<Pet>();
        adding.characterName = nameOfAdd;
        adding.moveSet= new Move[getAndProcessInt(input)];
        adding.level = getAndProcessInt(input);
        adding.energy = getAndProcessInt(input);
        adding.maxEnergy = getAndProcessInt(input);
        adding.strength = getAndProcessInt(input);
        adding.dexterity = getAndProcessInt(input);
        adding.luck = getAndProcessInt(input);
        adding.wisdom = getAndProcessInt(input);
        return adding;

    }
    //need to process
    public string getAndProcess(string inputVal) {
        char[] chars = {'-'};

        //char[] chars = { '-', '\n' };
        string returner = input;
        if (input == "endOfFile")
            Debug.Log("ABOUT TO CRASH");

        if (input.IndexOfAny(chars) == -1) {
            return returner;
        }
        else {
            returner = input.Substring(0, input.IndexOfAny(chars));
            input = input.Substring(input.IndexOfAny(chars) + 1);
        }
        return returner;
    }
    public int getAndProcessInt(string inputVal) {
        string returner = getAndProcess(inputVal);
        return int.Parse(returner);
    }
    public float getAndProcessFloat(string inputVal)
    {
        char[] chars = { '@' };

        string returner = inputVal;
        //char[] chars = { '-', '\n' };
        float result = 0;
        if (input.IndexOfAny(chars) == -1)
        {
        }
        else
        {
            returner = input.Substring(0, input.IndexOfAny(chars));
            input = input.Substring(input.IndexOfAny(chars) + 1);

        }
        result = float.Parse(returner);
        return result;
    }
    public void destroyPet(Pet thisPet) {
        foreach (Move i in thisPet.moveSet) {
            Destroy(i);
        }
        Destroy(thisPet);
    }

}