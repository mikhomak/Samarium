using UnityEngine;

namespace DefaultNamespace.Tricks
{
    public abstract class ATrick : ITrick
    {

        protected Plane plane;
        protected PlaneMovement planeMovement;
        protected TrickManager trickManager;

        protected float currentTrickScore;
        protected bool close;
        protected bool highSpeed;
        protected const float CLOSE_MULTIPLIER = 2f;
        protected const float HIGH_SPEED_MULTIPLIER = 1.5f;
        protected bool Active;
        protected bool preventedFromStop;
        protected bool stoppingSoon;
        protected float finishTimerTime = 1.5f;

        protected ATrick(Plane plane, PlaneMovement planeMovement, TrickManager trickManager)
        {
            this.plane = plane;
            this.planeMovement = planeMovement;
            this.trickManager = trickManager;
        }

        public virtual void StartTrick()
        {
            if (stoppingSoon) {
                stoppingSoon = false;
                preventedFromStop = true;
                return;
            }

            Active = true;
        }

        public abstract void UpdateTrick();

        public virtual bool FinishTrick()
        {
            if (preventedFromStop) {
                return false;
            }

            currentTrickScore = 0;
            Active = false;
            return true;
        }

        public virtual void FinishTrickWithTimer()
        {
            TimerManager.Instance.startTimer(finishTimerTime, FinishTrick);
            preventedFromStop = false;
            stoppingSoon = true;
        }

        public float GetCurrentScore()
        {
            return currentTrickScore;
        }

        protected void CancelFinishTimer()
        {
            stoppingSoon = false;
            preventedFromStop = true;
        }

        public bool IsActive()
        {
            return Active;
        }

        public void SetClose(bool close)
        {
            this.close = close;
        }

        public void SetHighSpeed(bool highSpeed)
        {
            this.highSpeed = highSpeed;
        }

        public bool GetClose()
        {
            return close;
        }

        public bool GetHighSpeed()
        {
            return highSpeed;
        }
    }
}