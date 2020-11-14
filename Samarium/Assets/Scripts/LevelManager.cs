using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float score = 0;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject driftCloseGameObject;
    [SerializeField] private GameObject driftHighSpeedGameObject;
    [SerializeField] private Text currentTrickScoreText;
    [SerializeField] private float currentTrickScore;

    public void UpdateCurrentTrick(string name, float currentScore)
    {
        currentTrickScoreText.text = name + " " + currentScore + "!";
        currentTrickScore = currentScore;
    }

    public void UpdateDriftClose(bool close)
    {
        if (!close && driftCloseGameObject.active) {
            driftCloseGameObject.SetActive(false);
        }
        else if (close && !driftCloseGameObject.active) {
            driftCloseGameObject.SetActive(true);
        }
    }

    public void UpdateDriftHighSpeed(bool highSpeed)
    {
        if (!highSpeed && driftHighSpeedGameObject.active) {
            driftHighSpeedGameObject.SetActive(false);
        }
        else if (highSpeed && !driftHighSpeedGameObject.active) {
            driftHighSpeedGameObject.SetActive(true);
        }
    }

    public void ReleaseCurrentTrick()
    {
        currentTrickScoreText.text = "";
        AddScore(currentTrickScore);
        currentTrickScore = 0;
    }

    public void AddScore(float addedScore)
    {
        this.score += addedScore;
        scoreText.text = "Your score is: " + this.score;
    }
}