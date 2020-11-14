namespace DefaultNamespace.Tricks
{
    public abstract class ASpecialTrick : ISpecialTrick
    {
        protected bool success;
        protected Plane plane;
        protected PlaneMovement planeMovement;
        protected TrickManager trickManager;

        protected float trickScoreMultiplier;
        protected bool Active;
        protected float finishTimerTime = 30f;

        protected ASpecialTrick(Plane plane, PlaneMovement planeMovement, TrickManager trickManager)
        {
            this.plane = plane;
            this.planeMovement = planeMovement;
            this.trickManager = trickManager;
        }

        public virtual void StartTrick()
        {
            if (Active) {
                return;
            }
            Active = true;
        }

        public abstract void UpdateTrick();

        public virtual bool FinishTrick()
        {
            Active = false;
            if (success) {
                trickManager.AddSpecialMove();
            }
            return true;
        }

        public virtual void FinishTrickWithTimer()
        {
            TimerManager.Instance.startTimer(finishTimerTime, FinishTrick);
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
    }
}