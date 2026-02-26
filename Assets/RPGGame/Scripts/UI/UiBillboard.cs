using UnityEngine;


namespace RPGGame
{
    public class UiBillboard : MonoBehaviour
    {
        private Transform refTransform;
        private Camera mainCamera;

        private void Awake()
        {
            if (refTransform == null)
            {
                refTransform = transform;
            }

            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        void LateUpdate()
        {
            if (mainCamera == null)
            {
                return;
            }
            refTransform.forward = mainCamera.transform.forward;

        }
    }

}