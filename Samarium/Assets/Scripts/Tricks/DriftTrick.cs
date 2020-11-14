using UnityEngine;

namespace DefaultNamespace.Tricks
{
    public class DriftTrick : ATrick, IContinuousTrick
    {

        public DriftTrick(Plane plane, PlaneMovement planeMovement, TrickManager trickManager) : base(plane, planeMovement, trickManager)
        {
            Active = true;
        }
        
        public override void UpdateTrick()
        {

            if (!planeMovement.IsDrifting() && !stoppingSoon) {
                FinishTrickWithTimer();
            }
            else if(planeMovement.IsDrifting()) {
                if (stoppingSoon) {
                    CancelFinishTimer();
                }
                currentTrickScore += CalculateContinuousScore();
                trickManager.UpdateContinuousUi(this);
            }
        }


        public float CalculateContinuousScore()
        {
            float scoreToAdd = 1;
            if (close) {
                scoreToAdd *= closeMultiplier;
            }

            if (highSpeed) {
                scoreToAdd *= highSpeedMultiplier;
            }

            return scoreToAdd;
        }

        public override string ToString()
        {
            return "Drift";
        }


    }
}