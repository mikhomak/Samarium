﻿using System;
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
            Thrusting();
            CalculateAerodynamics();
            //Debug.Log(currentThrust);
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

        public void ThrustInput(float inputVal)
        {
            if (inputVal < 0) {
                currentThrust -= stats.accelerationThrottleDown;
            }
            else if (inputVal > 0) {
                currentThrust += stats.accelerationThrottleUp;
            }
            else {
                currentThrust -= stats.accelerationNoThrottle;
            }

            currentThrust = Mathf.Clamp(currentThrust, stats.minAcceleration, stats.maxAcceleration);
        }

        private void AddTorqueToThePlane(Vector3 direction, float inputVal)
        {
            if (Math.Abs(inputVal) > FLOAT_TOLERANCE) {
                var tiltVector = Vector3.Lerp(Vector3.zero, direction * (inputVal * stats.airControl), 0.1f);
                rbd.AddTorque(tiltVector);
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
            if (dotProduct < 0) {
                rbd.AddForce(velocity * (dotProduct * stats.aerodynamic));
            }
        }
    }
}