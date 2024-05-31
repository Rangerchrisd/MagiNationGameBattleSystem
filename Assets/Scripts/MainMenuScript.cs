using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class MainMenuScript : MonoBehaviour
{
    public GameObject ProtagPrefab;
    public void startGame() {

        //Instantiate(ProtagPrefab);
        SceneManager.LoadScene("Scene0");

    }
    public void loadGame(string fileNameExtension) {
        string path = "Assets/"+fileNameExtension;
        StreamReader reader = new StreamReader(path);
        if (reader.EndOfStream) { 
        
        
        }
    }

}
