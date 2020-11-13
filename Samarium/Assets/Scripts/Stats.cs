using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Stats : MonoBehaviour
{
    // CONSTS 
    [Header("Physics")] 
    public float maxAcceleration = 10f;
    public float minAcceleration = -10f;
    public float accelerationThrottleDown = 2f;
    public float accelerationNoThrottle = 1f;
    public float accelerationThrottleUp = 3f;
    [Header("Speed")]
    public float airControl = 40f;
    public float aerodynamic = 200f;
    public float lerpValueVelocity = 0.014f;
    public float maxSpeedLerpValue = 0.4f;
    public float minSpeed = 100f;
    public float maxSpeed = 1500f;

    [Header("Input Controls")] public float pitchControl = 1f;
    public float yawnControl = 1f;
    public float rollControl = 1f;

}