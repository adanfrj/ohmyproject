using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

    [Header("Score Highlight")]
    public int scoreHighlightRange;
    public CharacterSoundController sound;
    private int currentScore = 0;
    private int lastScoreHighlight = 0;

    // Start is called before the first frame update
    private void Start()
    {
        //reset
        currentScore = 0;
        lastScoreHighlight = 0;
    }

    // Update is called once per frame
    public float GetCurrentScore()
    {
        return currentScore;
    }

    public void TambahScore (int increment)
    {
        currentScore += increment;
        if (currentScore - lastScoreHighlight > scoreHighlightRange)
        {
            sound.PlayScoreHighlight();
            lastScoreHighlight += scoreHighlightRange;
        }
    }

    public void FinishScoring()
    {
        //set highscore
        if (currentScore > ScoreData.highScore)
        {
            ScoreData.highScore = currentScore;
        }
    }

    
}
