using System;
using System.Collections;
using DefaultNamespace.Tricks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public float score = 0;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject driftTextGameObject;
    [SerializeField] private GameObject driftCloseGameObject;
    [SerializeField] private GameObject driftHighSpeedGameObject;
    [SerializeField] private GameObject specialMoveGameObject;
    [SerializeField] private Text currentTrickScoreText;
    [SerializeField] private float currentTrickScore;
    [SerializeField] private Animator animator;

    private Vector3 initialDriftTextPos;
    private Vector3 initialDriftCloseTextPos;
    private Vector3 initialDriftHighSpeedTextPos;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        initialDriftTextPos = driftTextGameObject.transform.position;
        initialDriftCloseTextPos = driftCloseGameObject.transform.position;
        initialDriftHighSpeedTextPos = driftHighSpeedGameObject.transform.position;
        StartCoroutine(JerkDebugText());
    }

    public void UpdateCurrentTrick(float currentScore)
    {
        currentTrickScore += currentScore;
        currentTrickScoreText.text = currentTrickScore + "!";
    }

    public void UpdateDriftClose(bool close)
    {
        animator.Play(close ? "CloseOn" : "CloseOff"); // why tf no who is gonna stop me
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


    private IEnumerator JerkDebugText()
    {
        for (;;) {
            driftTextGameObject.transform.position = new Vector3(initialDriftTextPos.x + Random.Range(-4, 4),
                initialDriftTextPos.y + Random.Range(-4, 4));         
            driftCloseGameObject.transform.position = new Vector3(initialDriftCloseTextPos.x + Random.Range(-4, 4),
                initialDriftCloseTextPos.y + Random.Range(-4, 4));            
            driftHighSpeedGameObject.transform.position = new Vector3(initialDriftHighSpeedTextPos.x + Random.Range(-4, 4),
                initialDriftHighSpeedTextPos.y + Random.Range(-4, 4));
            yield return new WaitForSeconds(0.05f);
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