using UnityEngine;


namespace RPGGame
{
    public class MonsterHitSMB : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            MonsterAnimationController animationController = animator.GetComponent<MonsterAnimationController>();
            if (animationController != null)
            {
                animationController.OnDamaged();
            }
        }
    }

}
