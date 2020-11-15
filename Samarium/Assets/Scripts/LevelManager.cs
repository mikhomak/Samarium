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
    [SerializeField] private GameObject cobraFlipGameObject;    
    [SerializeField] private Animation driftTextAnimation; // wow
    [SerializeField] private Animation driftCloseAnimation; // cool as shit
    [SerializeField] private Animation driftHighSpeedAnimation;
    [SerializeField] private Animation barrelRollAnimation;
    [SerializeField] private Animation cobraFlipAnimation;
    [SerializeField] private Text currentTrickScoreText;
    [SerializeField] private float currentTrickScore;
    [SerializeField] public float jerkSpeed = 0.05f;

    private Vector3 initialDriftTextPos;
    private Vector3 initialDriftCloseTextPos;
    private Vector3 initialDriftHighSpeedTextPos;
    private Vector3 initialBarrelRollTextPos;
    private Vector3 initialCobraFlipTextPos;

    private void Awake()
    {
        initialDriftTextPos = driftTextGameObject.transform.position;
        initialDriftCloseTextPos = driftCloseGameObject.transform.position;
        initialDriftHighSpeedTextPos = driftHighSpeedGameObject.transform.position;
        initialBarrelRollTextPos = barrelRollGameObject.transform.position;
        initialCobraFlipTextPos = cobraFlipGameObject.transform.position;
        
        driftCloseAnimation = driftCloseGameObject.GetComponent<Animation>();
        driftHighSpeedAnimation = driftHighSpeedAnimation.GetComponent<Animation>();
        barrelRollAnimation = barrelRollGameObject.GetComponent<Animation>();
        cobraFlipAnimation = cobraFlipGameObject.GetComponent<Animation>();
        
        StartCoroutine(JerkDebugText());
    }

    public void UpdateCurrentTrick(float currentScore)
    {
        currentTrickScore += currentScore;
        currentTrickScoreText.text = currentTrickScore + "";
    }

    public void UpdateDriftClose(bool close)
    {
        if (close && currentTrickScore != 0) {
            driftCloseAnimation.Play("CloseOn"); // why tf no who is gonna stop me
        }
        else if (!close) {
            driftCloseAnimation.Play("CloseOff");
        }
    }

    public void UpdateDriftHighSpeed(bool highSpeed)
    {
        if (highSpeed && currentTrickScore != 0) {
            driftHighSpeedAnimation.Play("HighSpeedOn");
        }
        else if (!highSpeed) {
            driftHighSpeedAnimation.Play("HighSpeedOff");
        }
    }


    private IEnumerator JerkDebugText()
    {
        for (;;) {
            driftTextGameObject.transform.position = new Vector3(initialDriftTextPos.x + Random.Range(-4, 4),
                initialDriftTextPos.y + Random.Range(-4, 4));
            driftCloseGameObject.transform.position = new Vector3(initialDriftCloseTextPos.x + Random.Range(-4, 4),
                initialDriftCloseTextPos.y + Random.Range(-4, 4));
            driftHighSpeedGameObject.transform.position = new Vector3(
                initialDriftHighSpeedTextPos.x + Random.Range(-4, 4),
                initialDriftHighSpeedTextPos.y + Random.Range(-4, 4));
            barrelRollGameObject.transform.position = new Vector3(initialBarrelRollTextPos.x + Random.Range(-4, 4),
                initialBarrelRollTextPos.y + Random.Range(-4, 4));
            cobraFlipGameObject.transform.position = new Vector3(initialCobraFlipTextPos.x + Random.Range(-4, 4),
                initialCobraFlipTextPos.y + Random.Range(-4, 4));
            Debug.Log("es");
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
        if (currentTrickScore != 0) {
            currentTrickScoreText.text = currentTrickScore + "";
        }

        if(specialTrick is BarrelRoll) {
            barrelRollAnimation.Play("BarrelRollOn");
            StartCoroutine(BarrelRollOff());
        }
        else {
            cobraFlipAnimation.Play("CobraFlip_ON");
            StartCoroutine(CobraFlipOff());
        }
    }


    private IEnumerator BarrelRollOff()
    {
        yield return new WaitForSeconds(2f);
        barrelRollAnimation.Play("BarrelRollOff");
    }
    
    private IEnumerator CobraFlipOff()
    {
        yield return new WaitForSeconds(2f);
        cobraFlipAnimation.Play("CobraFlip_off");
    }

    public void AddScore(float addedScore)
    {
        this.score += addedScore;
        scoreText.text = "Your score is: " + this.score;
    }



    public void OnBarrelOn()
    {
        barrelRollGameObject.transform.localScale=Vector3.one;
    }
    public void OnBarrelOff()
    {
        barrelRollGameObject.transform.localScale=Vector3.zero;
    }
}