using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Tricks;
using UnityEngine;

namespace DefaultNamespace
{
    public class TrickManager
    {
        private LevelManager levelManager;
        private Plane plane;

        private List<ITrick> tricks;
        public DriftTrick DriftTrick { get; set; }
        public BarrelRoll BarrelRoll { get; set; }
        public CobraFlip CobraFlip { get; set; }

        public TrickManager(LevelManager levelManager, Plane plane)
        {
            this.levelManager = levelManager;
            this.plane = plane;
            tricks = new List<ITrick>();
            DriftTrick = new DriftTrick(plane, plane.PlaneMovement, this);
            BarrelRoll = new BarrelRoll(plane, plane.PlaneMovement, this);
            CobraFlip = new CobraFlip(plane, plane.PlaneMovement, this);
            tricks.Add(DriftTrick);
            tricks.Add(BarrelRoll);
            tricks.Add(CobraFlip);
        }

        public void UpdateContinuousUi(float addedValue)
        {
            levelManager.UpdateCurrentTrick(addedValue);
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

        public void AddSpecialMove(ISpecialTrick specialTrick)
        {
            levelManager.AddSpecialMove(specialTrick);
        }

        public void SetClose(bool close)
        {
            tricks.ForEach(trick => trick.SetClose(close));
            levelManager.UpdateDriftClose(close);
        }

        public void SetHighSpeed(bool highSpeed)
        {
            tricks.ForEach(trick => trick.SetHighSpeed(highSpeed));
            levelManager.UpdateDriftHighSpeed(highSpeed);
        }
    }
}