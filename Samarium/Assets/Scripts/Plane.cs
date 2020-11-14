using System;
using DefaultNamespace;
using UnityEngine;

public class Plane : MonoBehaviour
{

    [SerializeField] private Rigidbody rbd;
    [SerializeField] private Stats stats;
    [SerializeField] private InputMaster inputMaster;
    [SerializeField] private LevelManager levelManager;

    [Header("Score")] private int closeMultiplier = 2;
    private float highSpeedMultiplier = 1.5f;

    public TrickManager TrickManager { get; set; }
    public PlaneMovement PlaneMovement { get; set; }

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

        PlaneMovement = new PlaneMovement(this, rbd, stats, transform);
        TrickManager = new TrickManager(levelManager, this);
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

    private void FixedUpdate()
    {
        PlaneMovement.PitchInput(inputMaster.Player.Pitch.ReadValue<float>());
        PlaneMovement.RollInput(inputMaster.Player.Roll.ReadValue<float>());
        PlaneMovement.YawnInput(inputMaster.Player.Yawn.ReadValue<float>());
        PlaneMovement.ThrustInput(inputMaster.Player.Thrust.ReadValue<float>());
        PlaneMovement.Movement();
        TrickManager.TickTricks();
    }


}