using UnityEngine;


namespace RPGGame
{
    public class MonsterChaseState : MonsterStateBase
    {
        protected override void Update()
        {
            base.Update();

            if (manager.IsPlayerDead)
            {
                manager.SetState(MonsterStateManager.State.Idle);
            }

            // 플레이어와의 거리가 공격 범위보다 가까우면 공격
            if (Vector3.Distance(refTransform.position, manager.playerTransform.position) <= monsterData.attackRange)
            {
                // 공격 스테이트 전환
                manager.SetState(MonsterStateManager.State.Attack);
            }

            Vector3 direction = (manager.playerTransform.position - refTransform.position).normalized;
            direction.y = 0f;

            if(direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                refTransform.rotation = Quaternion.RotateTowards(refTransform.rotation, targetRotation, Time.deltaTime * monsterData.chaseRotateSpeed);
            }
        }
    }

}
