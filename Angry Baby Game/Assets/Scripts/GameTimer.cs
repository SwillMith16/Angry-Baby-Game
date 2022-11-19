using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float gameLengthSeconds;
    void Update()
    {
        // count down seconds
        gameLengthSeconds -= Time.deltaTime;

        // at 0 seconds, go to Game Over scene
        if (gameLengthSeconds <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
