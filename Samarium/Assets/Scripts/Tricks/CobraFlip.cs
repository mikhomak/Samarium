using UnityEngine;
using UnityEngine.Assertions.Comparers;

namespace DefaultNamespace.Tricks
{
    public class CobraFlip : ASpecialTrick
    {
        
        private float axisRotation;
        private float initialRotation;
        private bool firstStage;


        public CobraFlip(Plane plane, PlaneMovement planeMovement, TrickManager trickManager) : base(plane,
            planeMovement, trickManager)
        {
            trickScoreMultiplier = 1.5f;
            finishTimerTime = 4;
            cooldown = 3f;
        }


        public override void StartTrick()
        {
            if (canStart) {
                if (Active) {
                    return;
                }

                canStart = false;
                initialRotation = plane.transform.rotation.x;
                success = false;
                firstStage = false;
                Active = true;
                FinishTrickWithTimer();
            }
        }


        public override void UpdateTrick()
        {
            axisRotation = plane.transform.rotation.x;
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
            return "Cobra flip";
        }
    }
}