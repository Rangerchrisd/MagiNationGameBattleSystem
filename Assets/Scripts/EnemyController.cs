using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Character thisCharacter;
    public Protagonist YourCharacter;
    // Start is called before the first frame update
    void Start()
    {
        thisCharacter = this.gameObject.GetComponent<Character>();
        YourCharacter = FindObjectOfType<Protagonist>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thisCharacter.attaba <= 0) {
            if (YourCharacter.activePets.Length != 0)
            {
                thisCharacter.moveSet[Random.Range(0, thisCharacter.moveSet.Length)].useAbility(thisCharacter,
                    YourCharacter.activePets[Random.Range(0, thisCharacter.moveSet.Length)]);
            }
            else
            {
                thisCharacter.moveSet[Random.Range(0, thisCharacter.moveSet.Length)].useAbility(thisCharacter, YourCharacter);
            }
                
        }

    }
}
