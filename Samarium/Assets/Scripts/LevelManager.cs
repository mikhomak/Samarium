using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float score = 0;
    [SerializeField]private Text scoreText;
    [SerializeField]private Text currentTrickScoreText;


    public void UpdateCurrentTrick(float currentScore)
    {
        currentTrickScoreText.text = currentScore + "!";
    }
    
    public void AddScore(float addedScore)
    {
        this.score += addedScore;
        scoreText.text = "Your score is: " + this.score;
    }
}
