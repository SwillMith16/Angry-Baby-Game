using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    public GameObject scoreObject;
    private TextMeshProUGUI scoreText;
    private float scoreCount;
    public GameObject highScoreObject;
    private TextMeshProUGUI highScoreText;
    private float highScoreCount;  
    

    void Start()
    {
        highScoreCount = PlayerPrefs.GetFloat("HighScore");
        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        scoreCount = ScoreTracker.scoreCount;
        scoreText.text = scoreCount.ToString();

        if (scoreCount > highScoreCount)
        {
            PlayerPrefs.SetFloat("HighScore", scoreCount);
        }

        highScoreText = highScoreObject.GetComponent<TextMeshProUGUI>();
        highScoreCount = PlayerPrefs.GetFloat("HighScore");
        highScoreText.text = highScoreCount.ToString();
    }
}
