using System;
using System.Collections;
using System.Numerics;
using DefaultNamespace.Tricks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class LevelManager : MonoBehaviour
{

    public float score = 0;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;
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
    [SerializeField] private Animation doubleScoreAnimation;
    [SerializeField] private Text currentTrickScoreText;
    [SerializeField] private float currentTrickScore;
    [SerializeField] public float jerkSpeed = 0.05f;
    [SerializeField] public float endTimer = 60.0f;
    [SerializeField] public float timer;
    [SerializeField] public float scoreToWin = 5000.0f;
    [SerializeField] private GameObject screenOverGO;
    [SerializeField] private Text screenOverText;
    [SerializeField] private Transform playerSpawnPos;
    [SerializeField] private GameObject player;

    [SerializeField] private AudioClip barrelAudioClip;
    [SerializeField] private AudioClip cobraAudioClip;

    [SerializeField] public AudioSource specialEffectsAudioSource;
    [SerializeField] public AudioSource musicLoopAudioSource;

    [SerializeField] private AudioSource crowdAudioSource;
    // sue me see if i care

    private Vector3 initialDriftTextPos;
    private Vector3 initialDriftCloseTextPos;
    private Vector3 initialDriftHighSpeedTextPos;
    private Vector3 initialBarrelRollTextPos;
    private Vector3 initialCobraFlipTextPos;


    private bool gameOver;

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
        Time.timeScale = 1;
    }

    public void UpdateCurrentTrick(float currentScore)
    {
        currentTrickScore += currentScore;
        currentTrickScoreText.text = ((int) currentTrickScore).ToString();
    }


    private void FixedUpdate()
    {
        endTimer -= Time.deltaTime;
        timerText.text = ((int) endTimer).ToString();
        if (endTimer < 0) {
            player.GetComponent<Plane>().DisableDriftTraces();
            gameOver = true;
            TimeOver();
        }
    }

    private void TimeOver()
    {
        AddScore(currentTrickScore);
        Time.timeScale = 0;
        screenOverGO.SetActive(true);
        musicLoopAudioSource.Stop();
        if (score > scoreToWin) {
            screenOverText.text = "YOU WON";
        }
        else {
            screenOverText.text = "YOU LOST";
        }
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
            yield return new WaitForSeconds(currentTrickScore > 1000 ? jerkSpeed : 0.01f);
        }
    }

    public void ResetCurrentScore()
    {
        currentTrickScore = 0;
        currentTrickScoreText.text = ((int) currentTrickScore).ToString();
    }

    public void DoubleCurrentScore()
    {
        currentTrickScore *= 2;
        currentTrickScoreText.text = ((int) currentTrickScore).ToString();
        doubleScoreAnimation.Play("doubleScoreOn");
        StartCoroutine(DoubleScoreOff());
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
            currentTrickScoreText.text = ((int) currentTrickScore).ToString();
        }

        if (specialTrick is BarrelRoll) {
            specialEffectsAudioSource.PlayOneShot(barrelAudioClip, 0.9f);
            barrelRollAnimation.Play("BarrelRollOn");
            StartCoroutine(BarrelRollOff());
        }
        else {
            specialEffectsAudioSource.PlayOneShot(cobraAudioClip, 0.9f);
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
    
    private IEnumerator DoubleScoreOff()
    {
        yield return new WaitForSeconds(2f);
        doubleScoreAnimation.Play("doubleScoreOff");

    }

    public void AddScore(float addedScore)
    {
        this.score += addedScore;
        scoreText.text = "Your score is: \n" + ((int) this.score);
        var volume = Remap(score, 0f, 5000f, 0, 0.1f);
        crowdAudioSource.volume = volume > 0.2f ? 0.2f : volume;
    }

    private float Remap(float value, float inA, float inB, float outA, float outB)
    {
        return outA + (value - inA) * (outB - outA) / (inB - inA);
    }

    public void RestartGame()
    {
        gameOver = false;
        score = 0;
        endTimer = 60;
        Time.timeScale = 1;
        player.transform.position = playerSpawnPos.position;
        player.transform.rotation = Quaternion.identity;
        musicLoopAudioSource.Play();
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        screenOverGO.SetActive(false);
        
        scoreText.text = "Your score is: \n" + ((int) this.score);
    }

    public void Menu()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }
}