using UnityEngine;

namespace DefaultNamespace
{
    public class PlaneAnimatorFacade
    {
        private Animator animator;
        private static readonly int Pitch = Animator.StringToHash("pitch");
        private static readonly int Roll = Animator.StringToHash("roll");
        private static readonly int Yawn = Animator.StringToHash("yawn");

        public PlaneAnimatorFacade(Animator animator)
        {
            this.animator = animator;
        }

        public void SetPitch(float pitch)
        {

            animator.SetFloat(Pitch, pitch);
        }
        
        public void SetRoll(float roll)
        {

            animator.SetFloat(Roll, roll);
        }        
        
        public void SetYawn(float yawn)
        {
            animator.SetFloat(Yawn, yawn);
        }
    }
}