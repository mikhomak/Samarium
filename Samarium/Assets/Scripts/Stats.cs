using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Stats : MonoBehaviour
{
    // CONSTS 
    [Header("Physics")] 
    public float maxAcceleration = 90f;
    public float minAcceleration = -10f;
    public float accelerationThrottleDown = -50f;
    public float accelerationNoThrottle = -40f;
    public float accelerationThrottleUp = 90f;
    [Header("Speed")]
    public float airControl = 40f;
    public float aerodynamic = 150f;
    public float lerpValueVelocity = 0.014f;
    public float maxSpeedLerpValue = 0.4f;
    public float minSpeed = 1;
    public float maxSpeed = 120f;

    [Header("Input Controls")] 
    public float pitchControl = 1f;
    public float pitchControlThrustDown = 1.6f;
    public float yawnControl = 1f;
    public float rollControl = 1f;
    public float rollControlThrustUp = 1.6f;

}