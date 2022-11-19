using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    public GameObject scoreObject;
    private TextMeshProUGUI score;
    private float scoreCount;

    void Start()
    {
        score = scoreObject.GetComponent<TextMeshProUGUI>();
        scoreCount = ScoreTracker.scoreCount;
        score.text = scoreCount.ToString();
    }
}
