using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Stats : MonoBehaviour {
    // CONSTS
    public const float ACCELERATION_THROTTLE_DOWN = 200f;
    public const float ACCELERATION_NO_THROTTLE = 200f;
    public const float ACCELERATION_THROTTLE_UP = 200f;
    public const float AIR_CONTROL = 200f;
    public const float AERODYNAMIC = 200f;
    public const float LERP_VALUE_VELOCITY = 200f;
    
    
    public float pitchControl = 200f;
    public float yawnControl = 200f;
    public float rollControl = 200f;
    
    public float maxSpeed = 400f;
}