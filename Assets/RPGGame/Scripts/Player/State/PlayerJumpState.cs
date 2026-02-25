using UnityEngine;
using UnityEngine.Events;


namespace RPGGame
{
    public class PlayerJumpState : PlayerStateBase
    {
        //[SerializeField] private float JumpPower = 8f;

        [SerializeField] private float verticalSpeed = 0f;

        //[SerializeField] private float gravityInJump = 10f;

        [SerializeField] private float moveSpeed = 5f;

        [SerializeField] private UnityEvent OnJumpEnd;

        [SerializeField] private bool enableLandingDebugLog = true;

        [SerializeField] private int groundedFramesForLanding = 2;

        private bool? prevCanLand;
        private bool? prevIsGrounded;
        private bool hasBeenAirborne;
        private int groundedFrames;

        protected override void OnEnable()
        {
            base.OnEnable();
            verticalSpeed = playerData.jumpPower;
            prevCanLand = null;
            prevIsGrounded = null;
            hasBeenAirborne = false;
            groundedFrames = 0;
        }

        protected override void Update()
        {
            if (verticalSpeed > 0f)
            {
                verticalSpeed -= playerData.gravityScale * Time.deltaTime;
            }

            if (Mathf.Approximately(verticalSpeed, 0f))
            {
                verticalSpeed = 0f;
            }

            verticalSpeed += Physics.gravity.y * Time.deltaTime;

            Vector3 movement = moveSpeed * refTransform.forward * Time.deltaTime;
            movement += Vector3.up * verticalSpeed * Time.deltaTime;

            CollisionFlags moveFlags = characterController.Move(movement);
            bool collidedBelow = (moveFlags & CollisionFlags.Below) != 0;
            bool isGrounded = collidedBelow || characterController.isGrounded;

            if (!isGrounded)
            {
                hasBeenAirborne = true;
                groundedFrames = 0;
            }
            else if (hasBeenAirborne)
            {
                groundedFrames++;
            }

            bool isDescending = verticalSpeed <= 0f;
            bool canLand = hasBeenAirborne &&
                           isDescending &&
                           (collidedBelow || groundedFrames >= Mathf.Max(1, groundedFramesForLanding));

            if (enableLandingDebugLog &&
                (prevCanLand != canLand || prevIsGrounded != isGrounded))
            {
                Debug.Log(
                    $"[PlayerJumpState] canLand={canLand}, descending={isDescending}, grounded={isGrounded}, airborne={hasBeenAirborne}, groundedFrames={groundedFrames}, verticalSpeed={verticalSpeed:F3}",
                    this
                );
            }

            prevCanLand = canLand;
            prevIsGrounded = isGrounded;

            if (!canLand)
            {
                return;
            }

            if (enableLandingDebugLog)
            {
                Debug.Log("[PlayerJumpState] Landing condition met. Triggering landing.", this);
            }

            OnJumpEnd?.Invoke();
            animationController?.OnLanding();

            manager.SetState(PlayerStateManager.State.Idle);
        }

        protected override void OnAnimatorMove()
        {
            // Jump state uses explicit CharacterController movement in Update.
            // Applying animator root motion here can overwrite grounded state.
        }
    }
}
