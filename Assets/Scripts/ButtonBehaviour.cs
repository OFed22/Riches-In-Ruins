using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{

    public GameObject options;
    public GameObject mainMenu;

    public void closeGame()
    {
        Debug.Log("Game Closed");
    }

    public void startGame()
    {
        Debug.Log("Game Started");
    }
    public void optionsMenu()
    {
        
        options.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void goToMainMenu()
    {
        options.SetActive(false);
        mainMenu.SetActive(true);
    }
}
