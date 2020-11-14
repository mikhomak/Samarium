using System;
using DefaultNamespace.Tricks;
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

        private float currentThrust;
        private bool thrustUp;
        private float dot;
        private bool isDrifting;

        private BarrelRoll barrelRoll;
        private DriftTrick driftTrick;

        public PlaneMovement(Plane plane, Rigidbody rbd, Stats stats, Transform transform)
        {
            this.plane = plane;
            this.rbd = rbd;
            this.stats = stats;
            this.transform = transform;
        }

        public void PostConstruct()
        {
            barrelRoll = plane.TrickManager.BarrelRoll;
            driftTrick = plane.TrickManager.DriftTrick;
        }

        public void Movement()
        {
            // Adding torque to compensate torque of pitch/roll/yawn
            rbd.AddTorque(rbd.angularVelocity * -1f / 0.5f);
            Thrusting();
            CalculateAerodynamics();
            //Debug.Log(currentThrust);
        }

        public void PitchInput(float inputVal)
        {
            var appliedControl = !thrustUp ? stats.pitchControlThrustDown : stats.pitchControl;
            AddTorqueToThePlane(Vector3.right, inputVal * appliedControl);
        }

        public void RollInput(float inputVal)
        {
            var appliedControl = thrustUp ? stats.rollControlThrustUp : stats.rollControl;
            AddTorqueToThePlane(Vector3.forward, inputVal * appliedControl);
            if (!barrelRoll.IsActive()) {
                barrelRoll.StartTrick();
            }
        }

        public void YawnInput(float inputVal)
        {
            AddTorqueToThePlane(Vector3.up, inputVal * stats.yawnControl);
        }

        public void ThrustInput(float inputVal)
        {
            if (inputVal < 0) {
                thrustUp = false;
                currentThrust -= stats.accelerationThrottleDown;
            }
            else if (inputVal > 0) {
                thrustUp = true;
                currentThrust += stats.accelerationThrottleUp;
            }
            else {
                thrustUp = false;
                currentThrust -= stats.accelerationNoThrottle;
            }

            currentThrust = Mathf.Clamp(currentThrust, stats.minAcceleration, stats.maxAcceleration);
        }

        private void AddTorqueToThePlane(Vector3 direction, float inputVal)
        {
            if (Math.Abs(inputVal) > FLOAT_TOLERANCE) {
                //var tiltVector = Vector3.Lerp(Vector3.zero, direction * (inputVal * stats.airControl), 0.1f);
                //rbd.AddTorque(tiltVector, ForceMode.VelocityChange);
                transform.Rotate(direction * (stats.rotationSpeed * inputVal * Time.deltaTime));
            }
        }

        private void Thrusting()
        {
            float speed = currentThrust > stats.maxSpeed
                ? Mathf.Lerp(stats.maxSpeed, currentThrust, stats.maxSpeedLerpValue)
                : Mathf.Clamp(currentThrust, stats.minSpeed, stats.maxSpeed);

            Vector3 velocity = Vector3.Lerp(rbd.velocity, transform.forward * speed, stats.lerpValueVelocity);
            rbd.velocity = velocity;
        }

        private void CalculateAerodynamics()
        {
            var velocity = rbd.velocity.normalized;
            var upVector = transform.up;
            float dotProduct = Vector3.Dot(upVector, velocity);
            //Debug.Log(Mathf.Cos(Vector3.Angle(upVector ,velocity)));
            Debug.DrawLine(transform.position,
                transform.position + velocity * (Mathf.Cos(Vector3.Angle(upVector, velocity)) * stats.aerodynamic),
                Color.red);
            //Debug.Log(dotProduct);
            if (dotProduct < -0.4f) {
                rbd.AddForce(velocity * (Mathf.Cos(Vector3.Angle(upVector, velocity)) * stats.aerodynamic));
            }

            if (Mathf.Abs(dotProduct) > 0.4f && !isDrifting) {
                isDrifting = true;
            }
            else if (Mathf.Abs(dotProduct) < 0.4f && isDrifting) {
                isDrifting = false;
            }
            HasDotChanged(dotProduct);
        }

        private void HasDotChanged(float newDot)
        {
            if (driftTrick.IsActive()) {
                return;
            }

            var absPreviousDot = Mathf.Abs(dot);
            var absDot = Mathf.Abs(newDot);
            if (absPreviousDot < 0.4f && absDot > 0.4f) {
                driftTrick.StartTrick();
                Debug.Log("new dot");
            }
        }

        public bool IsDrifting()
        {
            return isDrifting;
        }


    }
}