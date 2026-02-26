using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace RPGGame
{
    [DefaultExecutionOrder(-1)]

    public class InputManager : MonoBehaviour
    {
        public static Vector2 Movement { get; private set; } = Vector2.zero;

        public static bool IsJump { get; private set; } = false;

        public static bool IsAttack { get; private set; } = false;

        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction attackAction;

        private void Awake()
        {
            if(moveAction == null)
            {
                moveAction = InputSystem.actions.FindAction("Move");
            }
            if(jumpAction == null)
            {
                jumpAction = InputSystem.actions.FindAction("Jump");
            }
            if(attackAction == null)
            {
                attackAction = InputSystem.actions.FindAction("Attack");
            }
        }

        private void Update()
        {
            Movement = moveAction.ReadValue<Vector2>();

            IsJump = jumpAction.WasPressedThisFrame();

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                IsAttack = attackAction.WasPressedThisFrame();
            }  
        }
    }
}
