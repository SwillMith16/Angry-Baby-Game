using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public static float scoreCount;
    public GameObject scoreObject;
    private TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        score = scoreObject.GetComponent<TextMeshProUGUI>();
        scoreCount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = scoreCount.ToString();
    }
}
