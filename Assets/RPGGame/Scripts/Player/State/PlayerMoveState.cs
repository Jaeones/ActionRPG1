using UnityEngine;

namespace RPGGame
{
    public class PlayerMoveState : PlayerStateBase
    {
        //[SerializeField] private float rotationSpeed = 540f;

        private Transform cameraTransform;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (cameraTransform == null)
            {
                cameraTransform = Camera.main.transform;
            }
        }

        protected override void Update()
        {
            base.Update();

            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            Vector3 direction = InputManager.Movement.x * cameraTransform.right + InputManager.Movement.y * cameraForward;
            direction.y = 0f;

            //Vector3 direction = new Vector3(InputManager.Movement.x, 0f, InputManager.Movement.y);

            if (direction.sqrMagnitude > 1.0f)
            {
                direction.Normalize();
            }

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, playerData.rotationSpeed * Time.deltaTime);
            }
        }

        private void PlayStep()
        {

        }
    }
}
