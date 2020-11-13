using System;
using DefaultNamespace;
using UnityEngine;

public class Plane : MonoBehaviour
{

    [SerializeField] private Rigidbody rbd;
    [SerializeField] private Stats stats;
    [SerializeField] private InputMaster inputMaster;

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
        inputMaster.Player.Pitch.performed += ctx => planeMovement.PitchInput(ctx.ReadValue<float>());
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
//        planeMovement.RollInput(InputManager.GetRollInput());
  //      planeMovement.YawnInput(InputManager.GetYawnInput());
    }
}