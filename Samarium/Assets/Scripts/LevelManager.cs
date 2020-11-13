using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int score = 0;
    [SerializeField]private Text scoreText;


    public void AddScore(int addedScore)
    {
        this.score += addedScore;
        scoreText.text = "Your score is: " + this.score;
    }
}
