using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public int timeCost;
    public float cost;
    public string nameOfMove;
    public bool zeroTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void ability(Character user, Character target) { 
    
    }
    public virtual void useAbility(Character user,Character target) {
        if (user.attaba <= 0)
        {
            ability(user, target);
            user.attaba += timeCost;
            user.maxAttaba = timeCost;
        }
    }
}
