using UnityEngine;


namespace RPGGame
{
    public class MonsterIdleState : MonsterStateBase
    {
        private float elapsedTime = 0;
        protected override void OnEnable()
        {
            base.OnEnable();
            elapsedTime = 0;
            //refAnimator.SetTrigger("Idle");
        }

        protected override void Update()
        {
            base.Update();
            elapsedTime += Time.deltaTime;

            if (elapsedTime > monsterData.patrolWaitTime)
            {
                elapsedTime = 0f;
                manager.SetState(MonsterStateManager.State.Patrol);
            }
        }

        public void PlayStep()
        {

        }

        public void Grunt()
        {

        }
    }

}
