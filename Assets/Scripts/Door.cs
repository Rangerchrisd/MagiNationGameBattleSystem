using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string SceneName;
    public Vector2 exitLocation;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Hi");
        if (collision.gameObject.tag == "Player")
        {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.LoadScene(SceneName);
            collision.gameObject.transform.position = exitLocation;
            Destroy(this.gameObject);
        }
    }
}
