using UnityEngine;




namespace RPGGame
{
    public class PlayerStateBase : MonoBehaviour
    {
        protected Transform refTransform;

        protected CharacterController characterController;

        protected Animator refAnimator;

        protected PlayerStateManager manager;

        protected PlayerAnimationController animationController;

        // 플레이어가 사용할 데이터 변수
        protected PlayerData playerData;

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
                manager = GetComponent<PlayerStateManager>();
            }
            if (animationController == null)
            {
                animationController = GetComponentInChildren<PlayerAnimationController>();
            }
        }

        protected virtual void Update()
        {
            characterController.Move(Physics.gravity * Time.deltaTime);
        }

        protected virtual void OnDisable()
        {

        }

        protected virtual void OnAnimatorMove()
        {
            characterController.Move(refAnimator.deltaPosition);
        }

        public void SetData(PlayerData data)
        {
            this.playerData = data;
        }
    }
}

