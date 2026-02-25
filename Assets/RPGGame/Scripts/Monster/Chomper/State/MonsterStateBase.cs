using UnityEngine;


namespace RPGGame
{
    public class MonsterStateBase : MonoBehaviour
    {
        protected Transform refTransform;

        protected CharacterController characterController;

        protected Animator refAnimator;

        protected MonsterStateManager manager;

        //protected MonsterAnimationController animationController;

        // 플레이어가 사용할 데이터 변수
        protected MonsterData monsterData;

        protected virtual void OnEnable()
        {
            if (refTransform == null)
            {
                refTransform = transform;
            }
            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }
            if (refAnimator == null)
            {
                refAnimator = GetComponent<Animator>();
            }
            
            if (manager == null)
            {
                manager = GetComponent<MonsterStateManager>();
            }
            /*
            if (animationController == null)
            {
                animationController = GetComponentInChildren<MonsterAnimationController>();
            }
            */
        }

        protected virtual void Update()
        {
            if (characterController.enabled)
            {
                characterController.Move(Physics.gravity * Time.deltaTime);
            }
        }

        protected virtual void OnDisable()
        {

        }

        protected virtual void OnAnimatorMove()
        {
            if (characterController.enabled)
            {
                characterController.Move(refAnimator.deltaPosition);
            }
        }

        public void SetData(MonsterData data)
        {
            this.monsterData = data;
        }
    }
}