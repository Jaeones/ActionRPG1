using UnityEngine;


namespace RPGGame
{
    public class CameraRig : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;

        [SerializeField] private float movementDelay = 5f;

        private Transform refTransform;

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
        }

        private void LateUpdate()
        {
            refTransform.position = Vector3.Lerp(refTransform.position, followTarget.position, movementDelay * Time.deltaTime);
        }

    }

}
