using System.Collections;
using UnityEngine;


namespace RPGGame
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;

        [SerializeField] private float shakeTime = 0.4f;
        [SerializeField] private float shakeAmount = 0.5f;

        private Vector3 originalPos;

        private bool isShaking = false;


        private void Awake()
        {
            cameraTransform = Camera.main.transform;
        }

        private void OnEnable()
        {
            originalPos = cameraTransform.localPosition;

        }

        public void Play()
        {
            if (isShaking)
            {
                return;
            }

            originalPos = cameraTransform.localPosition;
            StartCoroutine(ShakeCamera());
        }

        IEnumerator ShakeCamera()
        {
            isShaking = true;
            float elapsed = 0.0f;
            while (elapsed < shakeTime)
            {
                Vector3 shakePosition = Random.insideUnitSphere * shakeAmount;

                cameraTransform.localPosition = originalPos + shakePosition;

                elapsed += Time.deltaTime;
                yield return null;
            }
            cameraTransform.localPosition = originalPos;
            isShaking = false;
        }
    }

}
