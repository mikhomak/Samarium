﻿using UnityEngine;
using UnityEngine.Assertions.Comparers;

namespace DefaultNamespace.Tricks
{
    public class BarrelRoll : ASpecialTrick
    {

        private float axisRotation;
        private float initialRotation;
        private bool firstStage;


        public BarrelRoll(Plane plane, PlaneMovement planeMovement, TrickManager trickManager) : base(plane,
            planeMovement, trickManager)
        {
            trickScoreMultiplier = 3f;
        }


        public override void StartTrick()
        {
            if (canStart) {
                if (Active) {
                    return;
                }

                canStart = false;
                initialRotation = plane.transform.rotation.z;
                success = false;
                firstStage = false;
                Active = true;
                FinishTrickWithTimer();
                Debug.Log("sd");
            }
        }


        public override void UpdateTrick()
        {
            axisRotation = plane.transform.rotation.z;
            if (Mathf.Abs(axisRotation) > 0.5 && !firstStage) {
                firstStage = true;
            }

            if (firstStage && FloatComparer.AreEqual(initialRotation, axisRotation, 0.05f) ){
                success = true;
                FinishTrick();
                return;
            }
        }


        public override string ToString()
        {
            return "Barrel Roll";
        }

    }
}