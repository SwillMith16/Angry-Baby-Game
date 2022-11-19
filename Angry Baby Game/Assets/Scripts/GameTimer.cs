using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float gameLengthSeconds;
    public static float secondsLeft;
    public GameObject timerObject;
    private TextMeshProUGUI timer;

    private void Start()
    {
        secondsLeft = gameLengthSeconds;
        timer = timerObject.GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        // count down seconds
        secondsLeft -= Time.deltaTime;
        UpdateTimer(secondsLeft);

        // at 0 seconds, go to Game Over scene
        if (secondsLeft <= 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void UpdateTimer(float timeLeft)
    {
        timeLeft = Mathf.FloorToInt(timeLeft);
        timer.text = timeLeft.ToString();
    }
}
