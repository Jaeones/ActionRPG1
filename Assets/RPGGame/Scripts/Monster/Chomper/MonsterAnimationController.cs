using System;
using UnityEngine;

namespace RPGGame
{
    public class MonsterAnimationController : MonoBehaviour
    {
        private Animator refAnimator;

        private void Awake()
        {
            if ( refAnimator == null)
            {
                refAnimator = GetComponentInParent<Animator>();
            }

            MonsterStateManager manager = GetComponentInParent<MonsterStateManager>();

            if (manager != null)
            {
                manager.SubscribeOnstateChanged(OnStateChanged);
            }
        }

        public void OnStateChanged(MonsterStateManager.State state)
        {
            if (refAnimator == null) { return; }

            refAnimator.SetInteger("State", (int)state);

        }

        public void OnDamaged()
        {
            refAnimator.SetTrigger("Hit");

        }
    }

}
