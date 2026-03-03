using System;
using UnityEngine;


namespace RPGGame
{
    public class NPCAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator refAnimator;

        private void OnEnable()
        {
            if (refAnimator == null)
            {
                refAnimator = transform.parent.GetComponent<Animator>();
                var stateManager = transform.parent.GetComponent<NPCStateManager>();
                if (stateManager)
                {
                    stateManager.SubscribeOnStateChanged(OnStateChanged);
                }
            }
        }

        private void OnStateChanged(NPCStateManager.State state)
        {
            if (refAnimator == null)
            {
                return;
            }
            refAnimator.SetInteger("State", (int)state);
        }
    }
}
