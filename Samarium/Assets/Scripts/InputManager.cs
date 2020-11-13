using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static float getYawnInput() {
        return Input.GetAxis("Yawn");
    }

    public static float getPitchInput() {
        return Input.GetAxis("Pitch");
    }

    public static float getRollInput() {
        return Input.GetAxis("Roll");
    }

    public static float getThrustInput() {
        return Input.GetAxis("Thrust");
    }
}
