using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour{


    public void startGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void closeGame(){
        
        Debug.Log("QUIT!!");
        Application.Quit();
    }

    public void backtoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
