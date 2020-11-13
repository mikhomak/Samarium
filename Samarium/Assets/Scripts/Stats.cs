using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Stats : MonoBehaviour
{
    // CONSTS 
    [Header("Physics")] public float accelerationThrottleDown = 200f;
    public float accelerationNoThrottle = 200f;
    public float accelerationThrottleUp = 200f;
    public float airControl = 40f;
    public float aerodynamic = 200f;
    public float lerpValueVelocity = 200f;

    [Header("Input Controls")] public float pitchControl = 1f;
    public float yawnControl = 1f;
    public float rollControl = 1f;

    public float maxSpeed = 400f;
}