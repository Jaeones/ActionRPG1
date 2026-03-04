using UnityEngine;


namespace RPGGame
{
    public class GrenadierIdleState : GrenadierStateBase
    {
        protected override void Update()
        {
            base.Update();

            float distanceToPlayer = (manager.playerTransform.position - refTransform.position).sqrMagnitude;

            if (distanceToPlayer <= manager.data.sightRange * manager.data.sightRange &&
                distanceToPlayer > manager.data.rangeAttackRange * manager.data.rangeAttackRange)
            {
                manager.SetState(GrenadierStateManager.State.Rotate);
                return;
            }

            if (Util.IsInSight(refTransform, manager.playerTransform, manager.data.sightAngle, manager.data.rangeAttackRange))
            {
                manager.ChangeToAttack();
                return;
            }

        }
    }
}
