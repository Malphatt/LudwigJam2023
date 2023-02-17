using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuController : MonoBehaviour
{
    public GameObject creditsPanel;
    //Start button
    public void StartGame()
    {
        //Load the first level
        SceneManager.LoadScene(1);
    }

    //Credits button
    public void ShowCredits()
    {
        //Show the credits panel
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }

    //Quit button
    public void QuitGame()
    {
        //Quit the game
        Application.Quit();
    }


}
