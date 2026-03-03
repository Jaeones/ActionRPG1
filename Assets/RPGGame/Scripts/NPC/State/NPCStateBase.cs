using UnityEngine;


namespace RPGGame
{
    public class NPCStateBase : MonoBehaviour
    {
        protected Transform refTransform;

        protected CharacterController characterController;

        protected NPCStateManager manager;

        protected virtual void OnEnable()
        {
            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }
            if (refTransform == null)
            {
                refTransform = transform;
            }

            if (manager == null)
            {
                manager = GetComponent<NPCStateManager>();
            }
        }

        protected virtual void Update()
        {
            characterController.Move(Physics.gravity * Time.deltaTime);
        }

        protected bool CanTalk()
        {
            return Vector3.Distance(manager.PlayerTransform.position, refTransform.position) <= manager.data.interactionSight;
        }
    }

}
