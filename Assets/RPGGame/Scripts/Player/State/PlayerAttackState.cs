using UnityEngine;
using UnityEngine.Events;


namespace RPGGame
{
    public class PlayerAttackState : PlayerStateBase
    {
        // 공격판정을 시작할 때 호출하는 이벤트
        [SerializeField] private UnityEvent OnAttackBegin;
        // 공격판정을 끝낼 때 호출하는 이벤트
        [SerializeField] private UnityEvent OnAttackCheckEnd;
        // 공격을 종료할 때 발행할 이벤트
        [SerializeField] private UnityEvent OnAttackEnd;

        protected override void OnDisable()
        {
            base.OnDisable();

            animationController.SetAttackComboState((int)PlayerStateManager.AttackCombo.None);
        }

        private void AttackStart()
        {
            OnAttackBegin?.Invoke();
        }

        public void SubscribeOnAttackBegin(UnityAction listener)
        {
            OnAttackBegin?.AddListener(listener);
        }

        private void AttackCheckEnd()
        {
            OnAttackCheckEnd?.Invoke();
        }

        public void SubscribeOnAttackCheckEnd(UnityAction listener)
        {
            OnAttackCheckEnd?.AddListener(listener);
        }

        private void ComboCheck()
        {
            animationController.SetAttackComboState((int)manager.NextAttackCombo);
        }

        private void AttackEnd()
        {
            OnAttackEnd?.Invoke();
            manager.SetState(PlayerStateManager.State.Idle);
        }

        public void SubscribeOnAttackEnd(UnityAction listener)
        {
            OnAttackEnd?.AddListener(listener);
        }

    }
}