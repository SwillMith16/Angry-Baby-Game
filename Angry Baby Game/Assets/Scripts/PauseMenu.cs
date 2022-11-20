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

        // lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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

        // unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // freeze the game
        Time.timeScale = 0f;

        // set isPaused to true
        isPaused = true;
    }

    public void mainMenu()
    {
        // unfreeze the game
        Time.timeScale = 1f;

        // go back to main menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        isPaused = false;
    }

    public void QuitGame()
    {
        // close the game
        Debug.Log("Quit game");
        Application.Quit();
    }
}
