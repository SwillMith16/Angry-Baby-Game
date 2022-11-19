using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject HUD;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // close pause menu, show HUD
        pauseMenuUI.SetActive(false);
        HUD.SetActive(true);

        // unfreeze the game
        Time.timeScale = 1f;

        // set isPaused to false
        isPaused = false;
    }

    void Pause()
    {
        // open pause menu, hide HUD
        pauseMenuUI.SetActive(true);
        HUD.SetActive(true);

        // freeze the game
        Time.timeScale = 0f;

        // set isPaused to true
        isPaused = true;
    }

    public void mainMenu()
    {
        // go back to main menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        // close the game
        Debug.Log("Quit game");
        Application.Quit();
    }
}
