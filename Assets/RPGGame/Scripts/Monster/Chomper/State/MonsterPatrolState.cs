using System;
using UnityEngine;

namespace RPGGame
{

    public class MonsterPatrolState : MonsterStateBase
    {
        [SerializeField] private float patrolDistance = 6f;
        [SerializeField] private Vector3 patrolDestination;

        [SerializeField] private float validPatrolTime = 3f;

        [SerializeField] private Transform pointer;     // for debug

        // 테스트 설정 옵션
        [SerializeField] private bool test = false;

        private float patrolStartTime = 0f;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (test)
            {
                pointer.SetParent(null);
                pointer.position = Vector3.zero;
            }

            if(Util.RandomPoint(refTransform.position, patrolDistance, out patrolDestination))
            {
                patrolStartTime = Time.time;

                if (test)
                {
                    pointer.position = patrolDestination;

                    pointer.gameObject.SetActive(true);
                }
                else
                {
                    pointer.gameObject.SetActive(false);
                }
            }
            else
            {
                ResetPoint();

                manager.SetState(MonsterStateManager.State.Idle);
            }


        }

        protected override void Update()
        {
            base.Update();
            if ( (Time.time > patrolStartTime + validPatrolTime))
            {
                manager.SetState(MonsterStateManager.State.Idle);
            }
            if (Util.IsArrived(refTransform, patrolDestination, 0.5f))
            {
                ResetPoint();
                manager.SetState(MonsterStateManager.State.Idle);
            }

            // 회전 처리
            Vector3 direction = patrolDestination - refTransform.position;
            direction.y = 0; // 수평 방향으로만 회전

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                refTransform.rotation = Quaternion.RotateTowards(refTransform.rotation, targetRotation, monsterData.patrolRotateSpeed * Time.deltaTime);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            patrolDestination = Vector3.zero;
            if (test)
            {
                ResetPoint();
            }
        }

        private void ResetPoint()
        {
            if (!transform.root.gameObject.activeInHierarchy) { return; }

            if (test)
            {
                pointer.gameObject.SetActive(false);
                pointer.SetParent(refTransform);
                pointer.localPosition = Vector3.zero;
            }
        }
    }

}
