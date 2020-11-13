using System;
using DefaultNamespace;
using UnityEngine;

public class Plane : MonoBehaviour
{

    [SerializeField] private Rigidbody rbd;
    [SerializeField] private Stats stats;
    [SerializeField] private InputMaster inputMaster;
    [SerializeField] private LevelManager levelManager;

    private PlaneMovement planeMovement;

    private void Awake()
    {
        InitComponents();
        RegisterInputs();
    }

    private void InitComponents()
    {
        if (rbd == null) {
            rbd = GetComponent<Rigidbody>();
        }

        planeMovement = new PlaneMovement(this, rbd, stats, transform);
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
        planeMovement.PitchInput(inputMaster.Player.Pitch.ReadValue<float>());
        planeMovement.RollInput(inputMaster.Player.Roll.ReadValue<float>());
        planeMovement.YawnInput(inputMaster.Player.Yawn.ReadValue<float>());
        planeMovement.ThrustInput(inputMaster.Player.Thrust.ReadValue<float>());
        planeMovement.Movement();
    }

    public void AddScore(int addedScore)
    {
        levelManager.AddScore(addedScore);
    }
}