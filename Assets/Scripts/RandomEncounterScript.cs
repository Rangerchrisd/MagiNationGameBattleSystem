using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounterScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Hi");
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Protagonist>().encounter(enemyPrefab);
            Destroy(this.gameObject);
        }
    }
}
