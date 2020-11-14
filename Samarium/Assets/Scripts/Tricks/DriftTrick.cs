using UnityEngine;

namespace DefaultNamespace.Tricks
{
    public class DriftTrick : ATrick, IContinuousTrick
    {

        public DriftTrick(Plane plane, PlaneMovement planeMovement, TrickManager trickManager) : base(plane,
            planeMovement, trickManager)
        {
        }

        public override void UpdateTrick()
        {
            bool drifting = planeMovement.IsDrifting();

            if (!drifting && !stoppingSoon) {
                Debug.Log("gonna stop");
                FinishTrickWithTimer();
            }
            else if (drifting) {
                if (stoppingSoon) {
                }
                CancelFinishTimer();

                currentTrickScore += CalculateContinuousScore();
                trickManager.UpdateContinuousUi( CalculateContinuousScore());
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
            Debug.Log("nice");
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