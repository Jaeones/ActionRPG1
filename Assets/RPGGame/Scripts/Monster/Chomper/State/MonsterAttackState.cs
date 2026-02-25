using UnityEngine;
using UnityEngine.Events;

namespace RPGGame
{
    public class MonsterAttackState : MonsterStateBase
    {
        [SerializeField] private UnityEvent OnAttackBegin;
        [SerializeField] private UnityEvent OnAttackEnd;

        protected override void Update()
        {
            base.Update();

            Vector3 direction = manager.playerTransform.position - refTransform.position;
            direction.y = 0f;

            if (Vector3.Distance(refTransform.position, manager.playerTransform.position) > monsterData.attackRange)
            {
                manager.SetState(MonsterStateManager.State.Chase);
            }
        }

        public void AttackBegin()
        {
            OnAttackBegin?.Invoke();
        }

        public void AttackEnd()
        {
            OnAttackEnd?.Invoke();
        }
    }
}