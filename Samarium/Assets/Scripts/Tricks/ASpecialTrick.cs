using System.Collections;
using UnityEngine;

namespace DefaultNamespace.Tricks
{
    public abstract class ASpecialTrick : ISpecialTrick
    {
        protected bool success;
        protected Plane plane;
        protected PlaneMovement planeMovement;
        protected TrickManager trickManager;

        protected float trickScoreMultiplier = 2f;
        protected bool Active;
        protected float finishTimerTime = 2f;
        protected float cooldown = 1.5f;

        protected bool canStart = true;
        
        protected ASpecialTrick(Plane plane, PlaneMovement planeMovement, TrickManager trickManager)
        {
            this.plane = plane;
            this.planeMovement = planeMovement;
            this.trickManager = trickManager;
        }

        public abstract void StartTrick();

        public abstract void UpdateTrick();

        public virtual bool FinishTrick()
        {
            if (Active) {
                Active = false;
                if (success) {
                    trickManager.AddSpecialMove(this);
                }
            }

            canStart = false;
            plane.StartCoroutine(Cooldown());
            return true;
        }

        public virtual void FinishTrickWithTimer()
        {
            //TimerManager.Instance.startTimer(finishTimerTime, FinishTrick);
            plane.StartCoroutine(Finish());
        }

        protected IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(cooldown);
            canStart = true;
        }
        
        protected IEnumerator Finish()
        {
            yield return new WaitForSeconds(finishTimerTime);
            FinishTrick();
        }
        
        
        public float GetCurrentScore()
        {
            return success ? trickScoreMultiplier : 0;
        }


        public bool IsActive()
        {
            return Active;
        }

        public void SetClose(bool close)
        {
        }

        public void SetHighSpeed(bool highSpeed)
        {
        }

        public bool GetClose()
        {
            return false;
        }

        public bool GetHighSpeed()
        {
            return false;
        }

        public void SetSuccess(bool success)
        {
            this.success = success;
        }

        public float GetSpecialTrickMultiplier()
        {
            return trickScoreMultiplier;
        }
    }
}