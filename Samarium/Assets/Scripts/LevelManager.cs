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
    [SerializeField] private GameObject barrelRollGameObject;
    [SerializeField] private Text currentTrickScoreText;
    [SerializeField] private float currentTrickScore;
    [SerializeField] private Animator animator;
    [SerializeField] public float jerkSpeed = 0.05f;

    private Vector3 initialDriftTextPos;
    private Vector3 initialDriftCloseTextPos;
    private Vector3 initialDriftHighSpeedTextPos;
    private Vector3 initialBarrelRollTextPos;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        initialDriftTextPos = driftTextGameObject.transform.position;
        initialDriftCloseTextPos = driftCloseGameObject.transform.position;
        initialDriftHighSpeedTextPos = driftHighSpeedGameObject.transform.position;
        initialBarrelRollTextPos = barrelRollGameObject.transform.position;
        StartCoroutine(JerkDebugText());
    }

    public void UpdateCurrentTrick(float currentScore)
    {
        currentTrickScore += currentScore;
        currentTrickScoreText.text = currentTrickScore + "";
    }

    public void UpdateDriftClose(bool close)
    {
        animator.Play(close ? "CloseOn" : "CloseOff"); // why tf no who is gonna stop me
    }

    public void UpdateDriftHighSpeed(bool highSpeed)
    {
        animator.Play(highSpeed ? "HighSpeedOn" : "HighSpeedOff"); // why tf no who is gonna stop me
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
            barrelRollGameObject.transform.position = new Vector3(initialBarrelRollTextPos.x + Random.Range(-4, 4),
                initialBarrelRollTextPos.y + Random.Range(-4, 4));
    
            yield return new WaitForSeconds(jerkSpeed);
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
        animator.Play("BarrelRoll_On");
        currentTrickScoreText.text = currentTrickScore + "";
        StartCoroutine(BarrelRollOff());
    }


    
    private IEnumerator BarrelRollOff()
    {
        yield return new WaitForSeconds(2f);
        animator.Play("BarrelRoll_Off");
    }
    
    public void AddScore(float addedScore)
    {
        this.score += addedScore;
        scoreText.text = "Your score is: " + this.score;
    }
}