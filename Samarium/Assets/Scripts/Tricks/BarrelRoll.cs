using UnityEngine;
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
        }


        public override void StartTrick()
        {
            if (Active) {
                return;
            }

            initialRotation = plane.transform.rotation.z;
            success = false;
            firstStage = false;
            Active = true;
            FinishTrickWithTimer();
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


    }
}