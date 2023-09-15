using UnityEngine;

namespace CountMasters.Game.Crowd.Mob
{
    public class MobAnimator
    {
        private Animator _animator;
        private const string AnimatorRunTrigger = "IsRun";
        private readonly int IsRun = Animator.StringToHash(AnimatorRunTrigger);

        public MobAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void SetActive(bool value) => _animator.enabled = value;

        public void SetAnimation(MobAnimation mobAnimation)
        {
            switch (mobAnimation)
            {
                case MobAnimation.Idle:
                    _animator.SetBool(IsRun, false);
                    break;
                case MobAnimation.Run:
                    _animator.SetBool(IsRun, true);
                    break;
            }
        }
        
        public enum MobAnimation : byte
        {
            Idle,
            Run
        }
    }
}