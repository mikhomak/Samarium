using UnityEngine;

namespace DefaultNamespace
{
    public class PlaneAnimatorFacade
    {
        private Animator animator;
        private static readonly int Pitch = Animator.StringToHash("pitch");
        private static readonly int Roll = Animator.StringToHash("roll");

        public PlaneAnimatorFacade(Animator animator)
        {
            this.animator = animator;
        }

        public void SetPitch(float pitch)
        {
            /*if (pitch == 0) {
                return;
            }
            animator.Play(pitch > 0.1 ? "pitch" : "pitch_down");*/
            animator.SetFloat(Pitch, pitch);
        }
        
        public void SetRoll(float roll)
        {
            /*if (roll == 0) {
                return;
            }
            animator.Play(roll > 0.1 ? "roll_right" : "roll_left");*/
            animator.SetFloat(Roll, roll);
        }
    }
}