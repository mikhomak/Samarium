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
        protected float closeMultiplier = 2f;
        protected float highSpeedMultiplier = 1.5f;
        protected bool Active;
        protected bool preventedFromStop;
        protected bool stoppingSoon;
        protected float finishTimerTime = 3f;

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
                this.close = close;
                this.highSpeed = highSpeed;
                return;
            }

            Active = true;
            this.close = close;
            this.highSpeed = highSpeed;
        }

        public abstract void UpdateTrick();

        public virtual bool FinishTrick()
        {
            if (preventedFromStop) {
                return false;
            }

            currentTrickScore = 0;
            Active = false;
            this.close = false;
            this.highSpeed = false;
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
    }
}