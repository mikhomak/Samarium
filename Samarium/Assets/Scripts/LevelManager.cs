using DefaultNamespace.Tricks;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float score = 0;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject driftCloseGameObject;
    [SerializeField] private GameObject driftHighSpeedGameObject;
    [SerializeField] private GameObject specialMoveGameObject;
    [SerializeField] private Text currentTrickScoreText;
    [SerializeField] private float currentTrickScore;

    public void UpdateCurrentTrick(float currentScore)
    {
        currentTrickScore += currentScore;
        currentTrickScoreText.text = currentTrickScore + "!";
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

    public void AddSpecialMove(ISpecialTrick specialTrick)
    {
        currentTrickScore *= specialTrick.GetSpecialTrickMultiplier();
        specialMoveGameObject.SetActive(true);
        currentTrickScoreText.text = currentTrickScore + "!";
    }

    public void AddScore(float addedScore)
    {
        this.score += addedScore;
        scoreText.text = "Your score is: " + this.score;
    }
}