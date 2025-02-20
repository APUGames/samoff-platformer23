using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Public instead of private, because the button in Unity needs to access it
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
