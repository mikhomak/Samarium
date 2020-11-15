using System;
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

    [Header("Score")] private int closeMultiplier = 2;
    private float highSpeedMultiplier = 1.5f;

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
    }

    public void EnableDriftTraces()
    {
        trailLeftTR.emitting = true;
        trailRightTR.emitting = true;
    }
    
    public void DisableDriftTraces()
    {
        trailLeftTR.emitting = false;
        trailRightTR.emitting = false;
    }
}