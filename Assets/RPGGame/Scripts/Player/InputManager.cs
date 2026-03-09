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

        public static Vector2 MouseMove { get; private set; } = Vector2.zero;

        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction attackAction;
        private InputAction cameraRotationAction;

        private void OnEnable()
        {
            UpdateCursorState(false);
        }
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
            if(cameraRotationAction == null)
            {
                cameraRotationAction = InputSystem.actions.FindAction("Look");
            }
        }

        private void Update()
        {
            bool toggleInventory = Keyboard.current != null &&
                (Keyboard.current.iKey.wasPressedThisFrame || Keyboard.current.tabKey.wasPressedThisFrame);

            if (toggleInventory)
            {
                if (UiInventoryWindow.IsOn) UiInventoryWindow.CloseWindow();
                else UiInventoryWindow.ShowWindow();
            }

            bool toggleQuest = Keyboard.current != null && Keyboard.current.jKey.wasPressedThisFrame;
            if (toggleQuest)
            {
                if (UIQuestWindow.IsOn) UIQuestWindow.Close();
                else UIQuestWindow.Show();
            }

            bool isGameMenuOpen = GameManager.Instance != null && GameManager.Instance.IsGameMenuOpen;
            bool isUIOpen = UiInventoryWindow.IsOn || UIQuestWindow.IsOn || isGameMenuOpen;
            UpdateCursorState(isUIOpen);

            if (isUIOpen)
            {
                Movement = Vector2.zero;
                IsJump = false;
                IsAttack = false;
                MouseMove = Vector2.zero;
                return;
            }

            Movement = moveAction.ReadValue<Vector2>();
            IsJump = jumpAction.WasPressedThisFrame();

            bool overUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
            IsAttack = !overUI && attackAction.WasPressedThisFrame();
            MouseMove = cameraRotationAction.ReadValue<Vector2>();
        }

        private static void UpdateCursorState(bool isUIOpen)
        {
            Cursor.lockState = isUIOpen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isUIOpen;
        }

    }
}
