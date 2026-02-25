using UnityEngine;


namespace RPGGame
{

    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator refAnimator;

        private void OnEnable()
        {
            if (refAnimator == null)
            {
                refAnimator = GetComponentInParent<Animator>();
            }
        }

        public void OnStateChanged(PlayerStateManager.State newState)
        {
            if (refAnimator == null) return;
            
            refAnimator.SetInteger("State", (int) newState);
        }

        public void OnLanding()
        {
            if (refAnimator == null) return;
            refAnimator.SetTrigger("Landing");
        }

        public void SetAttackComboState(int comboState)
        {
            if (refAnimator == null) return;
            refAnimator.SetInteger("AttackCombo", comboState);
        }

        // 현재 재생 중인 애니메이션 스테이트를 반환하는 함수
        public AnimatorStateInfo GetCurrentStateInfo()
        {
            // 애니메이터에서 재생하는 스테이트 정보를 반환
            return refAnimator.GetCurrentAnimatorStateInfo(0);
        }

    }

}
