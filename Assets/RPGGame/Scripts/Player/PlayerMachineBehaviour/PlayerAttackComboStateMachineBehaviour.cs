using UnityEngine;

namespace RPGGame
{
    public class PlayerAttackComboStateMachineBehaviour : StateMachineBehaviour
    {
        [SerializeField] private int effectIndex = -1;

        private WeaponController weaponController;

        // 상태에 진입할 때 파티클 효과 이펙트를 같이 재생하도록 처리
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (effectIndex == -1) return;
            if (weaponController == null)
            {
                weaponController = animator.GetComponentInChildren<WeaponController>();
            }

            weaponController.PlayAttackComboEffect(effectIndex);
        }
    }
}
