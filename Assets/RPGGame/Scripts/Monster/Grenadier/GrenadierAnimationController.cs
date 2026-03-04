using System;
using UnityEngine;
using static UnityEngine.CullingGroup;


namespace RPGGame
{
    public class GrenadierAnimationController : MonoBehaviour
    {
        private Animator refAnimator;

        private void Awake()
        {
            if (refAnimator == null)
            {
                refAnimator = transform.root.GetComponentInChildren<Animator>();
            }

            GrenadierStateManager manager = transform.root.GetComponentInChildren<GrenadierStateManager>();

            if (manager != null)
            {
                manager.SubscriveOnStateChanged(OnStateChanged);
                manager.SubscriveOnAttackTypeChanged(OnAttackTypeChanged);
            }
        }

        public void SetAngle(float angle)
        {
            if (refAnimator == null) { return; }
            refAnimator.SetFloat("Angle", angle);
        }

        public void Hit()
        {
            if (refAnimator == null) { return; }
            refAnimator.SetTrigger("Hit");
        }

        public void ResetHit()
        {
            if (refAnimator == null) { return; }
            refAnimator.ResetTrigger("Hit");
        }


        private void OnAttackTypeChanged(GrenadierStateManager.AttackType attackType)
        {
            if (refAnimator == null) { return; }
            refAnimator.SetInteger("AttackType", (int)attackType);
        }

        private void OnStateChanged(GrenadierStateManager.State state)
        {
            if (refAnimator == null) { return; }

            refAnimator.SetInteger("State", (int)state);
        }
    }

}
