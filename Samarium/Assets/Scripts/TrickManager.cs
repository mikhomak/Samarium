using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Tricks;

namespace DefaultNamespace
{
    public class TrickManager
    {
        private LevelManager levelManager;
        private Plane plane;

        private List<ITrick> tricks;
        public DriftTrick DriftTrick { get; set; }

        public TrickManager(LevelManager levelManager, Plane plane)
        {
            this.levelManager = levelManager;
            this.plane = plane;
            tricks = new List<ITrick>();
            DriftTrick = new DriftTrick(plane, plane.PlaneMovement, this);
            tricks.Add(DriftTrick);
        }

        public void UpdateContinuousUi(IContinuousTrick continuousTrick)
        {
            levelManager.UpdateCurrentTrick(continuousTrick.ToString(), continuousTrick.GetCurrentScore());
        }

        public void ReleaseContinuousUiText()
        {
            levelManager.ReleaseCurrentTrick();
        }
        
        public void TickTricks()
        {
            tricks.ForEach(trick =>
            {
                if (trick.IsActive()) {
                    trick.UpdateTrick();
                }
            });
        }

        public void SetClose(bool close)
        {
            tricks.ForEach(trick => trick.SetClose(close));
        }

        public void SetHighSpeed(bool highSpeed)
        {
            tricks.ForEach(trick => trick.SetHighSpeed(highSpeed));
        }
    }
}