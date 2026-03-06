using UnityEngine;


namespace RPGGame
{
    public class CameraRig : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;

        [SerializeField] private float movementDelay = 5f;

        private Transform refTransform;

        private Camera refCamera;

        [SerializeField] private float rotationDelay = 5f;
        [SerializeField] private float rotationSpeed = 0.2f;

        [SerializeField] private Vector2 rotationXMinMax = new Vector2(-20f , 25f);

        [SerializeField] private PlayerStateManager playerStateManager;

        private float xRotation = 0f;
        private float yRotation = 0f;

        private void Awake()
        {
            if (followTarget == null)
            {
                refTransform = transform;
            }

            if (followTarget == null)
            {
                followTarget = GameObject.FindGameObjectWithTag("Player").transform;
            }

            if (refCamera == null)
            {
                refCamera = Camera.main;
            }

            if (playerStateManager == null)
            {
                playerStateManager = FindFirstObjectByType<PlayerStateManager>();
            }
        }

        private void LateUpdate()
        {
            PlayerStateManager.State currentState = playerStateManager.currentState;

            if (currentState == PlayerStateManager.State.None || currentState == PlayerStateManager.State.Dead)
            {
                return;
            }

            refTransform.position = Vector3.Lerp(refTransform.position, followTarget.position, movementDelay * Time.deltaTime);

            if(UiInventoryWindow.IsOn)
            {
                return;
            }

            xRotation -= InputManager.MouseMove.y * rotationSpeed;
            xRotation = Mathf.Clamp(xRotation, rotationXMinMax.x, rotationXMinMax.y);

            yRotation += InputManager.MouseMove.x * rotationSpeed;

            Quaternion startRotation = refTransform.rotation;
            Quaternion endRotation = Quaternion.Euler(new Vector3(xRotation, yRotation, 0f));

            refTransform.rotation = Quaternion.Slerp(startRotation, endRotation, rotationDelay * Time.deltaTime);
        }

    }

}
