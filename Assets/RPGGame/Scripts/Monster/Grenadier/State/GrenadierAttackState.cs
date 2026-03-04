using UnityEngine;
using UnityEngine.Events;


namespace RPGGame
{
    public class GrenadierAttackState : GrenadierStateBase
    {
        [SerializeField]
        private UnityEvent OnMeleeAttackStart;
        [SerializeField]
        private UnityEvent OnMeleeAttackCheckStart;
        [SerializeField]
        private UnityEvent OnRangeAttackStart;
        [SerializeField]
        private UnityEvent OnAttackEnd;

        private GrenadierAnimationController animationController;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (animationController == null)
            {
                animationController = GetComponentInChildren<GrenadierAnimationController>();
            }

            if (manager.isPlayerDead)
            {
                manager.SetState(GrenadierStateManager.State.Idle);
            }

            if (manager.currentAttackType == GrenadierStateManager.AttackType.MeleeAttack)
            {
                OnMeleeAttackStart?.Invoke();
            }
            /*
            else if (manager.currentAttackType == GrenadierStateManager.AttackType.RangeAttack)
            {
                OnRangeAttackStart?.Invoke();
            }
            */

        }

        protected override void Update()
        {
            base.Update();
            
            manager.RotateToPlayer();

            if (!Util.IsInSight(refTransform, manager.playerTransform, manager.data.sightAngle, manager.data.sightRange))
            {
                manager.SetState(GrenadierStateManager.State.Idle);
            }
        }

        public void StartAttack()
        {
            if(manager.currentAttackType == GrenadierStateManager.AttackType.MeleeAttack)
            {
                OnMeleeAttackCheckStart?.Invoke();
            }
        }

        private void ActivateShield()
        {
            Debug.Log("ActivateShield");
            OnRangeAttackStart?.Invoke();
        }

        private void EndAttack()
        {
            Debug.Log("EndAttack");
            manager.SetState(GrenadierStateManager.State.Idle);
            OnAttackEnd?.Invoke();

            animationController.ResetHit();
        }

        public void SubscribeOnMeleeAttackStart(UnityAction listener)
        {
            OnMeleeAttackStart.AddListener(listener);
        }

        public void SubscribeOnMeleeAttackCheckStart(UnityAction listener)
        {
            OnMeleeAttackCheckStart.AddListener(listener);
        }

        public void SubscribeOnRangeAttackStart(UnityAction listener)
        {
            OnRangeAttackStart.AddListener(listener);
        }

        public void SubscribeOnAttackEnd(UnityAction listener)
        {
            OnAttackEnd.AddListener(listener);
        }

    }

}
