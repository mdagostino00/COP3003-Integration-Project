// Robert McNiven
// Main Menu operations as of now.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Need to use this to change scenes within uniity
using UnityEngine.SceneManagement;

// These classes need to be public because we need to be able to access them when the button objects are clicked.
public class MainMenu : MonoBehaviour
{
    // Public function that is applied to the "PLAY" button object so that
    // when clicked, it will play the main game loop.
    public void PlayGame()
    {
        // Calling the LoadScene function from the SceneManager class
        SceneManager.LoadScene("Main");
    }

    // Public function that is applied to the "QUIT" button object so that
    // when clicked, the application will close
    public void QuitGame()
    {
        Debug.Log("Quit");
        // Calling the Quit function from the Application class
        Application.Quit();
    }
}
