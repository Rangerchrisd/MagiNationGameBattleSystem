using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSummon : MonoBehaviour
{
    public Summon summonScript;
    public int i;
    // Start is called before the first frame update
    public void useThis() {
        summonScript.input = i;
    }
}
