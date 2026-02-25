using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace RPGGame
{
    public class PlayerController : MonoBehaviour
    {
        Transform refTransform;

        //이동 관련 입력 액션
        InputAction moveAction;

        [SerializeField]
        float moveSpeed = 3f;

        [SerializeField]
        float rotationSpeed = 720f; // rotationSpeed 필드 추가

        [SerializeField]
        Animator refAnimator;

        private void Awake()
        {
            if (refTransform == null)
            {
                refTransform = transform;
            }

            if (moveAction == null)
            {
                moveAction = InputSystem.actions.FindAction("Move");
            }

            if (refAnimator == null)
            {
                refAnimator = GetComponent<Animator>();
            }

        }

        private void Update()
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();

            Vector3 direction = new Vector3(moveValue.x, 0f, moveValue.y);
            direction.Normalize();

            refTransform.position = refTransform.position + direction * moveSpeed * Time.deltaTime;

            if(direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                refTransform.rotation = Quaternion.Slerp(refTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            if (moveValue == Vector2.zero)
            {
                refAnimator.SetInteger("State", 0);
            }
            else
            {
                refAnimator.SetInteger("State", 1);
            }
        }

        public void PlayStep()
        {

        }

    }
}

