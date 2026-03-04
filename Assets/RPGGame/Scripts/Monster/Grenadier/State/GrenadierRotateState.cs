using UnityEngine;
using UnityEngine.Events;


namespace RPGGame
{
    public class GrenadierRotateState : GrenadierStateBase
    {
        [SerializeField]
        private GrenadierAnimationController animationController;
        [SerializeField]
        private UnityEvent OnPlayStep;

        [SerializeField]
        private float angle = 0f;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (animationController == null)
            {
                animationController = GetComponentInChildren<GrenadierAnimationController>();
            }
        }

        protected override void Update()
        {
            base.Update();

            RotateToPlayer();

            if (Util.IsInSight(refTransform, manager.playerTransform, manager.data.sightAngle, manager.data.sightRange))
            {
                manager.ChangeToAttack();
            }

            if (Vector3.Distance(refTransform.position, manager.playerTransform.position) > manager.data.sightRange)
            {
                manager.SetState(GrenadierStateManager.State.Idle);
            }
        }

        private void PlayStep()
        {
            OnPlayStep?.Invoke();
        }

        public void RotateToPlayer()
        {
            // 8방향 회전
            Vector3 direction = (manager.playerTransform.position - refTransform.position);
            direction.y = 0f; // 수평 방향으로만 회전하도록 y축 고정

            if (direction == Vector3.zero)
            {
                // 플레이어가 현재 위치에 있는 경우 회전하지 않음
                return;
            }

            angle = Vector3.SignedAngle(refTransform.forward, direction, Vector3.up);

            if (Mathf.Abs(angle) < 20)
            {
                refTransform.rotation = Quaternion.LookRotation(direction);
                return;
            }

            angle /= 180f;  // 180도 단위로 변환
            animationController.SetAngle(angle);
        }

    }

}
