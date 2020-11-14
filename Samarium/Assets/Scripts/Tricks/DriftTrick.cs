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
                scoreToAdd *= CLOSE_MULTIPLIER;
            }

            if (highSpeed) {
                scoreToAdd *= HIGH_SPEED_MULTIPLIER;
            }

            return scoreToAdd;
        }


        public override bool FinishTrick()
        {
            if (base.FinishTrick()) {
                trickManager.ReleaseContinuousUiText();
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return "Drift";
        }


    }
}