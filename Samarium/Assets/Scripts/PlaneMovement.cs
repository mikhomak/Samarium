using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlaneMovement
    {

        private readonly Plane plane;
        private readonly Rigidbody rbd;
        private readonly Stats stats;
        private readonly Transform transform;

        private const float FLOAT_TOLERANCE = 0.01f;

        public PlaneMovement(Plane plane, Rigidbody rbd, Stats stats, Transform transform)
        {
            this.plane = plane;
            this.rbd = rbd;
            this.stats = stats;
            this.transform = transform;
        }

        public void Movement()
        {
            // Adding torque to compensate torque of pitch/roll/yawn
            rbd.AddTorque(rbd.angularVelocity * -1f / 0.5f);
        }

        public void PitchInput(float inputVal)
        {
            AddTorqueToThePlane(transform.right, inputVal * stats.pitchControl);
        }

        public void RollInput(float inputVal)
        {
            AddTorqueToThePlane(transform.forward, inputVal * stats.rollControl);
        }
        public void YawnInput(float inputVal)
        {
            AddTorqueToThePlane(transform.up, inputVal * stats.yawnControl);
        }


        private void AddTorqueToThePlane(Vector3 direction, float inputVal)
        {
            if (Math.Abs(inputVal) > FLOAT_TOLERANCE) {
                var tiltVector = Vector3.Lerp(Vector3.zero, direction * (inputVal * stats.airControl), 0.1f);
                rbd.AddTorque(tiltVector);
            }
        }
    }
}