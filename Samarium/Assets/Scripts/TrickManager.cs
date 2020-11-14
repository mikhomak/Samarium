namespace DefaultNamespace
{
    public class TrickManager
    {
        private LevelManager levelManager;
        private float currentTrickScore;
        private bool close;
        private bool highSpeed;
        private float closeMultiplier = 2f;
        private float highSpeedMultiplier = 1.5f;
        private bool active;

        public TrickManager(LevelManager levelManager)
        {
            this.levelManager = levelManager;
        }

        public void StartNewTrick(bool close, bool highSpeed)
        {
            active = true;
            this.close = close;
            this.highSpeed = highSpeed;
        }

        public void TickTrickManger()
        {
            if (active) {
                float scoreToAdd = 1;
                if (close) {
                    scoreToAdd *= closeMultiplier;
                }

                if (highSpeed) {
                    scoreToAdd *= highSpeedMultiplier;
                }

                currentTrickScore += scoreToAdd;
            }
            levelManager.UpdateCurrentTrick(currentTrickScore);
        }

        public void StopCurrentTrick()
        {
            levelManager.AddScore(currentTrickScore);
            currentTrickScore = 0;
            active = false;
            this.close = false;
            this.highSpeed = false;
        }

        public void AddClose()
        {
            close = true;
        }

        public void AddHighSpeed()
        {
            highSpeed = true;
        }

        public bool Active
        {
            get => active;
            set => active = value;
        }
    }
}