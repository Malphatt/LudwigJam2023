using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuController : MonoBehaviour
{
    //Return to menu button
    public void ReturnToMenu()
    {
        //Load the menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    //Quit Game button
    public void QuitGame()
    {
        //Quit the game
        Application.Quit();
    }
}
