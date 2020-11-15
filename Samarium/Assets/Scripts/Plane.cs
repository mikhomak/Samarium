using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;

public class Plane : MonoBehaviour
{

    [SerializeField] private Rigidbody rbd;
    [SerializeField] private Stats stats;
    [SerializeField] private InputMaster inputMaster;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Animator animator;
    [SerializeField] private TrailRenderer trailLeftTR;
    [SerializeField] private TrailRenderer trailRightTR;
    [SerializeField] private AudioSource driftSource;
    [SerializeField] private ParticleSystem hitPS;



    private bool stopAudioManagement;
    
    public TrickManager TrickManager { get; set; }
    public PlaneMovement PlaneMovement { get; set; }
    public PlaneAnimatorFacade PlaneAnimatorFacade { get; set; }

    private void Awake()
    {
        RegisterInputs();
        InitComponents();
    }



    private void InitComponents()
    {
        if (rbd == null) {
            rbd = GetComponent<Rigidbody>();
        }

        if (animator == null) {
            animator = GetComponentInChildren<Animator>();
        }

        PlaneMovement = new PlaneMovement(this, rbd, stats, transform);
        TrickManager = new TrickManager(levelManager, this);
        PlaneMovement.PostConstruct();
        PlaneAnimatorFacade = new PlaneAnimatorFacade(animator);
    }

    private void RegisterInputs()
    {
        inputMaster = new InputMaster();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }

    public void UpdateHighSpeed(bool highSpeed)
    {
        TrickManager.SetHighSpeed(highSpeed);
    }
    private void FixedUpdate()
    {
        float rollInput = inputMaster.Player.Roll.ReadValue<float>();
        float pitchInput = inputMaster.Player.Pitch.ReadValue<float>();
        float yawnInput = inputMaster.Player.Yawn.ReadValue<float>();
        PlaneMovement.PitchInput(pitchInput);
        PlaneMovement.RollInput(rollInput);
        PlaneMovement.YawnInput(yawnInput);
        PlaneMovement.ThrustInput(inputMaster.Player.Thrust.ReadValue<float>());
        PlaneMovement.Movement();
        TrickManager.TickTricks();
        PlaneAnimatorFacade.SetPitch(pitchInput);
        PlaneAnimatorFacade.SetRoll(rollInput);
        PlaneAnimatorFacade.SetYawn(yawnInput);
    }

    private void OnCollisionEnter(Collision other)
    {
        levelManager.ResetCurrentScore();
        hitPS.Emit(150);
    }

    public void EnableDriftTraces()
    {
        stopAudioManagement = true;
        trailLeftTR.emitting = true;
        trailRightTR.emitting = true;
        StartCoroutine(ProgressivelyIncreaseVolume());
    }
    
    public void DisableDriftTraces()
    {
        stopAudioManagement = true;
        trailLeftTR.emitting = false;
        trailRightTR.emitting = false; 
        StartCoroutine(ProgressivelyDecreaseVolume());
    }

    private IEnumerator ProgressivelyIncreaseVolume()
    {
        stopAudioManagement = false;
        while (Math.Abs(driftSource.volume - 0.85f) > 0.1f) {
            if (stopAudioManagement) {
                yield break;
            }
            driftSource.volume += 0.1f;
            yield return new WaitForSeconds(0.2f);
        }
    }    
    
    private IEnumerator ProgressivelyDecreaseVolume()
    {
        stopAudioManagement = false;
        while (Math.Abs(driftSource.volume - 0f) > 0.01f) {
            if (stopAudioManagement) {
                yield break;
            }
            driftSource.volume -= 0.1f;
            yield return new WaitForSeconds(0.2f);
        }
    }
}